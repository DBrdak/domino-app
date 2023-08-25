using Shared.Domain.Abstractions;
using Shared.Domain.Money;

namespace OnlineShop.Catalog.Domain.Events
{
    public sealed record ProductDiscountEndDomainEvent(string Id, Money NewPrice) : IDomainEvent
    {
    }
}