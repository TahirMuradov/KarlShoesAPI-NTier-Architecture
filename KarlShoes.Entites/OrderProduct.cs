using KarlShoes.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarlShoes.Entites
{
    public class OrderProduct: BaseEntity, IEntity
    {
   
        public Guid OrderId { get; set; }
        public Order Order { get; set; }
        public Guid ProductID { get; set; }
        public Product Product { get; set; }
    }
}
