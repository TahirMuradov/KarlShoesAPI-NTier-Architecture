using Entities.DTOs.ShippingMethods;
using Entities.DTOs.ShippingMethodsDTOs;
using KarlShoes.Bussines.Abstarct;
using KarlShoes.Core.Utilities.Results.Abtsract;
using KarlShoes.DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarlShoes.Bussines.Concrete
{
    public class ShippingMethodManager : IShippingMethodService
    {
        private readonly IShippingMethodDAL _shippingMethodDAL;

        public ShippingMethodManager(IShippingMethodDAL shippingMethodDAL)
        {
            _shippingMethodDAL = shippingMethodDAL;
        }

        public IResult AddShipping(AddShippingMethodDTO addShipping)
        {
           return _shippingMethodDAL.AddShipping(addShipping);
        }

        public IResult DeleteShipping(string id)
        {
            return _shippingMethodDAL.DeleteShipping(id);
        }

        public IDataResult<GetShippingMethodDTO> GetShipping(string id, string langCode)
        {
            return _shippingMethodDAL.GetShipping(id:id,langCode: langCode);
        }

        public IDataResult<List<GetShippingMethodDTO>> GetShippingAll(string langCode)
        {
            return _shippingMethodDAL.GetShippingAll(langCode);
        }
    }
}
