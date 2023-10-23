using OnlineShop.Catalog.Domain.Products.Events;
using Shared.Domain.Abstractions.Entities;

namespace OnlineShop.Catalog.Application.Features.Admin.Products.UpdateProduct;

internal sealed class ProductOutOfStockDomainEventHandler : IDomainEventHandler<ProductOutOfStockDomainEvent>
{
    public async Task Handle(ProductOutOfStockDomainEvent notification, CancellationToken cancellationToken)
    {
    }
}