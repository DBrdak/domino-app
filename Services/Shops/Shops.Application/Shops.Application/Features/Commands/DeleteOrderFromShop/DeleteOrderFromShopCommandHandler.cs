using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Domain.Abstractions.Messaging;
using Shared.Domain.ResponseTypes;
using Shops.Domain.Abstractions;

namespace Shops.Application.Features.Commands.DeleteOrderFromShop
{
    internal sealed class DeleteOrderFromShopCommandHandler : ICommandHandler<DeleteOrderFromShopCommand>
    {
        private readonly IShopRepository _shopRepository;

        public DeleteOrderFromShopCommandHandler(IShopRepository shopRepository)
        {
            _shopRepository = shopRepository;
        }

        public async Task<Result> Handle(DeleteOrderFromShopCommand request, CancellationToken cancellationToken)
        {
            var shops = await _shopRepository.GetShops(cancellationToken);

            var shop = shops.Single(s => s.Id == request.ShopId);

            shop.RemoveOrder(request.OrderId);

            await _shopRepository.UpdateShop(shop, cancellationToken);

            return Result.Success();
        }
    }
}
