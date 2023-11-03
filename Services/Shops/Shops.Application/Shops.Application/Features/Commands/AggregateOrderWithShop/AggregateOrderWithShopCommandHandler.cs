using Shared.Domain.Abstractions.Messaging;
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

            var updatedShop = shops.Single(s => s.Id == request.ShopId);

            updatedShop.AddOrder(request.OrderId);

            await _shopRepository.UpdateShop(updatedShop, cancellationToken);

            return Result.Success();
        }
    }
}
