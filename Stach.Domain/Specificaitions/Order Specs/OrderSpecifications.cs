using Stach.Domain.Models.Order_Aggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stach.Domain.Specificaitions.Order_Specs
{
    public class OrderSpecifications : BaseSpecifications<Order>
    {
        public OrderSpecifications(string buyerEmail)
            : base(Order => Order.BuyerEmail == buyerEmail)
        {
            AllIncludes();

            AddOrderByDesc(O => O.OrderDate);
        }

        public OrderSpecifications(int orderId, string buyerEmail)
            : base(O => O.Id == orderId && O.BuyerEmail == buyerEmail)
        {
            AllIncludes();
        }

        private void AllIncludes()
        {
            Includes.Add(O => O.DeliveryMethod);
            Includes.Add(O => O.Items);
        }
    }
}
