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

        public EFOrderDAL(IEmailHelper emailHelper)
        {
            _emailHelper = emailHelper;
        }

        public async  Task<IDataResult<string>> AddOrderAsync(AddOrderDTO orderDTO)
        {
            try
            {
                using (var context=new AppDBContext())
                {
                    Order order = new Order()
                    {
                        FirstName=orderDTO.FirstName,
                    Address=orderDTO.Address,
                    CreatedDate=DateTime.Now,
                    Email=orderDTO.Email,
                    LastName=orderDTO.LastName, 
                    Message=orderDTO.Message,
                 
                    PhoneNumber=orderDTO.PhoneNumber,
                    OrderStatus=0,
                    PaymentMethodId=orderDTO.PaymentMethodId,
                    ShippingMethodId=orderDTO.ShippingMethodId,
                    
                    
                    
                    };
                    context.Orders.Add(order);
                    context.SaveChanges();
                   
                    foreach (var item in orderDTO.OrderProducts)
                    {
                        var checekdProduct = context.Products.Include(x=>x.productLanguages).Include(x=>x.ProductSizes).ThenInclude(x=>x.Size).FirstOrDefault(x => x.Id == item.ProductId);

                        var ProduztSIzeCount = checekdProduct.ProductSizes.FirstOrDefault(x => x.ProductId == checekdProduct.Id && x.Size.NumberSize == int.Parse(item.Size));
                        if (checekdProduct == null)  continue;
                        OrderProduct product = new OrderProduct()
                        {
                            Amount = checekdProduct.Price,
                            Count = item.Count> ProduztSIzeCount.SizeStockCount ? ProduztSIzeCount.SizeStockCount :item.Count,
                            CreatedDate = DateTime.Now,
                            OrderId = order.Id,
                            ProductId = item.ProductId,
                            ProductName = checekdProduct.productLanguages.FirstOrDefault(x=>x.LangCode=="az").ProductName,
                            ProductCode = checekdProduct.ProductCode,
                            Size = item.Size,

                        };
                        ProduztSIzeCount.SizeStockCount = ProduztSIzeCount.SizeStockCount-product.Count;
                        if (ProduztSIzeCount.SizeStockCount == 0)
                        {

                            context.ProductSizes.Remove(ProduztSIzeCount);
                        }
                        else
                        {

                        context.ProductSizes.Update(ProduztSIzeCount);
                        }


                        order.TotalAmount += checekdProduct.Price * product.Count;
                        context.OrderProducts.Add(product);
                    }

                  


                    context.Orders.Update(order);
                    context.SaveChanges();
                  

     
                    var items = order.Products.Select(x => new GeneratePdfOrderProductDTO
                    {
                        Price = x.Amount,
                        ProductCode = x.ProductCode,
                        ProductName = x.ProductName,
                        Quantity = x.Count,
                        size = int.Parse(x.Size),

                    }).ToList();
                    var shippingMethod = context.ShippingMethods.Include(x => x.ShippingLanguage).Select(x => new ShippingMethodInOrderPdfDTO
                    {
                        Id = x.Id.ToString(),
                        Price = x.DeliveryPrice,
                        ShippingContent = x.ShippingLanguage.FirstOrDefault(y => y.LangCode == "az").Content
                    }).FirstOrDefault(x => x.Id == order.ShippingMethodId);
                    var paymentMethod = context.PaymentMethods.Include(x => x.PaymentMethodLanguages).Select(x => new PaymentMethodInOrderPdfDTO
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
                    context.Orders.Update(order);
                    context.SaveChanges();
                  var SendPfdResult= await _emailHelper.SendEmailPdfAsync(userEmail: order.Email, UserName: order.FirstName + " " + order.LastName, order.OrderPDfUrl);
                    if (!SendPfdResult.IsSuccess) return new ErrorDataResult<string>(data: order.OrderPDfUrl, message: "email could not be sent", HttpStatusCode.BadRequest);
                   
                    return new SuccessDataResult<string>(data:order.OrderPDfUrl, statusCode: HttpStatusCode.OK);
                }

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
                using (var context=new AppDBContext())
                {
                    var data = context.Orders.Include(x => x.Products).FirstOrDefault(x => x.Id.ToString() == OrderId);
                    if (data is null) return new ErrorResult(message: "Order is Not Founf", statusCode: HttpStatusCode.NotFound);
                    foreach (var product in data.Products)
                    {
                        context.OrderProducts.RemoveRange(context.OrderProducts.Where(x=>x.OrderId==product.OrderId));
                    }

                    bool checekPdf = FileHelper.RemoveFile(data.OrderPDfUrl,Pdf:true);
                        if (checekPdf)
                    {

                    context.Orders.Remove(data);
                    context.SaveChanges();
                        return new SuccessResult(statusCode: HttpStatusCode.OK);
                    }
                    return new ErrorResult(statusCode: HttpStatusCode.BadRequest);
                  

                }
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
                using var context = new AppDBContext();
               
                    var data = context.Orders.Include(x => x.Products).Select(x => new GetOrderDTO
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
                using var context = new AppDBContext();

                var data = context.Orders.Include(x => x.Products).Select(x => new GetOrderDTO
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
