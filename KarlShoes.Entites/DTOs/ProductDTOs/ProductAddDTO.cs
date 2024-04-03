using Microsoft.AspNetCore.Http;

namespace KarlShoes.Entites.DTOs.ProductDTOs
{
    public class ProductAddDTO
    {
        public Dictionary<string, string> LangCodeAndProductName { get; set; }
        public Dictionary<string, string> LangCodeAndProductDescription { get; set; }
        public Dictionary<int,int> SizeAndCount { get; set; }
     
        public List<string> CatgeoryId { get; set; }
        public List<string>?SubCategoryID { get; set; }
        public string ProductCode { get; set; }
        public string color { get; set; }
        public decimal DisCount { get; set; }
        public decimal Price { get; set; }
        public bool isFeatured { get; set; }

    }
}
