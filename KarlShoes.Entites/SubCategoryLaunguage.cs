using KarlShoes.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarlShoes.Entites
{
    public class SubCategoryLaunguage:IEntity
    {
        public Guid Id { get; set; }
        public string SubcategoryName { get; set; }
        public string LangCode { get; set; }
        public Guid SubCategoryId { get; set; }
        public SubCategory SubCategory { get; set; }
    }
}
