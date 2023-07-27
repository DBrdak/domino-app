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

        public PhoneNumber PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        // Delivery Info

        public Location DeliveryLocation { get; set; }
        public string ShopId { get; set; }
        public DateTimeRange DeliveryDate { get; set; }
    }
}