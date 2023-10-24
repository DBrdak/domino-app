using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineShop.Catalog.Domain.PriceLists;
using Shared.Domain.Abstractions.Messaging;
using Shared.Domain.ResponseTypes;

namespace OnlineShop.Catalog.Application.Features.Admin.PriceLists.UploadPriceListAsExcel
{
    internal sealed class UploadPriceListSpreadsheetCommandHandler : ICommandHandler<UploadPriceListSpreadsheetCommand>
    {
        private readonly IPriceListRepository _priceListRepository;

        public UploadPriceListSpreadsheetCommandHandler(IPriceListRepository priceListRepository)
        {
            _priceListRepository = priceListRepository;
        }

        public Task<Result> Handle(UploadPriceListSpreadsheetCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

    }
}
