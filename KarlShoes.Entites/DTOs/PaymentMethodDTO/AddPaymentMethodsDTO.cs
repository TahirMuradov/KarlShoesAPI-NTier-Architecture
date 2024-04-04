using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs.PaymentMethodDTOs
{
    public class AddPaymentMethodsDTO
    {
   public Dictionary<string,string> Content { get; set; }
        public bool Api {  get; set; }
       
    }
}
