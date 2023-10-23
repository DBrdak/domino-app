﻿using OnlineShop.Catalog.Domain.PriceLists;
using Shared.Domain.Abstractions.Messaging;
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
            await _priceListRepository.AddPriceList(PriceList.CreateRetail("Cennik detaliczny mięsa", PriceListCategory.Meat), cancellationToken);
            await _priceListRepository.AddPriceList(PriceList.CreateRetail("Cennik detaliczny wędlin", PriceListCategory.Sausage), cancellationToken);

            return Result.Success();
        }
    }
}