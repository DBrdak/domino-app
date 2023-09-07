using Shared.Domain.Abstractions;

namespace OnlineShop.Catalog.Domain.Products.Events
{
    public sealed record ProductInStockDomainEvent(string Id) : IDomainEvent
    {
    }
}