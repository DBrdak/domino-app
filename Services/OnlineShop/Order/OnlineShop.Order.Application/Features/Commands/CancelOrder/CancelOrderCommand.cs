using OnlineShop.Order.Application.Abstractions.Messaging;
using OnlineShop.Order.Application.Core;
using OnlineShop.Order.Domain;
using OnlineShop.Order.Domain.OnlineOrders;
using Shared.Domain.ResponseTypes;

namespace OnlineShop.Order.Application.Features.Commands.CancelOrder
{
    public record CancelOrderCommand(OnlineOrder Order) : ICommand<bool>;
}