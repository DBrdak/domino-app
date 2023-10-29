using OnlineShop.Catalog.Domain.PriceLists.Events;
using OnlineShop.Catalog.Domain.Products;
using Shared.Domain.Abstractions.Entities;

namespace OnlineShop.Catalog.Application.Features.Admin.PriceLists.Commands.RemoveLineItem
{
    internal sealed class LineItemDeletedDomainEventHandler : IDomainEventHandler<LineItemDeletedDomainEvent>
    {
        private readonly IProductRepository _productRepository;

        public LineItemDeletedDomainEventHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task Handle(LineItemDeletedDomainEvent notification, CancellationToken cancellationToken)
        {
            var isSuccess = await _productRepository.Delete(notification.ProductId, cancellationToken);

            if (!isSuccess)
            {
                throw new ApplicationException($"Delete operation for product with ID {notification.ProductId} failed");
            }
        }
    }
}