using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarlShoes.Entites
{
    public class ShippingLanguage
    {
        public Guid Id { get; set; }
        public string LangCode { get; set; }
        public string Content { get; set; }
        public Guid ShippingMethodId { get; set; }
        public ShippingMethods ShippingMethod { get; set; }
    }
}
