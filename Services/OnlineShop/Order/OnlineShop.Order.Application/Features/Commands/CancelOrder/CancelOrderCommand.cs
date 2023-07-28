using OnlineShop.Order.Application.Core;
using OnlineShop.Order.Application.Core.Interfaces;
using OnlineShop.Order.Domain.Entities;

namespace OnlineShop.Order.Application.Features.Commands.CancelOrder
{
    public record CancelOrderCommand(OnlineOrder Order) : ICommand<Result<bool>>;
}