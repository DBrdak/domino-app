using IntegrationEvents.Domain.Events.ShoppingCartCheckout;
using IntegrationEvents.Domain.Results;
using MassTransit;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using OnlineShop.ShoppingCart.API.Controllers.Requests;
using Shared.Domain.ResponseTypes;

namespace OnlineShop.ShoppingCart.API.Repositories
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly IDistributedCache _cache;
        private readonly IBus _bus;

        public ShoppingCartRepository(IDistributedCache cache, IBus bus)
        {
            _cache = cache;
            _bus = bus;
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
            await _cache.RemoveAsync(shoppingCart.ShoppingCartId);

            await _cache.SetStringAsync(shoppingCart.ShoppingCartId,
                JsonConvert.SerializeObject(shoppingCart));

            return await GetShoppingCart(shoppingCart.ShoppingCartId);
        }

        public async Task DeleteShoppingCart(string cartId)
        {
            await _cache.RemoveAsync(cartId);
        }

        public async Task<Result<string>> Checkout(ShoppingCartCheckoutRequest request)
        {
            var cart = await GetShoppingCart(request.ShoppingCart.ShoppingCartId);

            if (cart is null)
                return null;

            var eventMessage = new ShoppingCartCheckoutEvent(
                request.ShoppingCart.ShoppingCartId,
                request.ShoppingCart.TotalPrice,
                request.ShoppingCart.Items.Select(i => new ShoppingCartCheckoutItem(
                    i.Quantity, i.Price, i.TotalValue, i.ProductId, i.ProductName)).ToList(),
                request.PhoneNumber,
                request.FirstName,
                request.LastName,
                request.DeliveryLocation,
                request.DeliveryDate);
            
            var client = _bus.CreateRequestClient<ShoppingCartCheckoutEvent>();
            var responseFromOrder = await client.GetResponse<CheckoutOrderResult>(eventMessage);

            await DeleteShoppingCart(request.ShoppingCart.ShoppingCartId);

            return responseFromOrder.Message.OrderId;
        }
    }
}