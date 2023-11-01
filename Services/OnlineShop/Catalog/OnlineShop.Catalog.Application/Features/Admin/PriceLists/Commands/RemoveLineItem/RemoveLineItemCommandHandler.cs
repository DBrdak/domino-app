using OnlineShop.Catalog.Domain.PriceLists;
using Shared.Domain.Abstractions.Messaging;
using Shared.Domain.Errors;
using Shared.Domain.ResponseTypes;

namespace OnlineShop.Catalog.Application.Features.Admin.PriceLists.Commands.RemoveLineItem
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
                        $"Błąd podczas usuwania pozycji o nazwie {request.LineItemName} z cennika o ID: {request.PriceListId}"));
        }
    }
}