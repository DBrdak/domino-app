using Shared.Domain.Abstractions.Entities;

namespace OnlineShop.Catalog.Domain.PriceLists.Events
{
    public sealed record LineItemPriceUpdatedDomainEvent(string ProductId) : IDomainEvent;
    //TODO Updating ProductPrice
}