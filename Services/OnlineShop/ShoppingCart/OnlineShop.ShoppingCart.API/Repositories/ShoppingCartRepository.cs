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
        private readonly ILogger<ShoppingCartRepository> _logger;

        public ShoppingCartRepository(IDistributedCache cache, IBus bus, ILogger<ShoppingCartRepository> logger)
        {
            _cache = cache;
            _bus = bus;
            _logger = logger;
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

            _logger.LogInformation("The shopping cart checkout has started");
            
            var client = _bus.CreateRequestClient<ShoppingCartCheckoutEvent>();
            var responseFromOrder = await client.GetResponse<CheckoutOrderResult>(eventMessage);

            _logger.LogInformation($"Received order ID from order service: {responseFromOrder.Message.OrderId}");

            await DeleteShoppingCart(request.ShoppingCart.ShoppingCartId);

            _logger.LogInformation("The shopping cart checkout has been completed successfully");

            return responseFromOrder.Message.OrderId;
        }
    }
}