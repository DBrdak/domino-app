using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Domain.Abstractions.Messaging;
using Shared.Domain.Errors;
using Shared.Domain.ResponseTypes;
using Shops.Domain.Shops;

namespace Shops.Application.Features.Commands.UpdateShop
{
    internal sealed class UpdateShopCommandHandler : ICommandHandler<UpdateShopCommand, Shop>
    {
        private readonly IShopRepository _shopRepository;

        public UpdateShopCommandHandler(IShopRepository shopRepository)
        {
            _shopRepository = shopRepository;
        }

        public async Task<Result<Shop>> Handle(UpdateShopCommand request, CancellationToken cancellationToken)
        {
            var updatedShop = Shop.Create(
                //TODO request values
                );

            var result = await _shopRepository.UpdateShop(updatedShop, cancellationToken);

            return result ??
                   Result.Failure<Shop>(Error.InvalidRequest($"Cannot update shop named {request.Name}"));
        }
    }
}