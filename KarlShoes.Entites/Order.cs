using KarlShoes.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarlShoes.Entites
{
    public class Order:IEntity
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string? OrderNumber { get; set; }
        public string? Message { get; set; }
        public string PaymentMethodId { get; set; }
        public string ShippingMethodId { get; set; }
        public string? OrderPDfUrl { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public List<OrderProduct> Products { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
