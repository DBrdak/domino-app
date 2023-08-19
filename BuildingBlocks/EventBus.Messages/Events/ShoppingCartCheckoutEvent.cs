using EventBus.Messages.Common;
using System.ComponentModel.DataAnnotations;
using Shared.Domain.DateTimeRange;
using Shared.Domain.Location;

namespace EventBus.Messages.Events
{
    public class ShoppingCartCheckoutEvent
    {
        // Shopping Cart Info

        public string ShoppingCartId { get; set; }
        public decimal TotalPrice { get; set; }
        public List<ShoppingCartItem> Items { get; set; }
        public string Currency { get; set; }

        // Personal Info

        [RegularExpression("^[0-9]{9}$")]
        public string PhoneNumber { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        // Delivery Info

        public Location DeliveryLocation { get; set; }
        public DateTimeRange DeliveryDate { get; set; }
    }
}