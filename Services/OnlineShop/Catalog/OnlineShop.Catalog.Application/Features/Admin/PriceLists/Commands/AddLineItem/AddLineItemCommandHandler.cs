using OnlineShop.Catalog.Domain.PriceLists;
using Shared.Domain.Abstractions.Messaging;
using Shared.Domain.Errors;
using Shared.Domain.ResponseTypes;

namespace OnlineShop.Catalog.Application.Features.Admin.PriceLists.Commands.AddLineItem
{
    internal sealed class AddLineItemCommandHandler : ICommandHandler<AddLineItemCommand>
    {
        private readonly IPriceListRepository _priceListRepository;

        public AddLineItemCommandHandler(IPriceListRepository priceListRepository)
        {
            _priceListRepository = priceListRepository;
        }

        public async Task<Result> Handle(AddLineItemCommand request, CancellationToken cancellationToken)
        {
            var isSuccess = await _priceListRepository.AddLineItem(
                request.PriceListId,
                new LineItem(request.Name, request.Price),
                cancellationToken);

            return isSuccess ?
                Result.Success() :
                Result.Failure(Error.InvalidRequest(
                    $"Błąd podczas dodawania nowej pozycji o nazwie {request.Name} do cennika z ID {request.PriceListId}"
                    ));
        }
    }
}