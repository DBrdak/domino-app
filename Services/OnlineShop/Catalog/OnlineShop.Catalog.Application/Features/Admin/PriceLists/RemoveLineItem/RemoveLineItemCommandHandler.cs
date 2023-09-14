using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineShop.Catalog.Application.Abstractions.Messaging;
using OnlineShop.Catalog.Domain.PriceLists;
using Shared.Domain.Abstractions;
using Shared.Domain.Errors;
using Shared.Domain.ResponseTypes;

namespace OnlineShop.Catalog.Application.Features.Admin.PriceLists.RemoveLineItem
{
    internal sealed class RemoveLineItemCommandHandler : ICommandHandler<RemoveLineItemCommand, PriceList>
    {
        private readonly IPriceListRepository _priceListRepository;

        public RemoveLineItemCommandHandler(IPriceListRepository priceListRepository)
        {
            _priceListRepository = priceListRepository;
        }

        public async Task<Result<PriceList>> Handle(RemoveLineItemCommand request, CancellationToken cancellationToken)
        {
            var modifiedPriceList = await _priceListRepository.RemoveLineItem(
                request.PriceListId,
                request.LineItemName,
            cancellationToken);

            return modifiedPriceList is not null ?
                Result.Success(modifiedPriceList) :
                    Result.Failure<PriceList>(Error.InvalidRequest(
                        $"Problem while removing line item of name {request.LineItemName} from price list with ID {request.PriceListId}"));
        }
    }
}