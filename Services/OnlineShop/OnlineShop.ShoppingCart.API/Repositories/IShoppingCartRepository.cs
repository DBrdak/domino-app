using EventBus.Messages.Events;
using MassTransit;
using OnlineShop.ShoppingCart.API.Entities;

namespace OnlineShop.ShoppingCart.API.Repositories
{
    public interface IShoppingCartRepository
    {
        Task<Entities.ShoppingCart> GetShoppingCart(string cartId);

        Task<Entities.ShoppingCart> UpdateShoppingCart(Entities.ShoppingCart shoppingCart);

        Task DeleteShoppingCart(string cartId);

        Task<string> Checkout(ShoppingCartCheckout shoppingCartCheckout);
    }
}