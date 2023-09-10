using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineShop.Catalog.Domain.Products.Events;
using Shared.Domain.Abstractions;

namespace OnlineShop.Catalog.Application.Features.Admin.PriceLists.DomainEventHandlers
{
    internal sealed class ProductInStockDomainEventHandler : IDomainEventHandler<ProductInStockDomainEvent>
    {
        public async Task Handle(ProductInStockDomainEvent notification, CancellationToken cancellationToken)
        {
        }
    }
}