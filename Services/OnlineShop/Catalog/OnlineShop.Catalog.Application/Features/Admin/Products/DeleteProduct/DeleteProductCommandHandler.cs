﻿using OnlineShop.Catalog.Domain.Products;
using Shared.Domain.Abstractions.Messaging;
using Shared.Domain.Errors;
using Shared.Domain.ResponseTypes;

namespace OnlineShop.Catalog.Application.Features.Admin.Products.DeleteProduct
{
    internal sealed class DeleteProductCommandHandler : ICommandHandler<DeleteProductCommand>
    {
        private readonly IProductRepository _repository;

        public DeleteProductCommandHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var isSuccess = await _repository.Delete(request.ProductId, cancellationToken);

            if (!isSuccess)
            {
                return Result.Failure(Error.TaskFailed($"Problem while deleting product of ID {request.ProductId}"));
            }

            return Result.Success();
        }
    }
}