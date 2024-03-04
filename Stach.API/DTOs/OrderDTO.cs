using System.ComponentModel.DataAnnotations;

namespace Stach.API.DTOs
{
    public class OrderDTO
    {
        [Required]
        public string BuyerEmail { get; set; }
        [Required]
        public string BasketId { get; set; }
        [Required]
        public int DeliveryMethodId { get; set; }
        public AddressDTO ShippingAddress { get; set; }
    }
}
