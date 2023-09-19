using Shared.Domain.Money;
using Shared.Domain.Quantity;

namespace EventBus.Domain.Common
{
    public sealed record ShoppingCartCheckoutItem(
        Quantity Quantity,
        Money Price,
        Money TotalValue,
        string ProductId,
        string ProductName);
}