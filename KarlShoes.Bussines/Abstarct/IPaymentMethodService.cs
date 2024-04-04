using Entities.DTOs.PaymentMethodDTOs;
using KarlShoes.Core.Utilities.Results.Abtsract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarlShoes.Bussines.Abstarct
{
    public interface IPaymentMethodService
    {
        public IResult AddPaymentMethod(AddPaymentMethodsDTO addPaymentMethodsDTO);
        public IResult DeletePaymentMethod(string Id);
        public IDataResult<List<GetPaymentMethodDTO>> GetAllPaymentMethod(string langCode);
        public IDataResult<GetPaymentMethodDTO> GetPaymentMethod(string Id, string langCode);
    }
}
