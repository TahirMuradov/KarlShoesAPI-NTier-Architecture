using Entities.DTOs.ShippingMethods;
using Entities.DTOs.ShippingMethodsDTOs;
using KarlShoes.Core.Utilities.Results.Abtsract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarlShoes.Bussines.Abstarct
{
    public interface IShippingMethodService
    {
        IResult AddShipping(AddShippingMethodDTO addShipping);
        IResult DeleteShipping(string id);
        IDataResult<GetShippingMethodDTO> GetShipping(string id, string langCode);
        IDataResult<List<GetShippingMethodDTO>> GetShippingAll(string langCode);
    }
}
