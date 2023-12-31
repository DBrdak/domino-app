﻿using OnlineShop.Catalog.Domain.Products;
using Shared.Domain.Abstractions.Messaging;
using Shared.Domain.Errors;
using Shared.Domain.Photo;
using Shared.Domain.ResponseTypes;

namespace OnlineShop.Catalog.Application.Features.Admin.Products.Commands.UpdateProduct
{
    internal sealed class UpdateProductCommandHandler : ICommandHandler<UpdateProductCommand, Product>
    {
        private readonly IProductRepository _productRepository;
        private readonly IPhotoRepository _photoRepository;

        public UpdateProductCommandHandler(IProductRepository productRepository, IPhotoRepository photoRepository)
        {
            _productRepository = productRepository;
            _photoRepository = photoRepository;
        }

        public async Task<Result<Product>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            if (request.PhotoFile is not null)
            {
                var uploadResult = await _photoRepository.UploadPhoto(request.PhotoFile, cancellationToken);

                if (uploadResult is null)
                {
                    return Result.Failure<Product>(Error.TaskFailed("Błąd podczas przesyłania zdjęcia"));
                }

                request.NewValues.UpdatePhoto(uploadResult.PhotoUrl);
            }

            var updatedProduct = await _productRepository.UpdateProduct(request.NewValues, cancellationToken);

            if (updatedProduct is null)
            {
                return Result.Failure<Product>(
                    Error.TaskFailed($"Błąd podczas aktualizowania produktu o ID: {request.NewValues.Id}"));
            }

            return Result.Success(updatedProduct);
        }
    }
}