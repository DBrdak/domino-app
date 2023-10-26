using OnlineShop.Catalog.Domain.PriceLists;
using OnlineShop.Catalog.Domain.Shared;
using Shared.Domain.Abstractions.Messaging;
using Shared.Domain.ResponseTypes;

namespace OnlineShop.Catalog.Application.Features.Admin.PriceLists.AddBusinessPriceList
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
                PriceList.CreateBusiness(request.Name, request.ContractorName, Category.FromValue(request.Category)),
                cancellationToken);

            return Result.Success();
        }
    }
}