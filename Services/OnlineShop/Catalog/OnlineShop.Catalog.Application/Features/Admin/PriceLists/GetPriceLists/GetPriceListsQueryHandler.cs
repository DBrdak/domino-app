using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineShop.Catalog.Application.Abstractions.Messaging;
using OnlineShop.Catalog.Domain.PriceLists;
using Shared.Domain.ResponseTypes;

namespace OnlineShop.Catalog.Application.Features.Admin.PriceLists.GetPriceLists
{
    internal sealed class GetPriceListsQueryHandler : IQueryHandler<GetPriceListsQuery, List<PriceList>>
    {
        private readonly IPriceListRepository _priceListRepository;

        public GetPriceListsQueryHandler(IPriceListRepository priceListRepository)
        {
            _priceListRepository = priceListRepository;
        }

        public async Task<Result<List<PriceList>>> Handle(GetPriceListsQuery request, CancellationToken cancellationToken) =>
            await _priceListRepository.GetPriceListsAsync(cancellationToken);
    }
}