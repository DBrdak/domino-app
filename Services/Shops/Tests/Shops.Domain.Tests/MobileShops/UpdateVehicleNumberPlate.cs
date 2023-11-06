using Shared.Domain.Exceptions;
using Shops.Domain.MobileShops;

namespace Shops.Domain.Tests.MobileShops
{
    public class UpdateVehicleNumberPlate
    {
        [Theory]
        [InlineData("WPN 89QW")]
        [InlineData("WPN 89QWS")]
        [InlineData("WP 89QWS")]
        [InlineData("WP 9QWS")]
        public void UpdateVehicleNumberPlate_ValidData_ShouldUpdate(string newPlate)
        {
            // Arrange
            var shop = MobileShopTestData.CreateMobileShop();

            // Act
            shop.UpdateVehicleNumberPlate(newPlate);

            // Assert
            Assert.Equal(shop.VehiclePlateNumber, newPlate);
        }

        [Theory]
        [InlineData("WPNK 89QS")]
        [InlineData("W 9QWS")]
        [InlineData("WPN 89QWSS")]
        [InlineData("WPN 89Q")]
        [InlineData("WPN 89qs")]
        [InlineData("wpn 89QS")]
        public void UpdateVehicleNumberPlate_InvalidData_ShouldThrow(string newPlate)
        {
            // Arrange
            var shop = MobileShopTestData.CreateMobileShop();
            var oldPlate = shop.VehiclePlateNumber;

            // Act
            var invalidRegisterPlateNumberUpdateFunc = () => shop.UpdateVehicleNumberPlate(newPlate);

            // Assert
            Assert.Throws<DomainException<MobileShop>>(invalidRegisterPlateNumberUpdateFunc);
            Assert.Equal(shop.VehiclePlateNumber, oldPlate);
        }
    }
}
