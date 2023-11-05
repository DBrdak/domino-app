using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Domain.Exceptions;
using Shops.Domain.Shops;

namespace Shops.Domain.Tests.Shops
{
    public class AddOrder
    {
        [Fact]
        public void AddOrder_ValidData_ShouldAddOrder()
        {
            // Arrange
            var stationaryShop = ShopTestData.CreateStationaryShop();
            var mobileShop = ShopTestData.CreateMobileShop();
            var orderId = "exampleOrderId";

            // Act
            stationaryShop.AddOrder(orderId);
            mobileShop.AddOrder(orderId);
            
            // Assert
            Assert.True(stationaryShop.OrdersId.First() == orderId);
            Assert.True(mobileShop.OrdersId.First() == orderId);
        }

        [Fact]
        public void AddOrder_InvalidData_ShouldThrow()
        {
            // Arrange
            var stationaryShop = ShopTestData.CreateMobileShop();
            var orderId = "exampleOrderId";

            // Act
            stationaryShop.AddOrder(orderId);
            var tryToAddDuplicatedOrderFunc = () => stationaryShop.AddOrder(orderId);

            // Assert
            Assert.Throws<DomainException<Shop>>(tryToAddDuplicatedOrderFunc);
            Assert.True(stationaryShop.OrdersId.First() == orderId);
            Assert.True(stationaryShop.OrdersId.Count == 1);
        }
    }
}
