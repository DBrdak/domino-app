using OnlineShop.Order.Domain.OnlineOrders;
using Shared.Domain.Exceptions;

namespace OnlineShop.Order.Domain.Tests.OnlineOrders;

public class SetShopIdTests
{
    [Fact]
    public void SetShopId_ValidData_ShouldSetShopId()
    {
        // Arrange
        var order = OnlineOrderTestData.TestOnlineOrder.Invoke();
        var shopId = "exampleShopId1";

        // Act
        order.SetShopId(shopId);

        // Assert
        Assert.True(order.ShopId == shopId);
    }

    [Fact]
    public void SetShopId_InvalidData_ShouldThrow()
    {
        // Arrange
        var order = OnlineOrderTestData.TestOnlineOrder.Invoke();

        // Act
        var setShopIdFunc = () => order.SetShopId(" ");

        // Assert
        Assert.Throws<DomainException<OnlineOrder>>(setShopIdFunc);
    }
}