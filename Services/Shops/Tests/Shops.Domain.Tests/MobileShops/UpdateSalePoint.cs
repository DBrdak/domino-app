using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Domain.DateTimeRange;
using Shared.Domain.Exceptions;
using Shops.Domain.MobileShops;

namespace Shops.Domain.Tests.MobileShops
{
    public class UpdateSalePoint
    {
        [Fact]
        public void UpdateSalePoint_ValidData_ShouldUpdate()
        {
            // Arrange
            var shop = MobileShopTestData.CreateMobileShopWithSalePoints();
            var oldSalePoint = shop.SalePoints[0];
            var updatedSalePoint = new SalePoint(oldSalePoint.Location, oldSalePoint.WeekDay, false, new TimeRange("8:00", "12:30"), null, oldSalePoint.Id);

            // Act
            shop.UpdateSalePoint(updatedSalePoint);

            // Assert
            Assert.Equivalent(shop.SalePoints[0], updatedSalePoint);
        }

        [Fact]
        public void UpdateSalePoint_InvalidData_ShouldThrow()
        {
            // Arrange
            var shop = MobileShopTestData.CreateMobileShopWithSalePoints();
            var oldSalePoint = shop.SalePoints[0];
            var updatedSalePoint = new SalePoint(oldSalePoint.Location, oldSalePoint.WeekDay, false, new TimeRange("8:00", "12:30"), null, "Invalid ID");

            // Act
            var invalidUpdateFunc = () => shop.UpdateSalePoint(updatedSalePoint);

            // Assert
            Assert.Throws<DomainException<MobileShop>>(invalidUpdateFunc);
            Assert.Equivalent(shop.SalePoints[0], oldSalePoint);
        }
    }
}
