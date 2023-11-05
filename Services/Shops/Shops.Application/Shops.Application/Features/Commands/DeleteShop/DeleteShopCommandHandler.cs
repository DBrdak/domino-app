using Shared.Domain.Abstractions.Messaging;
using Shared.Domain.Errors;
using Shared.Domain.ResponseTypes;
using Shops.Domain.Shops;

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
                    Result.Failure(Error.InvalidRequest($"Nie można usunąć sklepu o ID: {request.ShopId}"));
        }
    }
}