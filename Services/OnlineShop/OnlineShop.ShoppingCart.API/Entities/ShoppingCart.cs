using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Http.Features;
using Newtonsoft.Json;

namespace OnlineShop.ShoppingCart.API.Entities
{
    public class ShoppingCart
    {
        public string ShoppingCartId { get; set; }
        public List<ShoppingCartItem> Items { get; set; }

        public decimal TotalPrice => Items.Select(i => i.TotalValue).Sum();
        public string Currency => Items.Select(i => i.Price.Currency).FirstOrDefault();

        [JsonConstructor]
        public ShoppingCart(string shoppingCartId, List<ShoppingCartItem> items, decimal totalPrice)
        {
            ShoppingCartId = shoppingCartId;
            Items = items.GroupBy(item => new { item.ProductId, item.Unit })
                .Select(group => new ShoppingCartItem
                {
                    ProductId = group.Key.ProductId,
                    ProductName = group.First().ProductName,
                    ProductImage = group.First().ProductImage,
                    Unit = group.Key.Unit,
                    Price = group.First().Price,
                    KgPerPcs = group.First().KgPerPcs,
                    Quantity = group.Sum(item => item.Quantity)
                })
                .ToList();
        }

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