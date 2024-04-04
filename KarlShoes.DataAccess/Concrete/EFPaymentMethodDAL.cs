using Entities.DTOs.PaymentMethodDTOs;
using KarlShoes.Core.Utilities.Results.Abtsract;
using KarlShoes.Core.Utilities.Results.Concrete.ErrorResults;
using KarlShoes.Core.Utilities.Results.Concrete.SuccessResults;
using KarlShoes.DataAccess.Abstract;
using KarlShoes.DataAccess.Concrete.SQLServer;
using KarlShoes.Entites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace KarlShoes.DataAccess.Concrete
{
    public class EFPaymentMethodDAL:IPaymentMethodDAL
    {
        public IResult AddPaymentMethod(AddPaymentMethodsDTO addPaymentMethodsDTO)
        {
            try
            {

                using (var context = new AppDBContext())
                {


                    PaymentMethod paymentMethod = new PaymentMethod()
                    {
                        Api = addPaymentMethodsDTO.Api,
                    };
                    context.PaymentMethods.Add(paymentMethod);
                    context.SaveChanges();

                    foreach (var item in addPaymentMethodsDTO.Content)
                    {
                        PaymentMethodLanguage paymentMethodLanguage = new PaymentMethodLanguage()
                        {
                            Content=item.Value,
                            LangCode = item.Key,
                            PaymentMehtodId=paymentMethod.Id
                        };
                        context.PaymentMethodsLaunge.Add(paymentMethodLanguage);
                    }
                    context.SaveChanges();
                }



                return new SuccessResult(statusCode: HttpStatusCode.OK);
            }
            catch (Exception ex)
            {

                return new ErrorResult(message:ex.Message,statusCode:HttpStatusCode.BadRequest);
            }

        }

        public IResult DeletePaymentMethod(string Id)
        {
            try
            {
                using (var context = new AppDBContext())
                {
                    var paymentMethod = context.PaymentMethods.FirstOrDefault(x => x.Id.ToString() == Id);
                    if (paymentMethod == null) return new ErrorResult(message: "Payment Method is NotFound", statusCode: HttpStatusCode.NotFound);
                    var paymentMethodsLaunge = context.PaymentMethodsLaunge.Where(x => x.PaymentMehtodId.ToString() == Id).ToList();
                    context.PaymentMethodsLaunge.RemoveRange(paymentMethodsLaunge);
                    context.PaymentMethods.Remove(paymentMethod);
                    context.SaveChanges();

                }

                return new SuccessResult(statusCode: HttpStatusCode.OK);
            }
            catch (Exception ex)
            {

                return new ErrorResult(message: ex.Message, statusCode: HttpStatusCode.BadRequest);
            }
     
        }

        public IDataResult<List<GetPaymentMethodDTO>> GetAllPaymentMethod(string langCode)
        {
            try
            {
                using var context = new AppDBContext();



                List<GetPaymentMethodDTO> PaymentMethods = context.PaymentMethods.Include(x => x.PaymentMethodLanguages).Select(x => new GetPaymentMethodDTO
                {
                    Api = x.Api,
                    Content = x.PaymentMethodLanguages.FirstOrDefault(y => y.LangCode == langCode).Content,
                    Id = x.Id.ToString()


                }).ToList();
                return new SuccessDataResult<List<GetPaymentMethodDTO>>(data: PaymentMethods, statusCode: HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<GetPaymentMethodDTO>>(message: ex.Message, HttpStatusCode.BadRequest);
            }
        
          
          
        }

        public IDataResult<GetPaymentMethodDTO> GetPaymentMethod(string Id, string langCode)
        {
            try
            {
                using var context = new AppDBContext();
              
                var paymentMethod = context.PaymentMethods.Include(x => x.PaymentMethodLanguages).FirstOrDefault(x => x.Id.ToString() == Id).PaymentMethodLanguages.FirstOrDefault(y => y.LangCode == langCode);

                    if (paymentMethod == null) return new ErrorDataResult<GetPaymentMethodDTO>(statusCode: HttpStatusCode.NotFound, message: "Payment Method is NotFound");

                GetPaymentMethodDTO result = new GetPaymentMethodDTO
                    {
                        Id = paymentMethod.PaymentMehtodId.ToString(),
                        Content = paymentMethod.Content,
                        Api = paymentMethod.PaymentMethod.Api,

                    };
                    return new SuccessDataResult<GetPaymentMethodDTO>(data: result, statusCode: HttpStatusCode.OK);
               
            }
            catch (Exception ex)
            {

                return new ErrorDataResult<GetPaymentMethodDTO>(message: ex.Message, statusCode: HttpStatusCode.BadRequest);
            }
          
          
        }

    }
}
