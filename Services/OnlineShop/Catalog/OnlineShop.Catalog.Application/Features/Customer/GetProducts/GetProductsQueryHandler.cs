using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineShop.Catalog.Application.Abstractions.Messaging;
using OnlineShop.Catalog.Domain;
using Shared.Domain.Errors;
using Shared.Domain.ResponseTypes;

namespace OnlineShop.Catalog.Application.Features.Customer.GetProducts
{
    internal sealed class GetProductsQueryHandler : IQueryHandler<GetProductsQuery, PagedList<Product>>
    {
        private readonly IProductRepository _productRepository;

        public GetProductsQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Result<PagedList<Product>>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetProductsAsync(
                request.Page,
                request.SortOrder,
                request.SortBy,
                request.PageSize,
                request.Category,
                request.Subcategory,
                request.SearchPhrase,
                request.MinPrice,
                request.MaxPrice,
                request.IsAvailable,
                request.IsDiscounted,
                cancellationToken);

            return Result.Success(products);
        }
    }
}