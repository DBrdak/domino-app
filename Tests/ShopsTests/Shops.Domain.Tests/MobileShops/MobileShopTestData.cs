using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Domain.DateTimeRange;
using Shared.Domain.Location;
using Shops.Domain.MobileShops;
using Shops.Domain.Shops;

namespace Shops.Domain.Tests.MobileShops
{
    public class MobileShopTestData
    {
        public static MobileShop CreateMobileShop() => new MobileShop(
            "Test Mobile Shop",
            "WPN EEEE");

        public static MobileShop CreateMobileShopWithSalePoints()
        {
            var shop = CreateMobileShop();

            shop.AddSalePoint(new Location("Test Location 1", "50.2", "20.1"), new TimeRange("9:30", "10:45"), "Wtorek");
            shop.AddSalePoint(new Location("Test Location 2", "50.6", "20.8"), new TimeRange("7:30", "9:00"), "Piątek");
            shop.AddSalePoint(new Location("Test Location 3", "50.1", "20.3"), new TimeRange("8:30", "12:00"), "Środa");

            return shop;
        }
    }

    public class AddSalePointOverlapTestData : TheoryData<TimeRange>
    {
        public AddSalePointOverlapTestData()
        {
            // 9:30 - 10:45
            Add(new TimeRange("9:00", "10:00")); // Starts earlier, ends earlier
            Add(new TimeRange("9:15", "12:30")); // Starts earlier, ends later
            Add(new TimeRange("10:00", "11:00")); // Starts later, ends later
            Add(new TimeRange("10:00", "10:30")); // Starts later, ends earlier
        }
    }
}
