namespace Entities.DTOs.ShippingMethods
{
    public class AddShippingMethodDTO
    {
      
        public Dictionary<string,string> ShippingContent { get; set; }

        public decimal DeliveryPrice { get; set; }

    }
}
