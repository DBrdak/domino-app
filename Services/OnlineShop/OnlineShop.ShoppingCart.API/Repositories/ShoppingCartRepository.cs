using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using OnlineShop.ShoppingCart.API.Entities;

namespace OnlineShop.ShoppingCart.API.Repositories
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly IDistributedCache _cache;

        public ShoppingCartRepository(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task<Entities.ShoppingCart> GetShoppingCart(string cartId)
        {
            var cart = await _cache.GetStringAsync(cartId);

            if (string.IsNullOrWhiteSpace(cart))
                return new(cartId);

            return JsonConvert.DeserializeObject<Entities.ShoppingCart>(cart);
        }

        public async Task<Entities.ShoppingCart> UpdateShoppingCart(Entities.ShoppingCart shoppingCart)
        {
            await _cache.SetStringAsync(shoppingCart.ShoppingCartId,
                JsonConvert.SerializeObject(shoppingCart));

            return await GetShoppingCart(shoppingCart.ShoppingCartId);
        }

        public async Task DeleteShoppingCart(string cartId)
        {
            await _cache.RemoveAsync(cartId);
        }

        public async Task<bool> Checkout(ShoppingCartCheckout shoppingCartCheckout)
        {
            var cart = await GetShoppingCart(shoppingCartCheckout.ShoppingCartId);

            if (cart is null)
                return false;

            // Here goes EventBus

            return true;
        }
    }
}