using Shared.Domain.DateTimeRange;
using Shared.Domain.Location;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Domain.Date;
using Shops.Domain.MobileShops;

namespace Shops.Domain.Tests.MobileShops
{
    public class AddSalePoint
    {
        [Fact]
        public void AddSalePoint_ValidData_ShouldAdd()
        {
            // Arrange
            var shop = MobileShopTestData.CreateMobileShop();
            var location = new Location("Test Location 1", "50.2", "20.1");
            var openHours = new TimeRange("9:30", "10:45");
            var weekDay = WeekDay.Tuesday;

            // Act
            shop.AddSalePoint(location, openHours, weekDay.Value);

            // Assert
            Assert.NotEmpty(shop.SalePoints);
            Assert.True(
                shop.SalePoints[0].WeekDay == weekDay &&
                shop.SalePoints[0].IsClosed == false &&
                shop.SalePoints[0].CachedOpenHours == null &&
                shop.SalePoints[0].Location == location &&
                shop.SalePoints[0].OpenHours == openHours);
        }
    }
}
