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

namespace OnlineShop.Catalog.Application.Features.Admin.Products.UpdateProduct
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
                    return Result.Failure<Product>(Error.TaskFailed("Photo upload failed"));
                }

                request.NewValues.UpdatePhoto(uploadResult.PhotoUrl);
            }

            var updatedProduct = await _productRepository.UpdateProduct(request.NewValues, cancellationToken);

            if (updatedProduct is null)
            {
                return Result.Failure<Product>(Error.NullValue);
            }

            return Result.Success(updatedProduct);
        }
    }
}