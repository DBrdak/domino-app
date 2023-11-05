using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Domain.Date;
using Shared.Domain.Location;
using Shops.Domain.Shared;
using Shops.Domain.Shops;
using Shops.Domain.StationaryShops;

namespace Shops.Domain.Tests.StationaryShops
{
    public class StationaryShopTestData
    {
        public static StationaryShop CreateStationaryShop() => new (
            "Test Stationary Shop",
            new Location("example location", "50.2", "18.9"));

        public static StationaryShop CreateStationaryShopWithWeekSchdule()
        {
            var shop = CreateStationaryShop();

            var weekSchedule = new List<ShopWorkingDay>()
            {
                new (WeekDay.Monday, new (TimeOnly.Parse("8:00"), TimeOnly.Parse("20:00"))),
                new (WeekDay.Tuesday, new (TimeOnly.Parse("8:00"), TimeOnly.Parse("20:00"))),
                new (WeekDay.Wednesday, new (TimeOnly.Parse("8:00"), TimeOnly.Parse("20:00"))),
                new (WeekDay.Thursday, new (TimeOnly.Parse("8:00"), TimeOnly.Parse("20:00"))),
                new (WeekDay.Friday, new (TimeOnly.Parse("8:00"), TimeOnly.Parse("20:00"))),
                new (WeekDay.Saturday, new (TimeOnly.Parse("9:00"), TimeOnly.Parse("18:00")))
            };

            shop.CreateWeekSchedule(weekSchedule);

            return shop;
        }
    }
}
