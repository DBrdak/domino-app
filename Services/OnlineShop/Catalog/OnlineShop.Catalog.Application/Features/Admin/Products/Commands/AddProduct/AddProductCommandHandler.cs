﻿using OnlineShop.Catalog.Domain.Products;
using Shared.Domain.Abstractions.Messaging;
using Shared.Domain.Errors;
using Shared.Domain.ResponseTypes;

namespace OnlineShop.Catalog.Application.Features.Admin.Products.Commands.AddProduct
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
                return Result.Failure<Product>(Error.TaskFailed("Nie udało się stworzyć produktu"));
            }

            return result;
        }
    }
}