using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using OnlineShop.Catalog.Domain.PriceLists;
using Shared.Domain.Abstractions.Messaging;
using Shared.Domain.ResponseTypes;

namespace OnlineShop.Catalog.Application.Features.Admin.PriceLists.DownloadPriceListAsExcel
{
    internal sealed class GetPriceListSpreadSheetQueryHandler : IQueryHandler<GetPriceListSpreadSheetQuery, byte[]>
    {
        private readonly IPriceListRepository _priceListRepository;

        public GetPriceListSpreadSheetQueryHandler(IPriceListRepository priceListRepository)
        {
            _priceListRepository = priceListRepository;
        }

        public Task<Result<byte[]>> Handle(GetPriceListSpreadSheetQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
