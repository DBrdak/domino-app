using Shared.Domain.Abstractions.Messaging;

namespace OnlineShop.Order.Application.Features.Commands.CancelOrder
{
    public record CancelOrderCommand(string OrderId) : ICommand<bool>;
}