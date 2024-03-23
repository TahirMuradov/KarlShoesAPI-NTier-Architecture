using KarlShoes.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarlShoes.Entites
{
    public class ProductLanguage:IEntity
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public string SeoUrl { get; set; }
        public string LangCode { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
    }
}
