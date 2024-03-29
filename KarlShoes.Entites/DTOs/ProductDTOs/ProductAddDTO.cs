using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarlShoes.Entites.DTOs.ProductDTOs
{
    public class ProductAddDTO
    {
        public Dictionary<string,string> ProductName { get; set; }
        public Dictionary<string,string> ProductDescription { get; set; }
 
        public decimal DisCount { get; set; }
        public decimal Price { get; set; }
        public bool isFeatured { get; set; }

    }
}
