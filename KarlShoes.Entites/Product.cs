using KarlShoes.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarlShoes.Entites
{
    public class Product:IEntity
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ProductCode { get; set; }

        public decimal DisCount { get; set; }
        public decimal Price { get; set; }
        public bool isFeatured { get; set; }

        public List<CategoryProduct> ProductCategories { get; set; }
        public List<SubCategoryProduct> SubCategories { get; set; }
        public List<OrderProduct>? OrderProducts { get; set; }

        public List<ProductSize>? ProductSizes { get; set; }

        public string Color { get; set; }
        public List<Picture>? Pictures { get; set; }
        public List<ProductLanguage> productLanguages { get; set; }

    }
}
