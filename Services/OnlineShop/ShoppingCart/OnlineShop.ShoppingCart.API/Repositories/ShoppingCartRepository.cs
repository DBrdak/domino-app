using AutoMapper;
using EventBus.Domain.Common;
using EventBus.Domain.Events;
using MassTransit;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using OnlineShop.ShoppingCart.API.Controllers.Requests;
using OnlineShop.ShoppingCart.API.Entities;
using Shared.Domain.ResponseTypes;
using ShoppingCartItem = EventBus.Domain.Common.ShoppingCartCheckoutItem;

namespace OnlineShop.ShoppingCart.API.Repositories
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly IDistributedCache _cache;
        private readonly IBus _bus;

        public ShoppingCartRepository(IDistributedCache cache, IPublishEndpoint publishEndpoint, IBus bus)
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
            var response = await client.GetResponse<Result<string>>(eventMessage);

            await DeleteShoppingCart(request.ShoppingCart.ShoppingCartId);

            return response.Message.Value;
        }
    }
}