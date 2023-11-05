using Shops.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Domain.Exceptions;
using Shops.Domain.Shops;

namespace Shops.Domain.Tests.Shops
{
    public class RemoveSeller
    {
        [Fact]
        public void RemoveSeller_ValidData_ShouldRemoveSeller()
        {
            // Arrange
            var stationaryShop = ShopTestData.CreateStationaryShop();
            var mobileShop = ShopTestData.CreateMobileShop();
            var seller1 = new Seller("Joe", "Doe", "123456789");
            var seller2 = new Seller("Mary", "Smith", null);
            stationaryShop.AddSeller(seller1);
            mobileShop.AddSeller(seller2);

            // Act
            stationaryShop.RemoveSeller(seller1);
            mobileShop.RemoveSeller(seller2);

            // Assert
            Assert.True(stationaryShop.Sellers.Count == 0);
            Assert.True(mobileShop.Sellers.Count == 0);
        }

        [Fact]
        public void RemoveSeller_InvalidData_ShouldThrow()
        {
            // Arrange
            var stationaryShop = ShopTestData.CreateMobileShop();
            var seller1 = new Seller("Joe", "Doe", "123456789");
            var seller2 = new Seller("Mary", "Smith", null);
            stationaryShop.AddSeller(seller1);

            // Act
            var removeWrongSellerFunc = () => stationaryShop.RemoveSeller(seller2);

            // Assert
            Assert.Throws<DomainException<Shop>>(removeWrongSellerFunc);
            Assert.True(stationaryShop.Sellers.Count == 1);
            Assert.True(stationaryShop.Sellers.All(s => s != seller2));
        }
    }
}
