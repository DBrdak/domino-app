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
    internal sealed class RemoveLineItemCommandHandler : ICommandHandler<RemoveLineItemCommand>
    {
        private readonly IPriceListRepository _priceListRepository;

        public RemoveLineItemCommandHandler(IPriceListRepository priceListRepository)
        {
            _priceListRepository = priceListRepository;
        }

        public async Task<Result> Handle(RemoveLineItemCommand request, CancellationToken cancellationToken)
        {
            var isSuccess = await _priceListRepository.RemoveLineItem(
                request.PriceListId,
                request.LineItemName,
            cancellationToken);

            return isSuccess ?
                    Result.Success() :
                    Result.Failure(Error.InvalidRequest(
                        $"Problem while removing line item of name {request.LineItemName} from price list with ID {request.PriceListId}"));
        }
    }
}