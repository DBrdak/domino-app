using OnlineShop.ShoppingCart.API.Controllers.Requests;
using Shared.Domain.ResponseTypes;

namespace OnlineShop.ShoppingCart.API.Repositories
{
    public interface IShoppingCartRepository
    {
        Task<Entities.ShoppingCart> GetShoppingCart(string cartId);

        Task<Entities.ShoppingCart> UpdateShoppingCart(Entities.ShoppingCart shoppingCart);

        Task DeleteShoppingCart(string cartId);

        Task<Result<string>> Checkout(ShoppingCartCheckoutRequest request);
    }
}