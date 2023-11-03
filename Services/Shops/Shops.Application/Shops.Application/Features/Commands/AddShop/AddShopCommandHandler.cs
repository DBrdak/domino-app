using Shared.Domain.Abstractions.Messaging;
using Shared.Domain.Errors;
using Shared.Domain.ResponseTypes;
using Shops.Domain.MobileShops;
using Shops.Domain.Shops;
using Shops.Domain.StationaryShops;

namespace Shops.Application.Features.Commands.AddShop
{
    internal sealed class AddShopCommandHandler : ICommandHandler<AddShopCommand, Shop>
    {
        private readonly IShopRepository _shopRepository;
        private Result<Shop> _result;

        public AddShopCommandHandler(IShopRepository shopRepository)
        {
            _shopRepository = shopRepository;
            _result = Result.Failure<Shop>(
                Error.InvalidRequest("Nie zapewniono wymaganych danych do stworzenia sklepu")
            );
        }

        public async Task<Result<Shop>> Handle(AddShopCommand request, CancellationToken cancellationToken)
        {
            if (request.MobileShopData is not null && request.StationaryShopData is not null)
            {
                _result = Result.Failure<Shop>(
                    Error.InvalidRequest("Dane do storzenia sklepu zostały podane dla sklepu stacjonarnego i mobilnego - proszę podać wartości dla jednego z nich")
                );
            }
            else if (request.MobileShopData is not null)
            {
                Shop? mobileShop = CreateMobileShop(request.ShopName, request.MobileShopData);
                mobileShop = await _shopRepository.AddShop(mobileShop, cancellationToken);

                _result = mobileShop is not null ? 
                    Result.Success<Shop>(mobileShop) :
                    Result.Failure<Shop>(
                        Error.InvalidRequest($"Błąd podczas wprowadzania do bazy danych nowego sklepu o nazwie: {request.ShopName}")
                    );
            }
            else if (request.StationaryShopData is not null)
            {
                Shop? stationaryShop = CreateStationaryShop(request.ShopName, request.StationaryShopData);
                stationaryShop = await _shopRepository.AddShop(stationaryShop, cancellationToken);

                _result = stationaryShop is not null ?
                        Result.Success<Shop>(stationaryShop) :
                        Result.Failure<Shop>(
                            Error.InvalidRequest($"Błąd podczas wprowadzania do bazy danych nowego sklepu o nazwie: {request.ShopName}")
                        );
            }

            return _result;
        }

        private static MobileShop CreateMobileShop(string shopName, MobileShopDto mobileShopData) =>
            new(shopName, mobileShopData.VehiclePlateNumber);

        private static StationaryShop CreateStationaryShop(string shopName, StationaryShopDto stationaryShopData) =>
            new(shopName, stationaryShopData.Location);
    }
}