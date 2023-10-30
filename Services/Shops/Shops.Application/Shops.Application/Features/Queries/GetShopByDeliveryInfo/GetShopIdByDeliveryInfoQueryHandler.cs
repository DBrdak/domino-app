using Shared.Domain.Abstractions.Messaging;
using Shared.Domain.Date;
using Shared.Domain.DateTimeRange;
using Shared.Domain.Errors;
using Shared.Domain.ResponseTypes;
using Shops.Domain.Abstractions;
using Shops.Domain.MobileShops;
using Shops.Domain.StationaryShops;

namespace Shops.Application.Features.Queries.GetShopByDeliveryInfo
{
    internal sealed class GetShopIdByDeliveryInfoQueryHandler : IQueryHandler<GetShopIdByDeliveryInfoQuery, string>
    {
        private readonly IShopRepository _shopRepository;

        public GetShopIdByDeliveryInfoQueryHandler(IShopRepository shopRepository)
        {
            _shopRepository = shopRepository;
        }

        public async Task<Result<string>> Handle(GetShopIdByDeliveryInfoQuery request, CancellationToken cancellationToken)
        {
            var shops = await _shopRepository.GetShops(cancellationToken);

            var shop = FromStationaryShop(request, shops) ?? FromMobileShop(request, shops);

            if (shop is null)
            {
                return Result.Failure<string>(
                    Error.NotFound(
                        $"Shop for delivery data: [{request.DeliveryLocation.Name}, {request.DeliveryDate.Start} - {request.DeliveryDate.End}] not found"));
            }

            return shop.Id;
        }

        private static Shop? FromMobileShop(GetShopIdByDeliveryInfoQuery request, List<Shop> shops)
        {
            return shops.SingleOrDefault(s =>
            {
                if (s is MobileShop shop) 
                    return shop.SalePoints
                        .Any(
                            sp => sp.Location == request.DeliveryLocation &&
                                  sp.OpenHours == new TimeRange(request.DeliveryDate) &&
                                  sp.WeekDay == WeekDay.FromDayOfWeekEnum(request.DeliveryDate.Start.DayOfWeek));
                return false;
            });
        }

        private static Shop? FromStationaryShop(GetShopIdByDeliveryInfoQuery request, List<Shop> shops)
        {
            return shops.SingleOrDefault(s => 
                (s as StationaryShop)?.Location == request.DeliveryLocation);
        }
    }
}
