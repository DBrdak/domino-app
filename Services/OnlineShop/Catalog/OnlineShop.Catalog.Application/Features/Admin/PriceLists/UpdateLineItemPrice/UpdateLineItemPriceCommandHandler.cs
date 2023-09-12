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

namespace OnlineShop.Catalog.Application.Features.Admin.PriceLists.UpdateLineItemPrice
{
    internal sealed class UpdateLineItemPriceCommandHandler : ICommandHandler<UpdateLineItemPriceCommand>
    {
        private readonly IPriceListRepository _priceListRepository;

        public UpdateLineItemPriceCommandHandler(IPriceListRepository priceListRepository)
        {
            _priceListRepository = priceListRepository;
        }

        public async Task<Result> Handle(UpdateLineItemPriceCommand request, CancellationToken cancellationToken)
        {
            var isSuccess = await _priceListRepository.UpdateLineItemPrice(
                request.PriceListId,
                request.LineItemName,
                request.NewPrice,
                cancellationToken);

            return isSuccess ?
                    Result.Success() :
                    Result.Failure(Error.InvalidRequest(
                        $"Cannot update price of line item named {request.LineItemName} to value {request.NewPrice} in price list with ID {request.PriceListId}"));
        }
    }
}