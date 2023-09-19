using Shared.Domain.DateTimeRange;
using Shared.Domain.Location;

namespace OnlineShop.ShoppingCart.API.Controllers.Requests
{
    public sealed record ShoppingCartCheckoutRequest(
        Entities.ShoppingCart ShoppingCart,
        string PhoneNumber,
        string FirstName,
        string LastName,
        Location DeliveryLocation,
        DateTimeRange DeliveryDate);
}