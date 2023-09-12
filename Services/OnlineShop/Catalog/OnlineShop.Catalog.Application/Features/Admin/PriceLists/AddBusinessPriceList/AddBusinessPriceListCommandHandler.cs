using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineShop.Catalog.Application.Abstractions.Messaging;
using OnlineShop.Catalog.Domain.PriceLists;
using Shared.Domain.Abstractions;
using Shared.Domain.ResponseTypes;

namespace OnlineShop.Catalog.Application.Features.Admin.PriceLists.AddPriceList
{
    internal sealed class AddBusinessPriceListCommandHandler : ICommandHandler<AddBusinessPriceListCommand>
    {
        private readonly IPriceListRepository _priceListRepository;

        public AddBusinessPriceListCommandHandler(IPriceListRepository priceListRepository)
        {
            _priceListRepository = priceListRepository;
        }

        public async Task<Result> Handle(AddBusinessPriceListCommand request, CancellationToken cancellationToken)
        {
            await _priceListRepository.AddPriceList(
                PriceList.CreateBusiness(request.Name, request.ContractorName),
                cancellationToken);

            return Result.Success();
        }
    }
}