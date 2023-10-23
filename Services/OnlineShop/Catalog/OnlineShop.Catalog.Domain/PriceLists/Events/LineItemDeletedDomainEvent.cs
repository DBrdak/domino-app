using Shared.Domain.Abstractions.Entities;

namespace OnlineShop.Catalog.Domain.PriceLists.Events
{
    public sealed record LineItemDeletedDomainEvent(string ProductId) : IDomainEvent;
}