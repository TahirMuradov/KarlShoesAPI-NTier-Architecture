using Entities.DTOs.ShippingMethods;
using Entities.DTOs.ShippingMethodsDTOs;
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
    public class EFShippingMethodDAL:IShippingMethodDAL
    {
        private readonly AppDBContext _context;

        public EFShippingMethodDAL(AppDBContext context)
        {
            _context = context;
        }

        public IResult AddShipping(AddShippingMethodDTO addShipping)
        {
            try
            {
               

                ShippingMethods shippingMethods = new ShippingMethods()
                {
                    DeliveryPrice = addShipping.DeliveryPrice,

                };
                _context.ShippingMethods.Add(shippingMethods);
               _context.SaveChanges();
                foreach (var item in addShipping.ShippingContent)
                {
                    ShippingLanguage shippingLanguage = new ShippingLanguage()
                    {
                        Content=item.Value,
                        LangCode = item.Key,
                        ShippingMethodId=shippingMethods.Id
                        
                    };
                    _context.ShippingLaunguages.Add(shippingLanguage);

                }
                _context.SaveChanges();

                return new SuccessResult(statusCode: HttpStatusCode.OK);
            }
            catch (Exception ex)
            {

                return new ErrorResult(message: ex.Message, statusCode: HttpStatusCode.BadRequest);
            }


        }

        public IResult DeleteShipping(string id)
        {
            try
            {
              
                var shippingMethod = _context.ShippingMethods.FirstOrDefault(x => x.Id.ToString() == id);
                var shippingMethodLanguages = _context.ShippingLaunguages.Where(x => x.ShippingMethodId.ToString() == id).ToList();
                _context.ShippingLaunguages.RemoveRange(shippingMethodLanguages);
               _context.ShippingMethods.Remove(shippingMethod);
                _context.SaveChanges();

                return new SuccessResult(statusCode:HttpStatusCode.OK);
            }
            catch (Exception ex)
            {

                return new ErrorResult(message: ex.Message,statusCode:HttpStatusCode.BadRequest);
            }
        }

        public IDataResult<GetShippingMethodDTO> GetShipping(string id, string langCode)
        {
            try
            {
             
                    var result = _context.ShippingMethods.Include(x => x.ShippingLanguage)
                        .FirstOrDefault(x => x.Id.ToString() == id);

                    if (result == null)
                    {
                        return new ErrorDataResult<GetShippingMethodDTO>(message: "Shipping method not found for the given id or langCode",statusCode:HttpStatusCode.NotFound);
                    }

                    return new SuccessDataResult<GetShippingMethodDTO>(data: new GetShippingMethodDTO
                    {
                        Id = result.Id.ToString(),
                        Content = result.ShippingLanguage.FirstOrDefault(x =>x.LangCode==langCode).Content,
                        Price = result.DeliveryPrice,
                    },statusCode:HttpStatusCode.OK);
                
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<GetShippingMethodDTO>(ex.Message,statusCode:HttpStatusCode.BadRequest);
            }
        }

        public IDataResult<List<GetShippingMethodDTO>> GetShippingAll(string langCode)
        {
            try
            {
                var result = _context.ShippingMethods.Select(x => new GetShippingMethodDTO
                {
                    Id = x.Id.ToString(),
                    Content = x.ShippingLanguage.FirstOrDefault(y => y.LangCode == langCode).Content,
                    Price = x.DeliveryPrice


                }).ToList();
                return new SuccessDataResult<List<GetShippingMethodDTO>>(data:result,statusCode:HttpStatusCode.OK);
            }
            catch (Exception ex)
            {

                return new ErrorDataResult<List<GetShippingMethodDTO>>(message: ex.Message, statusCode: HttpStatusCode.BadRequest);
            }
        }
    }
}
