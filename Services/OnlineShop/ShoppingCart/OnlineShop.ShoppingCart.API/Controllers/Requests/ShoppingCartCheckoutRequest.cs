using System.ComponentModel.DataAnnotations;
using OnlineShop.ShoppingCart.API.Entities;
using Shared.Domain.DateTimeRange;
using Shared.Domain.Location;
using Shared.Domain.Money;

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