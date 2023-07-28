using System.ComponentModel.DataAnnotations;
using OnlineShop.Order.Domain.Common;

namespace OnlineShop.Order.Domain.Entities
{
    public class OnlineOrder
    {
        // Shopping Cart Info

        public decimal TotalPrice { get; set; }
        public List<OrderItem> Items { get; set; }

        // Personal Info

        [RegularExpression("^[0-9]{9}$")]
        public string PhoneNumber { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        // Delivery Info

        public Location DeliveryLocation { get; set; }
        public DateTimeRange DeliveryDate { get; set; }

        // Order Info

        public string OrderId { get; set; } = GenerateId();
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public bool IsCanceled { get; set; } = false; // Customer
        public string Status { get; set; } // Admin
        public bool IsConfirmed { get; set; } = false; // Admin
        public bool IsRejected { get; set; } = false; // Admin

        private static string GenerateId()
        {
            return Ulid.NewUlid(DateTimeOffset.UtcNow).ToString().Substring(4, 12);
        }
    }
}