using Shared.Domain.Abstractions.Messaging;
using Shared.Domain.Errors;
using Shared.Domain.ResponseTypes;
using Shops.Domain.Shops;

namespace Shops.Application.Features.Commands.AggregateOrderWithShop
{
    internal sealed class AggregateOrderWithShopCommandHandler : ICommandHandler<AggregateOrderWithShopCommand>
    {
        private readonly IShopRepository _shopRepository;

        public AggregateOrderWithShopCommandHandler(IShopRepository shopRepository)
        {
            _shopRepository = shopRepository;
        }

        public async Task<Result> Handle(AggregateOrderWithShopCommand request, CancellationToken cancellationToken)
        {
            var shops = await _shopRepository.GetShops(cancellationToken);

            var updatedShop = shops.SingleOrDefault(s => s.Id == request.ShopId);

            if (updatedShop is null)
            {
                return Result.Failure(Error.InvalidRequest($"Nie znaleziono sklepu z ID: {request.ShopId}"));
            }

            var isOrderAlreadyAssignedToShop = shops.SelectMany(s => s.OrdersId).Any(id => id == request.OrderId);

            if (isOrderAlreadyAssignedToShop)
            {
                return Result.Failure(Error.InvalidRequest($"Zamówienie z ID: {request.OrderId} już jest przypisane do innego sklepu"));
            }

            updatedShop.AddOrder(request.OrderId);

            await _shopRepository.UpdateShop(updatedShop, cancellationToken);

            return Result.Success();
        }
    }
}
