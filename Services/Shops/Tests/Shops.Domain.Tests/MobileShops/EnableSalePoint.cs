using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Domain.Date;
using Shared.Domain.DateTimeRange;
using Shared.Domain.Exceptions;
using Shared.Domain.Location;
using Shops.Domain.MobileShops;

namespace Shops.Domain.Tests.MobileShops
{
    public class EnableSalePoint
    {
        [Fact]
        public void EnableSalePoint_ValidData_ShouldEnable()
        {
            // Arrange
            var shop = MobileShopTestData.CreateMobileShopWithSalePoints();
            var salePointToEnable = shop.SalePoints[0];

            // Act
            shop.EnableSalePoint(salePointToEnable);

            //Assert
            var enabledSalePoint = shop.SalePoints[0];
            Assert.True(
                !enabledSalePoint.IsClosed &&
                enabledSalePoint.CachedOpenHours is null &&
                enabledSalePoint.OpenHours is not null);
        }

        [Fact]
        public void EnableSalePoint_InvalidData_ShouldThrow()
        {
            // Arrange
            var shop = MobileShopTestData.CreateMobileShopWithSalePoints();
            var salePointsBeforeUpdate = shop.SalePoints;
            var salePointToEnable = new SalePoint(new Location("example", "12.12", "21.21"), new TimeRange("15:30", "16:15"), WeekDay.Friday);

            // Act
            var enableNoExistingSalePointFunc = () => shop.EnableSalePoint(salePointToEnable);

            // Assert
            Assert.Throws<DomainException<MobileShop>>(enableNoExistingSalePointFunc);
            Assert.Equal(shop.SalePoints, salePointsBeforeUpdate);
        }
    }
}
