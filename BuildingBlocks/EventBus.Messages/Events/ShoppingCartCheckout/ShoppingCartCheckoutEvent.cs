using Shared.Domain.DateTimeRange;
using Shared.Domain.Location;
using Shared.Domain.Money;

namespace IntegrationEvents.Domain.Events.ShoppingCartCheckout
{
    public sealed record ShoppingCartCheckoutEvent(
        string ShoppingCartId,
        Money TotalPrice,
        List<ShoppingCartCheckoutItem> Items,
        string PhoneNumber,
        string FirstName,
        string LastName,
        Location DeliveryLocation,
        DateTimeRange DeliveryDate);
}