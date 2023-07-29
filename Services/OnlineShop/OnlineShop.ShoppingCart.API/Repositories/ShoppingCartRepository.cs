﻿using AutoMapper;
using EventBus.Messages.Events;
using MassTransit;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using OnlineShop.ShoppingCart.API.Entities;
using OnlineShop.ShoppingCart.API.EventBusConsumer;

namespace OnlineShop.ShoppingCart.API.Repositories
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly IDistributedCache _cache;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IMapper _mapper;

        public ShoppingCartRepository(IDistributedCache cache, IPublishEndpoint publishEndpoint, IMapper mapper)
        {
            _cache = cache;
            _publishEndpoint = publishEndpoint;
            _mapper = mapper;
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

        public async Task<string> Checkout(ShoppingCartCheckout shoppingCartCheckout)
        {
            var cart = await GetShoppingCart(shoppingCartCheckout.ShoppingCartId);

            if (cart is null)
                return null;

            var eventMessage = _mapper.Map<ShoppingCartCheckoutEvent>(shoppingCartCheckout);

            await _publishEndpoint.Publish(eventMessage);

            Task.WaitAll(CheckoutResultConsumer.GetCheckoutResult());

            var result = await CheckoutResultConsumer.GetCheckoutResult();

            if (!result.IsSuccess)
                return result.Message;

            await DeleteShoppingCart(shoppingCartCheckout.ShoppingCartId);

            return result.Message;
        }
    }
}