using OnlineShop.Order.Domain.OnlineOrders.Events;

namespace OnlineShop.Order.Domain.Tests.OnlineOrders;

public class SafeDeleteTests
{
    [Fact]
    public void SafeDelete_WithShop_ShouldRaiseDomainEvent()
    {
        // Arrange
        var order = OnlineOrderTestData.TestOnlineOrder.Invoke();
        order.SetShopId("exampleShopId1");
        
        // Act
        order.SafeDelete();
        
        // Assert
        Assert.Contains(order.GetDomainEvents(), e => e is OrderDeletedDomainEvent);
    }
}