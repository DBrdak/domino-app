using Newtonsoft.Json;

namespace OnlineShop.ShoppingCart.API.Entities
{
    public class ShoppingCart
    {
        public string ShoppingCartId { get; set; }
        public List<ShoppingCartItem> Items { get; set; }

        public decimal TotalPrice => Items.Select(i =>
            i.Unit == "kg" ?
            i.Price * i.Quantity :
            i.Price * i.Quantity * i.KgPerPcs)
            .Sum();

        public ShoppingCart(string cartId, List<ShoppingCartItem> items)
        {
            ShoppingCartId = cartId;
            Items = items;
        }

        public ShoppingCart(string cartId)
        {
            ShoppingCartId = cartId;
            Items = new List<ShoppingCartItem>();
        }

        public ShoppingCart()
        {
        }
    }
}