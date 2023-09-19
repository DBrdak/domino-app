using OnlineShop.Catalog.Domain.Products.Events;
using Shared.Domain.Abstractions.Entities;

namespace OnlineShop.Catalog.Application.Features.Admin.Products.UpdateProduct
{
    internal sealed class ProductInStockDomainEventHandler : IDomainEventHandler<ProductInStockDomainEvent>
    {
        public async Task Handle(ProductInStockDomainEvent notification, CancellationToken cancellationToken)
        {
        }
    }
}