using OnlineShop.Catalog.Domain.PriceLists;
using Shared.Domain.Abstractions.Messaging;
using Shared.Domain.Errors;
using Shared.Domain.ResponseTypes;

namespace OnlineShop.Catalog.Application.Features.Admin.PriceLists.Commands.UploadPriceListAsExcel
{
    internal sealed class UploadPriceListSpreadsheetCommandHandler : ICommandHandler<UploadPriceListSpreadsheetCommand>
    {
        private readonly IPriceListRepository _priceListRepository;

        public UploadPriceListSpreadsheetCommandHandler(IPriceListRepository priceListRepository)
        {
            _priceListRepository = priceListRepository;
        }

        public async Task<Result> Handle(UploadPriceListSpreadsheetCommand request, CancellationToken cancellationToken)
        {
            var result = await _priceListRepository.UploadPriceListFile(request.PriceListId, request.PriceListFile, cancellationToken);

            return result ?
                Result.Success() :
                Result.Failure(Error.TaskFailed($"Error during uploading price list with ID: {request.PriceListId}"));
        }

    }
}
