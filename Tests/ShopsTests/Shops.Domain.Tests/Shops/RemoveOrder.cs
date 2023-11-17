using Shared.Domain.Exceptions;
using Shops.Domain.MobileShops;
using Shops.Domain.Shared;
using Shops.Domain.Shops;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shops.Domain.Tests.Shops
{
    public class RemoveOrder
    {
        [Fact]
        public void Removeorder_ValidData_ShouldRemoveOrder()
        {
            // Arrange
            var stationaryShop = ShopTestData.CreateMobileShop();
            var mobileShop = ShopTestData.CreateMobileShop();
            var orderId1 = "exmapleOrderId1";
            var orderId2 = "exmapleOrderId2";
            stationaryShop.AddOrder(orderId1);
            mobileShop.AddOrder(orderId2);

            // Act
            stationaryShop.RemoveOrder(orderId1);
            mobileShop.RemoveOrder(orderId2);

            // Assert
            Assert.True(stationaryShop.OrdersId.Count == 0);
            Assert.True(mobileShop.OrdersId.Count == 0);
        }

        [Fact]
        public void RemoveOrder_InvalidData_ShouldThrow()
        {
            // Arrange
            var stationaryShop = ShopTestData.CreateMobileShop();
            var orderId1 = "exmapleOrderId1";
            var orderId2 = "exmapleOrderId2";
            stationaryShop.AddOrder(orderId1);

            // Act
            var removeWrongOrderFunc = () => stationaryShop.RemoveOrder(orderId2);

            // Assert
            Assert.Throws<DomainException<Shop>>(removeWrongOrderFunc);
            Assert.True(stationaryShop.OrdersId.Count == 1);
            Assert.True(stationaryShop.OrdersId.All(s => s != orderId2));
        }
    }
}
