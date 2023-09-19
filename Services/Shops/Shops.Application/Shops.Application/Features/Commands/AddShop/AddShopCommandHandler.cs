using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Domain.Abstractions.Messaging;
using Shared.Domain.Errors;
using Shared.Domain.ResponseTypes;
using Shops.Domain.Shops;

namespace Shops.Application.Features.Commands.AddShop
{
    internal sealed class AddShopCommandHandler : ICommandHandler<AddShopCommand, Shop>
    {
        private readonly IShopRepository _shopRepository;

        public AddShopCommandHandler(IShopRepository shopRepository)
        {
            _shopRepository = shopRepository;
        }

        public async Task<Result<Shop>> Handle(AddShopCommand request, CancellationToken cancellationToken)
        {
            var newShop = Shop.Create(
                //TODO request values
                );

            var result = await _shopRepository.AddShop(newShop, cancellationToken);

            return result ??
                   Result.Failure<Shop>(Error.InvalidRequest("Cannot create shop"));
        }
    }
}