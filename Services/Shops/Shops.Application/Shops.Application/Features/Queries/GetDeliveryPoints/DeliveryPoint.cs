using Shared.Domain.Date;
using Shared.Domain.DateTimeRange;
using Shared.Domain.Location;
using Shops.Domain.MobileShops;
using Shops.Domain.Shared;
using Shops.Domain.StationaryShops;

namespace Shops.Application.Features.Queries.GetDeliveryPoints;

public sealed record DeliveryPoint(Location Location, ShopWorkingDay[] WorkingDays)
{
    public List<DateTimeRange> PossiblePickupDate { get; private set; } = new();
    private const int utcWorkEndHour = 14;

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
                    if (wd is null)
                    {
                        return false;
                    }

                    var dayDifferenceCount = wd.Date.DayNumber - DateTimeService.Today.DayNumber;

                    return DateTimeService.UtcNow.Hour >= utcWorkEndHour ?
                        dayDifferenceCount >= 2 :
                        dayDifferenceCount >= 1;
                });

        var availableWorkingDays = availableWorkingDaysAsDate.Select(wd => new DateTimeRange(wd.Date, wd.Time));

        PossiblePickupDate = availableWorkingDays.OrderBy(d => d.Start).ToList();
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
            var activeSalePoints = group.Where(sp => !sp.IsClosed);

            deliveryPoints.Add(new DeliveryPoint(
                group.Key,
                activeSalePoints.Select(sp => new ShopWorkingDay(sp.WeekDay, sp.OpenHours!)).ToArray()));
        }

        return deliveryPoints;
    }

    public static explicit operator DeliveryPoint(StationaryShop stationaryShop)
    {
        return new(
            stationaryShop.Location,
            stationaryShop.WeekSchedule.Where(d => !d.IsClosed).ToArray()
        );
    }
}