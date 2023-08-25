using Shared.Domain.Abstractions;

namespace OnlineShop.Catalog.Domain.Events
{
    public sealed record ProductOutOfStockDomainEvent(string Id) : IDomainEvent
    {
    }
}