using OnlineShop.Order.Domain.OnlineOrders;
using Shared.Domain.Abstractions.Messaging;

namespace OnlineShop.Order.Application.Features.Queries.GetCustomerOrder
{
    public record GetCustomerOrderQuery(string PhoneNumber, string OrderId) : IQuery<OnlineOrder>;
}