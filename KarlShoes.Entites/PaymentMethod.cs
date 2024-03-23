using KarlShoes.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarlShoes.Entites
{
    public class PaymentMethod: BaseEntity, IEntity
    {
   
        public bool Api { get; set; }
        public List<PaymentMethodLanguage> PaymentMethodLanguages { get; set; }
    }
}
