using Shared.Domain.Abstractions.Entities;
using Shared.Domain.Money;

namespace OnlineShop.Catalog.Domain.Products.Events
{
    public sealed record ProductDiscountEndDomainEvent(string Id, Money NewPrice) : IDomainEvent
    {
    }
}