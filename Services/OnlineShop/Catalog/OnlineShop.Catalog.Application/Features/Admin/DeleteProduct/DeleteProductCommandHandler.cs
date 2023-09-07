using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineShop.Catalog.Application.Abstractions.Messaging;
using OnlineShop.Catalog.Domain;
using OnlineShop.Catalog.Domain.Products;
using Shared.Domain.ResponseTypes;

namespace OnlineShop.Catalog.Application.Features.Admin.DeleteProduct
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
            await _repository.Delete(request.ProductId, cancellationToken);

            return Result.Success();
        }
    }
}