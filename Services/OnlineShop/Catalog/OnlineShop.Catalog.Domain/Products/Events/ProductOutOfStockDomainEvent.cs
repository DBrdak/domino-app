using Shared.Domain.Abstractions.Entities;

namespace OnlineShop.Catalog.Domain.Products.Events
{
    public sealed record ProductOutOfStockDomainEvent(string Id) : IDomainEvent
    {
    }
}