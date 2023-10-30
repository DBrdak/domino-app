using Shared.Domain.Abstractions.Entities;

namespace OnlineShop.Order.Domain.OnlineOrders.Events
{
    public sealed record OrderDeletedDomainEvent(string ShopId, string OrderId) : IDomainEvent
    {
    }
}
