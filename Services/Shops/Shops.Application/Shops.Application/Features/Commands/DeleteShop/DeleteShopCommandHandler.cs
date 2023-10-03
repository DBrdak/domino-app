using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Domain.Abstractions.Messaging;
using Shared.Domain.Errors;
using Shared.Domain.ResponseTypes;
using Shops.Domain.Abstractions;

namespace Shops.Application.Features.Commands.DeleteShop
{
    internal sealed class DeleteShopCommandHandler : ICommandHandler<DeleteShopCommand>
    {
        private readonly IShopRepository _shopRepository;

        public DeleteShopCommandHandler(IShopRepository shopRepository)
        {
            _shopRepository = shopRepository;
        }

        public async Task<Result> Handle(DeleteShopCommand request, CancellationToken cancellationToken)
        {
            var isSuccess = await _shopRepository.DeleteShop(request.ShopId, cancellationToken);

            return isSuccess ?
                    Result.Success() :
                    Result.Failure(Error.InvalidRequest($"Cannot delete shop with ID {request.ShopId}"));
        }
    }
}