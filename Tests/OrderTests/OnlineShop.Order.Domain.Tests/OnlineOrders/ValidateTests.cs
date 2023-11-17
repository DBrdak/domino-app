using System.Runtime.InteropServices;

namespace OnlineShop.Order.Domain.Tests.OnlineOrders;

public class ValidateTests
{
    [Fact]
    public void Validate_ValidData_ShouldValidateSuccessfully()
    {
        // Arrange
        var order = OnlineOrderTestData.TestOnlineOrder.Invoke();

        // Act
        order.Validate(true);

        // Assert
        Assert.Equal(order.Status, Domain.OnlineOrders.OrderStatus.Waiting);
    }
}