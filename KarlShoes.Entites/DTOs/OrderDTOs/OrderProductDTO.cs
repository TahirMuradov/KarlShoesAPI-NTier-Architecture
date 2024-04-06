using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarlShoes.Entites.DTOs.OrderDTOs
{
    public class OrderProductDTO
    {
        public Guid ProductId { get; set; }
        //public string ProductName { get; set; }
        //public string ProductCode { get; set; }
        public string Size { get; set; }
        public int Count { get; set; }
     
    }
}
