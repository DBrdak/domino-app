using Shared.Domain.Abstractions;

namespace OnlineShop.Catalog.Domain.Products.Events
{
    public sealed record ProductOutOfStockDomainEvent(string Id) : IDomainEvent
    {
    }
}