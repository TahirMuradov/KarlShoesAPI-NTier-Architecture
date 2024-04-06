using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarlShoes.Entites.DTOs.OrderDTOs
{
    public class AddOrderDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
   
        public string? Message { get; set; }
    
        public List<OrderProductDTO> OrderProducts { get; set; }
 
        public string PaymentMethodId { get; set; }
        public string ShippingMethodId { get; set; }

    }
}
