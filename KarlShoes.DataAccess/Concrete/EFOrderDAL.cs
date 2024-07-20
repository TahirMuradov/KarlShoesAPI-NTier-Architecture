using Entities.DTOs.CheckOutDTOs;
using KarlShoes.Core.Helper.EmailHelper.Abstrac;
using KarlShoes.Core.Helper.FileHelper;
using KarlShoes.Core.Utilities.Results.Abtsract;
using KarlShoes.Core.Utilities.Results.Concrete.ErrorResults;
using KarlShoes.Core.Utilities.Results.Concrete.SuccessResults;
using KarlShoes.DataAccess.Abstract;
using KarlShoes.DataAccess.Concrete.SQLServer;
using KarlShoes.Entites;
using KarlShoes.Entites.DTOs.OrderDTOs;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace KarlShoes.DataAccess.Concrete
{
    public class EFOrderDAL : IOrderDAL
    {
        private readonly IEmailHelper _emailHelper;
        private readonly AppDBContext _context;
        public EFOrderDAL(IEmailHelper emailHelper, AppDBContext context)
        {
            _emailHelper = emailHelper;
            _context = context;
        }

        public async  Task<IDataResult<string>> AddOrderAsync(AddOrderDTO orderDTO)
        {
            try
            {
               

                    foreach (var Product in orderDTO.OrderProducts)
                    {
                        var checkedData = _context.Products.Include(x => x.ProductSizes).ThenInclude(x => x.Size).FirstOrDefault(x => x.Id == Product.ProductId);
                        if (checkedData == null)  return new ErrorDataResult<string>(message: $"Product is Not Found this id: {Product.ProductId}",statusCode:HttpStatusCode.NotFound);

                        var size = checkedData.ProductSizes.FirstOrDefault(x => x.Size.NumberSize == int.Parse( Product.Size));

                        if (size == null) return new ErrorDataResult<string>(message: $"This size:{Product.Size} is Not Found ", statusCode: HttpStatusCode.NotFound);
                        if (size.SizeStockCount<Product.Count) return new ErrorDataResult<string>(message: $"Size Count is Not Found this Size: {size.Size.NumberSize} and size Current Count: {size.SizeStockCount}", statusCode: HttpStatusCode.NotFound);
                       
                    }

                    var ChekeckdShippingMethod=_context.ShippingMethods.Include(x=>x.ShippingLanguage).FirstOrDefault(x=>x.Id.ToString()==orderDTO.ShippingMethodId);
                    if (ChekeckdShippingMethod == null) return new ErrorDataResult<string>(message: "ShippingMethod is Not Found", statusCode: HttpStatusCode.NotFound);
                    var CheckedPaymentMethod = _context.PaymentMethods.Include(x => x.PaymentMethodLanguages).FirstOrDefault(x => x.Id.ToString() == orderDTO.PaymentMethodId);
                    if (CheckedPaymentMethod == null) return new ErrorDataResult<string>(message: "PaymentMethod is Not Found", statusCode: HttpStatusCode.NotFound);
                    Order order = new Order()
                    {
                        FirstName = orderDTO.FirstName,
                        Address = orderDTO.Address,
                        CreatedDate = DateTime.Now,
                        Email = orderDTO.Email,
                        LastName = orderDTO.LastName,
                        Message = orderDTO.Message,

                        PhoneNumber = orderDTO.PhoneNumber,
                        OrderStatus = 0,
                        PaymentMethodId = orderDTO.PaymentMethodId,
                        ShippingMethodId = orderDTO.ShippingMethodId,



                    };
                    _context.Orders.Add(order);
                    _context.SaveChanges();

                    foreach (var item in orderDTO.OrderProducts)
                    {
                        var checekdProduct = _context.Products.Include(x => x.productLanguages).Include(x => x.ProductSizes).ThenInclude(x => x.Size).FirstOrDefault(x => x.Id == item.ProductId);

                        var ProduztSIzeCount = checekdProduct.ProductSizes.FirstOrDefault(x => x.ProductId == checekdProduct.Id && x.Size.NumberSize == int.Parse(item.Size));
                  
                        OrderProduct product = new OrderProduct()
                        {
                            Amount = checekdProduct.Price,
                            Count = item.Count > ProduztSIzeCount.SizeStockCount ? ProduztSIzeCount.SizeStockCount : item.Count,
                            CreatedDate = DateTime.Now,
                            OrderId = order.Id,
                            ProductId = item.ProductId,
                            ProductName = checekdProduct.productLanguages.FirstOrDefault(x => x.LangCode == "az").ProductName,
                            ProductCode = checekdProduct.ProductCode,
                            Size = item.Size,

                        };
                        ProduztSIzeCount.SizeStockCount = ProduztSIzeCount.SizeStockCount - product.Count;
                        if (ProduztSIzeCount.SizeStockCount == 0)
                        {

                            _context.ProductSizes.Remove(ProduztSIzeCount);
                        }
                        else
                        {

                            _context.ProductSizes.Update(ProduztSIzeCount);
                        }


                        order.TotalAmount += checekdProduct.Price * product.Count;
                        _context.OrderProducts.Add(product);
                    }




                    _context.SaveChanges();
                  

     
                    var items = order.Products.Select(x => new GeneratePdfOrderProductDTO
                    {
                        Price = x.Amount,
                        ProductCode = x.ProductCode,
                        ProductName = x.ProductName,
                        Quantity = x.Count,
                        size = int.Parse(x.Size),

                    }).ToList();
                    var shippingMethod = _context.ShippingMethods.Include(x => x.ShippingLanguage).Select(x => new ShippingMethodInOrderPdfDTO
                    {
                        Id = x.Id.ToString(),
                        Price = x.DeliveryPrice,
                        ShippingContent = x.ShippingLanguage.FirstOrDefault(y => y.LangCode == "az").Content
                    }).FirstOrDefault(x => x.Id == order.ShippingMethodId);
                    var paymentMethod = _context.PaymentMethods.Include(x => x.PaymentMethodLanguages).Select(x => new PaymentMethodInOrderPdfDTO
                    {
                        Content = x.PaymentMethodLanguages.FirstOrDefault(y => y.LangCode == "az").Content,
                        Id = x.Id.ToString()
                    }).FirstOrDefault(x => x.Id == order.PaymentMethodId);
                  List<string> pdfInfo = FileHelper.SaveOrderPdf(
                         items: items,
                        shippingMethod: shippingMethod,
                        paymentMethod: paymentMethod
                        );
                    order.OrderPDfUrl =pdfInfo[0];
                    order.OrderNumber = pdfInfo[1];
                    _context.Orders.Update(order);
                   _context.SaveChanges();
                  var SendPfdResult= await _emailHelper.SendEmailPdfAsync(userEmail: order.Email, UserName: order.FirstName + " " + order.LastName, order.OrderPDfUrl);
                    if (!SendPfdResult.IsSuccess) return new ErrorDataResult<string>(data: order.OrderPDfUrl, message: "email could not be sent", HttpStatusCode.BadRequest);
                   
                    return new SuccessDataResult<string>(data:order.OrderPDfUrl, statusCode: HttpStatusCode.OK);
                

            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.InnerException?.Message);

                return new ErrorDataResult<string>(message: ex.Message, statusCode: HttpStatusCode.BadRequest);
            }
        }

        public IResult DeleteOrder(string OrderId)
        {
            try
            {
              
                    var data = _context.Orders.Include(x => x.Products).FirstOrDefault(x => x.Id.ToString() == OrderId);
                    if (data is null) return new ErrorResult(message: "Order is Not Founf", statusCode: HttpStatusCode.NotFound);
                    foreach (var product in data.Products)
                    {
                        _context.OrderProducts.RemoveRange(_context.OrderProducts.Where(x=>x.OrderId==product.OrderId));
                    }

                    bool checekPdf = FileHelper.RemoveFile(data.OrderPDfUrl,Pdf:true);
                        if (checekPdf)
                    {

                    _context.Orders.Remove(data);
                    _context.SaveChanges();
                        return new SuccessResult(statusCode: HttpStatusCode.OK);
                    }
                    return new ErrorResult(statusCode: HttpStatusCode.BadRequest);
                  

                
            }
            catch (Exception ex)
            {

                return new ErrorResult(message:ex.Message, statusCode: HttpStatusCode.BadRequest);
            }
        }

        public IDataResult<List<GetOrderDTO>> GetAllOrder()
        {
            try
            {
             
               
                    var data = _context.Orders.Include(x => x.Products).Select(x => new GetOrderDTO
                    {
                        Address = x.Address,
                        Email = x.Email,
                        FirstName = x.FirstName,
                        LastName = x.LastName,
                        Message = x.Message,
                        OrderId = x.Id.ToString(),
                        OrderNumber = x.OrderNumber,
                        OrderPDfUrl = x.OrderPDfUrl,
                        OrderProducts = x.Products.Select(y => new OrderProductDTO
                        {

                            Count = y.Count,
                           
                          
                            ProductId = y.ProductId,
                            Size = y.Size
                        }).ToList(),
                        PaymentMethodId = x.PaymentMethodId,
                        PhoneNumber = x.PhoneNumber,
                        ShippingMethodId = x.ShippingMethodId
                    }).ToList();
                return new SuccessDataResult<List<GetOrderDTO>>(data: data, statusCode: HttpStatusCode.OK);
            }
            catch (Exception ex)
            {

                return new ErrorDataResult<List<GetOrderDTO>>(message: ex.Message, statusCode: HttpStatusCode.BadRequest);
            }
        }

        public IDataResult<GetOrderDTO> GetOrder(string id)
        {
            try
            {
              

                var data = _context.Orders.Include(x => x.Products).Select(x => new GetOrderDTO
                {
                    Address = x.Address,
                    Email = x.Email,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Message = x.Message,
                    OrderId = x.Id.ToString(),
                    OrderNumber = x.OrderNumber,
                    OrderPDfUrl = x.OrderPDfUrl,
                    OrderProducts = x.Products.Select(y => new OrderProductDTO
                    {
                       
                        Count = y.Count,
                    
                        ProductId = y.ProductId,
                        Size = y.Size
                    }).ToList(),
                    PaymentMethodId = x.PaymentMethodId,
                    PhoneNumber = x.PhoneNumber,
                    ShippingMethodId = x.ShippingMethodId
                }).FirstOrDefault(x=>x.OrderId==id);
                return new SuccessDataResult<GetOrderDTO>(data: data, statusCode: HttpStatusCode.OK);
            }
            catch (Exception ex)
            {

                return new ErrorDataResult<GetOrderDTO>(message: ex.Message, statusCode: HttpStatusCode.BadRequest);
            }
        }
    }
}
