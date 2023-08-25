using Shared.Domain.Abstractions;

namespace OnlineShop.Catalog.Domain.Events
{
    public sealed record ProductInStockDomainEvent(string Id) : IDomainEvent
    {
    }
}