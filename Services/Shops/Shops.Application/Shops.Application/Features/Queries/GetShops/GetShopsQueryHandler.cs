using Shared.Domain.Abstractions.Messaging;
using Shared.Domain.ResponseTypes;
using Shops.Domain.Shops;

namespace Shops.Application.Features.Queries.GetShops
{
    internal sealed class GetShopsQueryHandler : IQueryHandler<GetShopsQuery, List<Shop>>
    {
        private readonly IShopRepository _shopRepository;

        public GetShopsQueryHandler(IShopRepository shopRepository)
        {
            _shopRepository = shopRepository;
        }

        public async Task<Result<List<Shop>>> Handle(GetShopsQuery request, CancellationToken cancellationToken)
        {
            return await _shopRepository.GetShops(cancellationToken);
        }
    }
}