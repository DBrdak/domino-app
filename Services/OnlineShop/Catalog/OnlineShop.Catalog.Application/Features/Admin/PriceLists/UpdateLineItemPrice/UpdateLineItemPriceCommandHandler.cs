using OnlineShop.Catalog.Domain.PriceLists;
using Shared.Domain.Abstractions.Messaging;
using Shared.Domain.Errors;
using Shared.Domain.ResponseTypes;

namespace OnlineShop.Catalog.Application.Features.Admin.PriceLists.UpdateLineItemPrice
{
    internal sealed class UpdateLineItemPriceCommandHandler : ICommandHandler<UpdateLineItemPriceCommand, PriceList>
    {
        private readonly IPriceListRepository _priceListRepository;

        public UpdateLineItemPriceCommandHandler(IPriceListRepository priceListRepository)
        {
            _priceListRepository = priceListRepository;
        }

        public async Task<Result<PriceList>> Handle(UpdateLineItemPriceCommand request, CancellationToken cancellationToken)
        {
            var priceListBeforeUpdate = await _priceListRepository.UpdateLineItemPrice(
                request.PriceListId,
                request.LineItemName,
                request.NewPrice,
                cancellationToken);

            return priceListBeforeUpdate is not null ?
                    Result.Success(priceListBeforeUpdate) :
                    Result.Failure<PriceList>(Error.InvalidRequest(
                        $"Cannot update price of line item named {request.LineItemName} to value {request.NewPrice} in price list with ID {request.PriceListId}"));
        }
    }
}