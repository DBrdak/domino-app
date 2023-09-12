using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineShop.Catalog.Application.Abstractions.Messaging;
using OnlineShop.Catalog.Domain;
using OnlineShop.Catalog.Domain.Products;
using Shared.Domain.Abstractions;
using Shared.Domain.Errors;
using Shared.Domain.Photo;
using Shared.Domain.ResponseTypes;

namespace OnlineShop.Catalog.Application.Features.Admin.Products.AddProduct
{
    internal sealed class AddProductCommandHandler : ICommandHandler<AddProductCommand, Product>
    {
        private readonly IProductRepository _productRepository;

        public AddProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Result<Product>> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            var result = await _productRepository.Add(request.Values, request.PhotoFile, cancellationToken);

            if (result is null)
            {
                return Result.Failure<Product>(Error.TaskFailed("Product create proccess failed"));
            }

            return result;
        }
    }
}