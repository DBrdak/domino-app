using Shared.Domain.Abstractions.Entities;

namespace OnlineShop.Order.Domain.OnlineOrders.Events
{
    public sealed record OrderCreatedDomainEvent(string OrderId, string PhoneNumber) : IDomainEvent
    {
        //TODO Reject i Accept eventy
    }
}