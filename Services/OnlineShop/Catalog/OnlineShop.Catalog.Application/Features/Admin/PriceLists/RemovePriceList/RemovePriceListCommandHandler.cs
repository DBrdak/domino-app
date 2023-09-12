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

namespace OnlineShop.Catalog.Application.Features.Admin.PriceLists.RemovePriceList
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
                Result.Failure(Error.TaskFailed($"Problem while removing price list with ID {request.PriceListId}"));
        }
    }
}