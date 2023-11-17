using Shared.Domain.Date;
using Shared.Domain.DateTimeRange;
using Shared.Domain.Exceptions;
using Shared.Domain.Location;
using Shops.Domain.MobileShops;

namespace Shops.Domain.Tests.MobileShops
{
    public class RemoveSalePoint
    {
        [Fact]
        public void RemoveSalePoint_ValidData_ShouldRemove()
        {
            // Arrange
            var shop = MobileShopTestData.CreateMobileShopWithSalePoints();
            var salePointToRemove = shop.SalePoints[0];

            // Act
            shop.RemoveSalePoint(salePointToRemove);

            // Assert
            Assert.True(!shop.SalePoints.Contains(salePointToRemove));
        }

        [Fact]
        public void RemoveSalePoint_InvalidData_ShouldThrow()
        {
            // Arrange
            var shop = MobileShopTestData.CreateMobileShopWithSalePoints();
            var salePointsInitialCount = shop.SalePoints.Count;
            var salePointToRemove = new SalePoint(new Location("example", "12.12", "21.21"), new TimeRange("15:30", "16:15"), WeekDay.Friday);

            // Act
            var removeNoExistingSalePointFunc = () => shop.RemoveSalePoint(salePointToRemove);

            // Assert
            Assert.Throws<DomainException<MobileShop>>(removeNoExistingSalePointFunc);
            Assert.Equal(shop.SalePoints.Count, salePointsInitialCount);
        }
    }
}
