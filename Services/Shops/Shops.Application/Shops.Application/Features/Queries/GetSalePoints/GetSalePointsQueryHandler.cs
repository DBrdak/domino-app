using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Domain.Abstractions.Messaging;
using Shared.Domain.Errors;
using Shared.Domain.ResponseTypes;
using Shops.Domain.Shops;

namespace Shops.Application.Features.Queries.GetSalePoints
{
    internal sealed class GetSalePointsQueryHandler : IQueryHandler<GetSalePointsQuery, List<SalePoint>>
    {
        private readonly IShopRepository _shopRepository;

        public GetSalePointsQueryHandler(IShopRepository shopRepository)
        {
            _shopRepository = shopRepository;
        }

        public async Task<Result<List<SalePoint>>> Handle(GetSalePointsQuery request, CancellationToken cancellationToken)
        {
            return await _shopRepository.GetSalePoints(cancellationToken);
        }
    }
}