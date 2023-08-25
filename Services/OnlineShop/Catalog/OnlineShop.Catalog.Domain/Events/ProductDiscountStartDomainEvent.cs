using Shared.Domain.Abstractions;
using Shared.Domain.Money;

namespace OnlineShop.Catalog.Domain.Events
{
    public sealed record ProductDiscountStartDomainEvent(string Id, Money NewPrice) : IDomainEvent
    {
    }
}