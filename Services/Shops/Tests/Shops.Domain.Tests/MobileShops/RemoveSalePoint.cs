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
    }
}
