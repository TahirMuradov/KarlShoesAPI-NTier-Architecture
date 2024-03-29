using KarlShoes.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarlShoes.Entites
{
    public class Size: IEntity
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public int NumberSize { get; set; }
        public List<ProductSize> ProductSize { get; set; }
    }
}
