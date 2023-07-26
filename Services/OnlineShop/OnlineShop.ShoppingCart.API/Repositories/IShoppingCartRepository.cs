using OnlineShop.ShoppingCart.API.Entities;

namespace OnlineShop.ShoppingCart.API.Repositories
{
    public interface IShoppingCartRepository
    {
        Task<Entities.ShoppingCart> GetShoppingCart(string cartId);

        Task<Entities.ShoppingCart> UpdateShoppingCart(Entities.ShoppingCart shoppingCart);

        Task DeleteShoppingCart(string cartId);

        Task<bool> Checkout(ShoppingCartCheckout shoppingCartCheckout);
    }
}