using OnlineShop.Catalog.Domain.PriceLists.Events;
using OnlineShop.Catalog.Domain.Products;
using Shared.Domain.Abstractions.Entities;

namespace OnlineShop.Catalog.Application.Features.Admin.PriceLists.Commands.UpdateLineItemPrice
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