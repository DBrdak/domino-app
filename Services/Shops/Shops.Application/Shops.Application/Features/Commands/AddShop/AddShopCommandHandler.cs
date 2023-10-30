using Shared.Domain.Abstractions.Messaging;
using Shared.Domain.Errors;
using Shared.Domain.ResponseTypes;
using Shops.Domain.Abstractions;
using Shops.Domain.MobileShops;
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
                Error.InvalidRequest("Shop values not provided - unable to create shop")
            );
        }

        public async Task<Result<Shop>> Handle(AddShopCommand request, CancellationToken cancellationToken)
        {
            if (request.MobileShopData is not null && request.StationaryShopData is not null)
            {
                _result = Result.Failure<Shop>(
                    Error.InvalidRequest("Shop values provided for both mobile and stationary shop - unable to create shop")
                );
            }
            else if (request.MobileShopData is not null)
            {
                Shop? mobileShop = CreateMobileShop(request.ShopName, request.MobileShopData);
                mobileShop = await _shopRepository.AddShop(mobileShop, cancellationToken);

                _result = mobileShop is not null ? 
                    Result.Success<Shop>(mobileShop) :
                    Result.Failure<Shop>(
                        Error.InvalidRequest($"Error during database operation on insert new shop with name: {request.ShopName}")
                    );
            }
            else if (request.StationaryShopData is not null)
            {
                Shop? stationaryShop = CreateStationaryShop(request.ShopName, request.StationaryShopData);
                stationaryShop = await _shopRepository.AddShop(stationaryShop, cancellationToken);

                _result = stationaryShop is not null ?
                        Result.Success<Shop>(stationaryShop) :
                        Result.Failure<Shop>(
                            Error.InvalidRequest($"Error during database operation on insert new shop with name: {request.ShopName}")
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