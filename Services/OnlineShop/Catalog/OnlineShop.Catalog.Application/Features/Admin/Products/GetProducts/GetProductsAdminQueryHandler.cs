using OnlineShop.Catalog.Domain.Products;
using Shared.Domain.Abstractions.Messaging;
using Shared.Domain.ResponseTypes;

namespace OnlineShop.Catalog.Application.Features.Admin.Products.GetProducts
{
    public sealed class GetProductsAdminQueryHandler : IQueryHandler<GetProductsAdminQuery, List<Product>>
    {
        private readonly IProductRepository _productRepository;

        public GetProductsAdminQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Result<List<Product>>> Handle(GetProductsAdminQuery request, CancellationToken cancellationToken)
        {
            return Result.Success(
                await _productRepository.GetProductsAsync(request.SearchPhrase, cancellationToken)
                );
        }
    }
}