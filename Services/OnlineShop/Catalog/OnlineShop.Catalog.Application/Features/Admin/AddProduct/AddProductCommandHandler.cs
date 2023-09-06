using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineShop.Catalog.Application.Abstractions.Messaging;
using OnlineShop.Catalog.Domain;
using Shared.Domain.Errors;
using Shared.Domain.Photo;
using Shared.Domain.ResponseTypes;

namespace OnlineShop.Catalog.Application.Features.Admin.AddProduct
{
    internal sealed class AddProductCommandHandler : ICommandHandler<AddProductCommand, Product>
    {
        private readonly IProductRepository _productRepository;
        private readonly IPhotoRepository _photoRepository;

        public AddProductCommandHandler(IProductRepository productRepository, IPhotoRepository photoRepository)
        {
            _productRepository = productRepository;
            _photoRepository = photoRepository;
        }

        public async Task<Result<Product>> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            var uploadResult = await _photoRepository.UploadPhoto(request.PhotoFile);

            if (uploadResult is null)
            {
                return Result.Failure<Product>(Error.TaskFailed("Photo upload failed"));
            }

            request.Values.AttachImage(uploadResult.PhotoUrl);

            var result = await _productRepository.Add(request.Values, cancellationToken);

            return result;
        }
    }
}