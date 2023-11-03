using MongoDB.Bson.Serialization.Attributes;
using Shared.Domain.Date;
using Shared.Domain.DateTimeRange;
using Shared.Domain.Exceptions;
using Shared.Domain.Location;
using Shops.Domain.Shared;
using Shops.Domain.Shops;

namespace Shops.Domain.StationaryShops
{
    [BsonDiscriminator(nameof(StationaryShop))]
    public sealed class StationaryShop : Shop
    {
        public ShopWorkingDay[] WeekSchedule { get; init; }
        public Location Location { get; init; }

        public StationaryShop(string shopName, Location location) : base(shopName)
        {
            Location = location;
            WeekSchedule = new ShopWorkingDay[7]
            {
                new (WeekDay.Monday),
                new (WeekDay.Tuesday),
                new (WeekDay.Wednesday),
                new (WeekDay.Thursday),
                new (WeekDay.Friday),
                new (WeekDay.Saturday),
                new (WeekDay.Sunday)
            };
        }

        private void AddShopWorkingDay(WeekDay weekDay, TimeRange openHours)
        {
            var weekDayIndex = WeekDay.GetIndex(weekDay);

            WeekSchedule[weekDayIndex]!.UpdateOpenHours(openHours);
        }

        public void CreateWeekSchedule(List<ShopWorkingDay> weekSchedule)
        {
            if (WeekSchedule.Any(wd => wd.OpenHours is not null))
            {
                throw new DomainException<StationaryShop>($"Week schedule for shop with name {ShopName} already exists");
            }

            var shopWorkingDays = weekSchedule.Where(
            shopWorkingDay => shopWorkingDay.OpenHours is not null && !shopWorkingDay.IsClosed);

            foreach (var shopWorkingDay in shopWorkingDays)
            {
                AddShopWorkingDay(shopWorkingDay.WeekDay, shopWorkingDay.OpenHours!);
            }
        }

        public void UpdateOpenHoursForWeekDay(WeekDay weekDay, TimeRange openHours)
        {
            var weekDayIndex = WeekDay.GetIndex(weekDay);

            WeekSchedule[weekDayIndex]!.UpdateOpenHours(openHours);
        }

        public void SetHolidayForWeekDay(WeekDay weekDay)
        {
            WeekSchedule.First(wd => wd.WeekDay == weekDay).Close();
        }

        public void SetWorkForWeekDay(WeekDay weekDay)
        {
            WeekSchedule.First(wd => wd.WeekDay == weekDay).Open();
        }
    }
}