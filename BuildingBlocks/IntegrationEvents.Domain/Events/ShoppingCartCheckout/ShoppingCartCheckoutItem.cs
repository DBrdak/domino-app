using Shared.Domain.Money;
using Shared.Domain.Quantity;

namespace IntegrationEvents.Domain.Events.ShoppingCartCheckout
{
    public sealed record ShoppingCartCheckoutItem(
        Quantity Quantity,
        Money Price,
        Money TotalValue,
        string ProductId,
        string ProductName);
}