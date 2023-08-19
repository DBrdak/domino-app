using System.ComponentModel.DataAnnotations;
using Shared.Domain.DateTimeRange;
using Shared.Domain.Location;
using Shared.Domain.Money;

namespace OnlineShop.ShoppingCart.API.Entities
{
    public class ShoppingCartCheckout
    {
        // Shopping Cart Info

        public string ShoppingCartId { get; init; }
        public Money TotalPrice { get; init; }
        public List<ShoppingCartItem> Items { get; init; }

        // Personal Info

        [RegularExpression("^[0-9]{9}$")]
        public string PhoneNumber { get; init; }

        public string FirstName { get; init; }
        public string LastName { get; init; }

        // Delivery Info

        public Location DeliveryLocation { get; init; }
        public DateTimeRange DeliveryDate { get; init; }
    }
}