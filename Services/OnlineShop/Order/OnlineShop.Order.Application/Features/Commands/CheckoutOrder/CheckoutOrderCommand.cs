using OnlineShop.Order.Application.Abstractions.Messaging;
using OnlineShop.Order.Application.Core;
using OnlineShop.Order.Domain;
using OnlineShop.Order.Domain.OnlineOrders;

namespace OnlineShop.Order.Application.Features.Commands.CheckoutOrder
{
    public sealed record CheckoutOrderCommand(OnlineOrder CheckoutOrder) : ICommand<string>;
}