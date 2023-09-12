using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineShop.Catalog.Application.Abstractions.Messaging;
using OnlineShop.Catalog.Domain.PriceLists;
using Shared.Domain.Abstractions;
using Shared.Domain.ResponseTypes;

namespace OnlineShop.Catalog.Application.Features.Admin.PriceLists.AddRetailPriceList
{
    internal sealed class AddRetailPriceListCommandHandler : ICommandHandler<AddRetailPriceListCommand>
    {
        private readonly IPriceListRepository _priceListRepository;

        public AddRetailPriceListCommandHandler(IPriceListRepository priceListRepository)
        {
            _priceListRepository = priceListRepository;
        }

        public async Task<Result> Handle(AddRetailPriceListCommand request, CancellationToken cancellationToken)
        {
            await _priceListRepository.AddPriceList(PriceList.CreateRetail("Cennik detaliczny"), cancellationToken);

            return Result.Success();
        }
    }
}