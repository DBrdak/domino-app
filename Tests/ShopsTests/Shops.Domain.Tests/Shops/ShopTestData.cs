using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Domain.Location;
using Shops.Domain.MobileShops;
using Shops.Domain.Shops;
using Shops.Domain.StationaryShops;

namespace Shops.Domain.Tests.Shops
{
    public class ShopTestData
    {
        public static Shop CreateStationaryShop () => new StationaryShop(
            "Test Stationary Shop",
            new Location("example location", "50.2", "18.9"));

        public static Shop CreateMobileShop () => new MobileShop(
            "Test Mobile Shop", 
            "WPN EEEE");
    }
}
