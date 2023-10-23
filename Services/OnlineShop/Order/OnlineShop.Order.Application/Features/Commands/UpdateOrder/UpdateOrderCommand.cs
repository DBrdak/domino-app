using OnlineShop.Order.Domain.OnlineOrders;
using Shared.Domain.Abstractions.Messaging;

namespace OnlineShop.Order.Application.Features.Commands.UpdateOrder
{
    public sealed record UpdateOrderCommand(string OrderId, string Status, string? SmsMessage, OnlineOrder? ModifiedOrder) : ICommand;
}