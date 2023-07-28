using System.ComponentModel.DataAnnotations;
using OnlineShop.ShoppingCart.API.Models;

namespace OnlineShop.ShoppingCart.API.Entities
{
    public class ShoppingCartCheckout
    {
        // Shopping Cart Info

        public string ShoppingCartId { get; set; }
        public decimal TotalPrice { get; set; }
        public List<ShoppingCartItem> Items { get; set; }

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