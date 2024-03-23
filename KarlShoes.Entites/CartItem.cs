using KarlShoes.Core.Entities;

namespace KarlShoes.Entites
{
    public class CartItem:IEntity
    {
        public Guid Id { get; set; }
        public Guid CartId { get; set; }
        public Cart Cart { get; set; }
        public Guid ItemId { get; set; }
        public Item Item { get; set; }
    }
}
