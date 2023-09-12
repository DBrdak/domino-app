using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using OnlineShop.Catalog.Domain.PriceLists.Events;
using OnlineShop.Catalog.Domain.Products;
using Shared.Domain.Abstractions;

namespace OnlineShop.Catalog.Application.Features.DomainEventHandlers
{
    internal sealed class LineItemPriceUpdatedDomainEventHandler : IDomainEventHandler<LineItemPriceUpdatedDomainEvent>
    {
        private readonly IProductRepository _productRepository;

        public LineItemPriceUpdatedDomainEventHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task Handle(LineItemPriceUpdatedDomainEvent notification, CancellationToken cancellationToken)
        {
            var isSuccess = await _productRepository.RefreshPrice(notification.ProductId, cancellationToken);

            if (!isSuccess)
            {
                throw new ApplicationException($"Price update for product with ID {notification.ProductId} failed");
            }
        }
    }
}