using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stach.Domain.Models.Order_Aggregate
{
    public class Order : Base
    {
        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; }
        public OrderStatus Status { get; set; }
        public Adress ShippingAddress { get; set; }
        public DeliveryMethod DeliveryMethod { get; set; }  // Navigational Property [One]
        public ICollection<OrderItem> Items { get; set; } = new HashSet<OrderItem>(); // Navigational Property [Many]
        public decimal Subtotal { get; set; }

        //[NotMapped]
        //public decimal Total => Subtotal + DeliveryMethod.Cost;
        public decimal GetTotal() => Subtotal + DeliveryMethod.Cost;
    }
}
