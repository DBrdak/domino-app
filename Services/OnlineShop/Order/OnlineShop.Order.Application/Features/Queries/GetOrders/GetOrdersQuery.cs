using OnlineShop.Order.Domain.OnlineOrders;
using Shared.Domain.Abstractions.Messaging;

namespace OnlineShop.Order.Application.Features.Queries.GetOrders
{
    public sealed record GetOrdersQuery() : ICommand<List<OnlineOrder>>;
}