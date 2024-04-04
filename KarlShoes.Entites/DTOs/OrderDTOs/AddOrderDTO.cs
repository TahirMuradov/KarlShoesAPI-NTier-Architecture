using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarlShoes.Entites.DTOs.OrderDTOs
{
    public class AddOrderDTO
    {
      List<OrderProductDTO> OrderProducts { get; set; }
        public decimal TotalPrice { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public ShippingMethods ShippingMethod { get; set; }

    }
}
