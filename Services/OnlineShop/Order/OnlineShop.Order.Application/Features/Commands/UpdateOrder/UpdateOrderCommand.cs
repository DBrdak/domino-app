using OnlineShop.Order.Domain.OnlineOrders;
using OnlineShop.Order.Domain.OrderItems;
using Shared.Domain.Abstractions.Messaging;

namespace OnlineShop.Order.Application.Features.Commands.UpdateOrder
{
    public sealed record UpdateOrderCommand(string OrderId, string? Status, string? SmsMessage, IEnumerable<OrderItem>? ModifiedOrderItems, bool? IsPrinted) : ICommand;
}