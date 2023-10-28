using Shared.Domain.Abstractions.Messaging;
using Shared.Domain.ResponseTypes;
using Shops.Domain.Abstractions;
using Shops.Domain.MobileShops;
using Shops.Domain.StationaryShops;

namespace Shops.Application.Features.Queries.GetDeliveryPoints
{
    internal sealed class GetDeliveryPointsQueryHandler : IQueryHandler<GetDeliveryPointsQuery, List<DeliveryPoint>>
    {
        private readonly IMobileShopRepository _mobileShopRepository;
        private readonly IStationaryShopRepository _stationaryShopRepository;

        public GetDeliveryPointsQueryHandler(
            IMobileShopRepository mobileShopRepository,
            IStationaryShopRepository stationaryShopRepository)
        {
            _mobileShopRepository = mobileShopRepository;
            _stationaryShopRepository = stationaryShopRepository;
        }

        public async Task<Result<List<DeliveryPoint>>> Handle(GetDeliveryPointsQuery request, CancellationToken cancellationToken)
        {
            var salePoints = await _mobileShopRepository
                .GetSalePoints(cancellationToken);
            var stationarySalePoints = (await _stationaryShopRepository
                .GetStationarySalePoints(cancellationToken));

            var deliveryPoints = DeliveryPoint
                .GetDeliveryPointsFromSalePoints(
                salePoints,
                stationarySalePoints);

            return deliveryPoints.Where(dp => dp.PossiblePickupDate.Any()).ToList();
        }
    }
}