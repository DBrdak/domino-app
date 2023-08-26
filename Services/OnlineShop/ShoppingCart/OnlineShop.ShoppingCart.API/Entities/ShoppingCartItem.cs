using Shared.Domain.Money;
using Shared.Domain.Photo;
using Shared.Domain.Quantity;

namespace OnlineShop.ShoppingCart.API.Entities
{
    public sealed record ShoppingCartItem(
    Quantity Quantity,
    Money Price,
    Money TotalValue,
    string ProductId,
    string ProductName,
    Photo ProductImage);
}