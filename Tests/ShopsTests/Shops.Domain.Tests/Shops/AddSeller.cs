using Shared.Domain.Exceptions;
using Shops.Domain.Shops;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shops.Domain.Shared;

namespace Shops.Domain.Tests.Shops
{
    public class AddSeller
    {
        [Fact]
        public void AddSeller_ValidData_ShouldAddSeller()
        {
            // Arrange
            var stationaryShop = ShopTestData.CreateStationaryShop();
            var mobileShop = ShopTestData.CreateMobileShop();
            var seller1 = new Seller("Joe", "Doe", "123456789");
            var seller2 = new Seller("Mary", "Smith", null);

            // Act
            stationaryShop.AddSeller(seller1);
            mobileShop.AddSeller(seller2);

            // Assert
            Assert.True(stationaryShop.Sellers.First() == seller1);
            Assert.True(mobileShop.Sellers.First() == seller2);
        }

        [Fact]
        public void AddSeller_InvalidDuplicatedData_ShouldThrow()
        {
            // Arrange
            var stationaryShop = ShopTestData.CreateStationaryShop();
            var seller1 = new Seller("Joe", "Doe", "123456789");

            // Act
            stationaryShop.AddSeller(seller1);
            var tryToAddDuplicatedsellerFunc = () => stationaryShop.AddSeller(seller1);

            // Assert
            Assert.Throws<DomainException<Shop>>(tryToAddDuplicatedsellerFunc);
            Assert.True(stationaryShop.Sellers.First() == seller1);
            Assert.True(stationaryShop.Sellers.Count == 1);
        }

        [Theory]
        [InlineData("", "Tester", "555444333")]
        [InlineData("Test", "", "555444333")]
        [InlineData("Test", "Tester", "1234567890")]
        public void AddSeller_InvalidSellerData_ShouldThrow(string sellerFirstName, string sellerLastName, string sellerPhoneNumber)
        {
            // Arrange

            // Act
            var tryCreateSellerFunc = () => new Seller(sellerFirstName, sellerLastName, sellerPhoneNumber);

            // Assert
            Assert.Throws<DomainException<Seller>>(tryCreateSellerFunc);
        }
    }
}
