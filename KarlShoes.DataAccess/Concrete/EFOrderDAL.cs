using Entities.DTOs.CheckOutDTOs;
using KarlShoes.Core.Helper.EmailHelper.Abstrac;
using KarlShoes.Core.Helper.EmailHelper.Concrete;
using KarlShoes.Core.Helper.FileHelper;
using KarlShoes.Core.Utilities.Results.Abtsract;
using KarlShoes.Core.Utilities.Results.Concrete.ErrorResults;
using KarlShoes.Core.Utilities.Results.Concrete.SuccessResults;
using KarlShoes.DataAccess.Abstract;
using KarlShoes.DataAccess.Concrete.SQLServer;
using KarlShoes.Entites;
using KarlShoes.Entites.DTOs.OrderDTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

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
                    OrderNumber=orderDTO.OrderNumber,
                    PhoneNumber=orderDTO.PhoneNumber,
                    OrderStatus=0,
                    PaymentMethodId=orderDTO.PaymentMethodId,
                    ShippingMethodId=orderDTO.ShippingMethodId,
                    
                    
                    
                    };
                    context.Orders.Add(order);
                    context.SaveChanges ();
                    foreach (var item in orderDTO.OrderProducts)
                    {
                        OrderProduct product = new OrderProduct()
                        {
                            Amount = item.Amount,
                            Count = item.Count,
                            CreatedDate = DateTime.Now,
                            OrderId = order.Id,
                            ProductId = item.ProductId,
                            ProductName = item.ProductName,
                            ProductCode = item.ProductCode,
                            Size = item.Size,

                        };
                        order.TotalAmount += product.Amount * product.Count;
                        context.OrderProducts.Add(product);
                    }

                  


                    context.Orders.Update(order);
                    context.SaveChanges();


                    var oreder=context.Orders
                        
                        .Include(x=>x.Products)
                        .FirstOrDefault(x=>x.Id==order.Id);
                    oreder.OrderPDfUrl = FileHelper.SaveOrderPdf(
                         items: order.Products.Select(x => new GeneratePdfOrderProductDTO
                        {
                            Price = x.Amount,
                            ProductCode = x.ProductCode,
                            ProductName = x.ProductName,
                            Quantity = x.Count,
                            size = int.Parse(x.Size),

                        }).ToList(),
                        shippingMethod: context.ShippingMethods.Include(x => x.ShippingLanguage).Select(x => new ShippingMethodInOrderPdfDTO
                        {
                            Id = x.Id.ToString(),
                            Price = x.DeliveryPrice,
                            ShippingContent = x.ShippingLanguage.FirstOrDefault(y => y.LangCode == "az").Content
                        }).FirstOrDefault(x => x.Id == oreder.ShippingMethodId),
                        paymentMethod: context.PaymentMethods.Include(x => x.PaymentMethodLanguages).Select(x => new PaymentMethodInOrderPdfDTO
                        {
                            Content = x.PaymentMethodLanguages.FirstOrDefault(y => y.LangCode == "az").Content,
                            Id = x.Id.ToString()
                        }).FirstOrDefault(x => x.Id == oreder.PaymentMethodId)
                        );
                    context.Orders.Update(oreder);
                    context.SaveChanges();
                  var SendPfdResult= await _emailHelper.SendEmailPdfAsync(userEmail: oreder.Email, UserName: oreder.FirstName + " " + oreder.LastName, oreder.OrderPDfUrl);
                    if (!SendPfdResult.IsSuccess) return new ErrorDataResult<string>(data: oreder.OrderPDfUrl, message: "email could not be sent", HttpStatusCode.BadRequest);
                   
                    return new SuccessDataResult<string>(data:oreder.OrderPDfUrl, statusCode: HttpStatusCode.OK);
                }

            }
            catch (Exception ex)
            {

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

                    bool checekPdf = FileHelper.RemoveFile(data.OrderPDfUrl);
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
                            Amount = y.Amount,
                            Count = y.Count,
                            ProductCode = y.ProductCode,
                            ProductName = y.ProductName,
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
                        Amount = y.Amount,
                        Count = y.Count,
                        ProductCode = y.ProductCode,
                        ProductName = y.ProductName,
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
