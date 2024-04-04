using Entities.DTOs.PaymentMethodDTOs;
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
    internal class PaymentMethodManager : IPaymentMethodService
    {
        private readonly IPaymentMethodDAL _paymentMethodDAL;

        public PaymentMethodManager(IPaymentMethodDAL paymentMethodDAL)
        {
            _paymentMethodDAL = paymentMethodDAL;
        }

        public IResult AddPaymentMethod(AddPaymentMethodsDTO addPaymentMethodsDTO)
        {
            return _paymentMethodDAL.AddPaymentMethod(addPaymentMethodsDTO);
        }

        public IResult DeletePaymentMethod(string Id)
        {
            return _paymentMethodDAL.DeletePaymentMethod(Id);
                }

        public IDataResult<List<GetPaymentMethodDTO>> GetAllPaymentMethod(string langCode)
        {
            return _paymentMethodDAL.GetAllPaymentMethod(langCode);
        }

        public IDataResult<GetPaymentMethodDTO> GetPaymentMethod(string Id, string langCode)
        {
            return _paymentMethodDAL.GetPaymentMethod(Id: Id,langCode: langCode);
        }
    }
}
