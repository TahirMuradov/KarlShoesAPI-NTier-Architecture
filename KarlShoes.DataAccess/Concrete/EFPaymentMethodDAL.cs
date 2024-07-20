using Entities.DTOs.PaymentMethodDTOs;
using KarlShoes.Core.Utilities.Results.Abtsract;
using KarlShoes.Core.Utilities.Results.Concrete.ErrorResults;
using KarlShoes.Core.Utilities.Results.Concrete.SuccessResults;
using KarlShoes.DataAccess.Abstract;
using KarlShoes.DataAccess.Concrete.SQLServer;
using KarlShoes.Entites;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace KarlShoes.DataAccess.Concrete
{
    public class EFPaymentMethodDAL : IPaymentMethodDAL
    {
        private readonly AppDBContext _context;

        public EFPaymentMethodDAL(AppDBContext context)
        {
            _context = context;
        }

        public IResult AddPaymentMethod(AddPaymentMethodsDTO addPaymentMethodsDTO)
        {
            try
            {



                PaymentMethod paymentMethod = new PaymentMethod()
                {
                    Api = addPaymentMethodsDTO.Api,
                };
                _context.PaymentMethods.Add(paymentMethod);
                _context.SaveChanges();

                foreach (var item in addPaymentMethodsDTO.Content)
                {
                    PaymentMethodLanguage paymentMethodLanguage = new PaymentMethodLanguage()
                    {
                        Content = item.Value,
                        LangCode = item.Key,
                        PaymentMehtodId = paymentMethod.Id
                    };
                    _context.PaymentMethodsLaunge.Add(paymentMethodLanguage);
                }
                _context.SaveChanges();




                return new SuccessResult(statusCode: HttpStatusCode.OK);
            }
            catch (Exception ex)
            {

                return new ErrorResult(message: ex.Message, statusCode: HttpStatusCode.BadRequest);
            }

        }

        public IResult DeletePaymentMethod(string Id)
        {
            try
            {

                var paymentMethod = _context.PaymentMethods.FirstOrDefault(x => x.Id.ToString() == Id);
                if (paymentMethod == null) return new ErrorResult(message: "Payment Method is NotFound", statusCode: HttpStatusCode.NotFound);
                var paymentMethodsLaunge = _context.PaymentMethodsLaunge.Where(x => x.PaymentMehtodId.ToString() == Id).ToList();
                _context.PaymentMethodsLaunge.RemoveRange(paymentMethodsLaunge);
                _context.PaymentMethods.Remove(paymentMethod);
                _context.SaveChanges();



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




                List<GetPaymentMethodDTO> PaymentMethods = _context.PaymentMethods.Include(x => x.PaymentMethodLanguages).Select(x => new GetPaymentMethodDTO
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


                var paymentMethod = _context.PaymentMethods.Include(x => x.PaymentMethodLanguages).FirstOrDefault(x => x.Id.ToString() == Id).PaymentMethodLanguages.FirstOrDefault(y => y.LangCode == langCode);

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
