using Entities.DTOs.ShippingMethods;
using Entities.DTOs.ShippingMethodsDTOs;
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
    public class EFShippingMethodDAL:IShippingMethodDAL
    {
        public IResult AddShipping(AddShippingMethodDTO addShipping)
        {
            try
            {
                var context = new AppDBContext();

                ShippingMethods shippingMethods = new ShippingMethods()
                {
                    DeliveryPrice = addShipping.DeliveryPrice,

                };
                context.ShippingMethods.Add(shippingMethods);
                context.SaveChanges();
                foreach (var item in addShipping.ShippingContent)
                {
                    ShippingLanguage shippingLanguage = new ShippingLanguage()
                    {
                        Content=item.Value,
                        LangCode = item.Key,
                        ShippingMethodId=shippingMethods.Id
                        
                    };
                    context.ShippingLaunguages.Add(shippingLanguage);

                }
                context.SaveChanges();

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
                var context = new AppDBContext();
                var shippingMethod = context.ShippingMethods.FirstOrDefault(x => x.Id.ToString() == id);
                var shippingMethodLanguages = context.ShippingLaunguages.Where(x => x.ShippingMethodId.ToString() == id).ToList();
                context.ShippingLaunguages.RemoveRange(shippingMethodLanguages);
                context.ShippingMethods.Remove(shippingMethod);
                context.SaveChanges();

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
                using (var context = new AppDBContext())
                {
                    var result = context.ShippingMethods.Include(x => x.ShippingLanguage)
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
                var context = new AppDBContext();
                var result = context.ShippingMethods.Select(x => new GetShippingMethodDTO
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
