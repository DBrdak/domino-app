using OnlineShop.Catalog.Domain.PriceLists;
using Shared.Domain.Abstractions.Messaging;
using Shared.Domain.Errors;
using Shared.Domain.ResponseTypes;

namespace OnlineShop.Catalog.Application.Features.Admin.PriceLists.Commands.RemovePriceList
{
    internal class RemovePriceListCommandHandler : ICommandHandler<RemovePriceListCommand>
    {
        private readonly IPriceListRepository _priceListRepository;

        public RemovePriceListCommandHandler(IPriceListRepository priceListRepository)
        {
            _priceListRepository = priceListRepository;
        }

        public async Task<Result> Handle(RemovePriceListCommand request, CancellationToken cancellationToken)
        {
            var isSuccess = await _priceListRepository.RemovePriceList(request.PriceListId, cancellationToken);

            return isSuccess ?
                Result.Success() :
                Result.Failure(Error.TaskFailed($"Błąd podczas usuwania cennika z ID {request.PriceListId}"));
        }
    }
}