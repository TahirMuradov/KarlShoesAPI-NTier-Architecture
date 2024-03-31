using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarlShoes.Entites.DTOs.ProductDTOs
{
    public class ProductGetDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Dictionary<int,int> SizeAndCount { get; set; }
        public List<string> PictureUrls { get; set; }
        public Dictionary<string,string> Category { get; set; }
        public Dictionary<string,string> SubCategory { get; set; }
        public decimal Price { get; set; }
        public decimal DisCount { get; set; }
        public bool IsFeatured { get; set; }
        public string color { get; set; }
    }
}
