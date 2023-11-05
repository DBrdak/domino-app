using Shared.Domain.Date;
using Shared.Domain.DateTimeRange;
using Shared.Domain.Exceptions;
using Shared.Domain.Location;
using Shops.Domain.MobileShops;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shops.Domain.Tests.MobileShops
{
    public class DisableSalePoint
    {
        [Fact]
        public void DisableSalePoint_ValidData_ShouldDisable()
        {
            // Arrange
            var shop = MobileShopTestData.CreateMobileShopWithSalePoints();
            var salePointToDisable = shop.SalePoints[0];

            // Act
            shop.DisableSalePoint(salePointToDisable);

            //Assert
            var disabledSalePoint = shop.SalePoints[0];
            Assert.True(
                disabledSalePoint.IsClosed &&
                disabledSalePoint.CachedOpenHours is not null &&
                disabledSalePoint.OpenHours is null);
        }

        [Fact]
        public void DisableSalePoint_InvalidData_ShouldThrow()
        {
            // Arrange
            var shop = MobileShopTestData.CreateMobileShopWithSalePoints();
            var salePointsBeforeUpdate = shop.SalePoints;
            var salePointToDisable = new SalePoint(new Location("example", "12.12", "21.21"), new TimeRange("15:30", "16:15"), WeekDay.Friday);

            // Act
            var disableNoExistingSalePointFunc = () => shop.DisableSalePoint(salePointToDisable);

            // Assert
            Assert.Throws<DomainException<MobileShop>>(disableNoExistingSalePointFunc);
            Assert.Equal(shop.SalePoints, salePointsBeforeUpdate);
        }
    }
}
