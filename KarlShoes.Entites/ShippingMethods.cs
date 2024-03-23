using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarlShoes.Entites
{
    public class ShippingMethods
    {
        public Guid Id { get; set; }
        public List<ShippingLanguage> ShippingLanguage { get; set; }
        public decimal DeliveryPrice { get; set; }
    }
}
