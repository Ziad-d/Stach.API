using Stach.Domain.Models.Order_Aggregate;

namespace Stach.API.DTOs
{
    public class OrderToReturnDTO
    {
        public int Id { get; set; }
        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; }
        public string Status { get; set; }
        public Address ShippingAddress { get; set; }

        public string DeliveryMethod { get; set; }
        public decimal DeliveryMethodCost { get; set; }

        public ICollection<OrderItemDTO> Items { get; set; } = new HashSet<OrderItemDTO>();

        public decimal Subtotal { get; set; }

        public decimal Total { get; set; }

        public string PaymentIntentId { get; set; } = string.Empty;
    }
}
