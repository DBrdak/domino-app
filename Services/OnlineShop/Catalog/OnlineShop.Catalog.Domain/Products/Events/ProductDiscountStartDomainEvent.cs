using Shared.Domain.Abstractions.Entities;
using Shared.Domain.Money;

namespace OnlineShop.Catalog.Domain.Products.Events
{
    public sealed record ProductDiscountStartDomainEvent(string Id, Money NewPrice) : IDomainEvent
    {
    }
}