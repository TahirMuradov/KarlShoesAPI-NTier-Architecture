using KarlShoes.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarlShoes.Entites
{
    public class Category:IEntity
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsFeatured { get; set; }
        public List<SubCategory>?SubCategory { get; set; }
        public List<CategoryProduct>? CategoryProducts { get; set; }
        public List<CategoryLanguage> CategoryLanguages { get; set; }
    }
}
