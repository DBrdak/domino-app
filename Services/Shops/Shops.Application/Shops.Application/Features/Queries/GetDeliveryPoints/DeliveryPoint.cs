using Shared.Domain.Date;
using Shared.Domain.DateTimeRange;
using Shared.Domain.Location;
using Shops.Domain.Abstractions;
using Shops.Domain.MobileShops;
using Shops.Domain.Shared;
using Shops.Domain.StationaryShops;

namespace Shops.Application.Features.Queries.GetDeliveryPoints;

public sealed record DeliveryPoint(Location Location, ShopWorkingDay[] WorkingDays)
{
    public List<DateTimeRange> PossiblePickupDate { get; private set; } = new();
    private const int workEndHour = 12;

    public void CalculateNextPossiblePickup()
    {
        var workingDaysAsDate = WorkingDays.Select(wd =>
        {
            if (!wd.IsClosed)
            {
                return new
                {
                    Date = DateTimeService.GetDateOnlyForNextDayOfWeek(wd.WeekDay),
                    Time = wd.OpenHours
                };
            }

            return null;
        });

        var availableWorkingDaysAsDate = workingDaysAsDate.Where(
                wd =>
                {
                    var dayDifferenceCount = DateTimeService.Today.DayNumber - wd.Date.DayNumber;

                    return DateTimeService.UtcNow.Hour >= workEndHour ?
                        dayDifferenceCount >= 2 :
                        dayDifferenceCount >= 1;
                });

        var availableWorkingDays = availableWorkingDaysAsDate.Select(wd => new DateTimeRange(wd.Date, wd.Time));

        PossiblePickupDate = availableWorkingDays.ToList();
    }

    public static List<DeliveryPoint> GetDeliveryPointsFromSalePoints(
        IEnumerable<SalePoint> mobileSalePoints,
        IEnumerable<StationaryShop> stationarySalePoints)
    {
        var mobileShopsSalePoints = GroupSalePoints(mobileSalePoints);

        return CreateDeliveryPoints(mobileShopsSalePoints, stationarySalePoints);
    }

    private static List<DeliveryPoint> CreateDeliveryPoints(
        IEnumerable<IGrouping<Location, SalePoint>> mobileShopsSalePoints,
        IEnumerable<StationaryShop> stationaryShops)
    {
        var deliveryPoints = new List<DeliveryPoint>();

        deliveryPoints.AddRange(FromSalePointGroupedList(mobileShopsSalePoints));
        deliveryPoints.AddRange(stationaryShops.Select(ss => (DeliveryPoint)ss));

        deliveryPoints.ForEach(dp => dp.CalculateNextPossiblePickup());

        return deliveryPoints.ToList();
    }

    private static IEnumerable<IGrouping<Location, SalePoint>> GroupSalePoints(
        IEnumerable<SalePoint> mobileSalePoints)
    {
        var salePointsByLocation = mobileSalePoints.GroupBy(sp => sp.Location);

        return salePointsByLocation;
    }

    public static IEnumerable<DeliveryPoint> FromSalePointGroupedList(IEnumerable<IGrouping<Location, SalePoint>> groupedSalePoints)
    {
        var deliveryPoints = new List<DeliveryPoint>();

        foreach (var group in groupedSalePoints)
        {
            deliveryPoints.Add(new DeliveryPoint(
                group.Key,
                group.Select(sp => new ShopWorkingDay(sp.WeekDay, sp.OpenHours!)).ToArray()));
        }

        return deliveryPoints;
    }

    public static explicit operator DeliveryPoint(StationaryShop stationaryShop)
    {
        return new(
            stationaryShop.Location,
            stationaryShop.WeekSchedule
        );
    }
}