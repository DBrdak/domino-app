using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineShop.Catalog.Application.Abstractions.Messaging;
using OnlineShop.Catalog.Domain.PriceLists;
using Shared.Domain.Errors;
using Shared.Domain.ResponseTypes;

namespace OnlineShop.Catalog.Application.Features.Admin.PriceLists.AddLineItem
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
                    $"Problem while adding new line item of name {request.Name} to price list with ID {request.PriceListId}"
                    ));
        }
    }
}