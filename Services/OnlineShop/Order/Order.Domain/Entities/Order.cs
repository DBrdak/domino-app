using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Order.Domain.Common;

namespace Order.Domain.Entities
{
    public class Order
    {
        // Shopping Cart Info

        public decimal TotalPrice { get; set; }
        public List<OrderItem> Items { get; set; }

        // Personal Info

        public PhoneNumber PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        // Delivery Info

        public Location DeliveryLocation { get; set; }
        public string ShopId { get; set; }
        public DateTimeRange DeliveryDate { get; set; }

        // Order Info

        public string OrderId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Status { get; set; }
    }
}