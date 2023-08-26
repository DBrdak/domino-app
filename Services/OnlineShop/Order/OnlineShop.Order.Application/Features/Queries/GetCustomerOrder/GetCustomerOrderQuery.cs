using OnlineShop.Order.Application.Abstractions.Messaging;
using OnlineShop.Order.Application.Core;
using OnlineShop.Order.Domain;
using OnlineShop.Order.Domain.OnlineOrders;

namespace OnlineShop.Order.Application.Features.Queries.GetCustomerOrder
{
    public record GetCustomerOrderQuery(string PhoneNumber, string OrderId) : IQuery<OnlineOrder>;
}