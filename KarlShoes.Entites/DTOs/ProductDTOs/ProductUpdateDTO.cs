using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarlShoes.Entites.DTOs.ProductDTOs
{
    public class ProductUpdateDTO
    {
        public string ProductId { get; set; }
        public Dictionary<string, string> LangCodeAndProductName { get; set; }
        public Dictionary<string, string> LangCodeAndProductDescription { get; set; }
        public Dictionary<int, int> SizeAndCount { get; set; }

        public List<string> CatgeoryId { get; set; }
        public List<string>? SubCategoryID { get; set; }
        public string color { get; set; }
        public decimal DisCount { get; set; }
        public decimal Price { get; set; }
        public bool isFeatured { get; set; }
    }
}
