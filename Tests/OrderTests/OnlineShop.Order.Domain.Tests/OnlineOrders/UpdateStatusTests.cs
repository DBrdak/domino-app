using OnlineShop.Order.Domain.OnlineOrders;
using OnlineShop.Order.Domain.OrderItems;
using Shared.Domain.Exceptions;

namespace OnlineShop.Order.Domain.Tests.OnlineOrders;

public class UpdateStatusTests
{
    [Theory]
    [InlineData("Potwierdzone")]
    [InlineData("Potwierdzone ze zmianami")]
    [InlineData("Odrzucone")]
    public void UpdateStatusForValidOrder_ValidData_ShouldUpdateStatus(string status)
    {
        // Arrange
        var order = OnlineOrderTestData.TestOnlineOrder.Invoke();
        order.Validate(true);
        

        // Act
        Action updateAction = status != "Potwierdzone ze zmianami" ?
            () => order.UpdateStatus(status, null) :
            () => order.UpdateStatus(status, new List<OrderItem>(){ order.Items.First() });
        updateAction.Invoke();

        // Assert
        Assert.Equal(order.Status.StatusMessage, status);
    }

    [Theory]
    [InlineData("Odebrane")]
    public void UpdateStatusForAcceptedOrder_ValidData_ShouldUpdateStatus(string status)
    {
        // Arrange
        var order = OnlineOrderTestData.TestOnlineOrder.Invoke();
        order.Validate(true);
        order.UpdateStatus("Potwierdzone", null);

        // Act
        order.UpdateStatus(status, null);

        // Assert
        Assert.Equal(order.Status.StatusMessage, status);
    }

    [Theory]
    [InlineData("Odrzucone")]
    public void UpdateStatusForInvalidOrder_ValidData_ShouldUpdateStatus(string status)
    {
        // Arrange
        var order = OnlineOrderTestData.TestOnlineOrder.Invoke();

        // Act
        Action updateAction = status != "Potwierdzone ze zmianami" ?
            () => order.UpdateStatus(status, null) :
            () => order.UpdateStatus(status, new List<OrderItem>() { order.Items.First() });
        updateAction.Invoke();

        // Assert
        Assert.Equal(order.Status.StatusMessage, status);
    }

    [Theory]
    [InlineData("Oczekuje na potwierdzenie")]
    [InlineData("Potwierdź kod SMS")]
    [InlineData("Anulowane")]
    public void UpdateStatusForValidOrder_InvalidData_ShouldThrow(string status)
    {
        // Arrange
        var order = OnlineOrderTestData.TestOnlineOrder.Invoke();
        order.Validate(true);


        // Act
        Action updateAction = status != "Potwierdzone ze zmianami" ?
            () => order.UpdateStatus(status, null) :
            () => order.UpdateStatus(status, new List<OrderItem>() { order.Items.First() });

        // Assert
        Assert.Throws<DomainException<OnlineOrder>>(updateAction);
    }

    [Theory]
    [InlineData("Oczekuje na potwierdzenie")]
    [InlineData("Potwierdź kod SMS")]
    [InlineData("Anulowane")]
    [InlineData("Potwierdzone")]
    [InlineData("Potwierdzone ze zmianami")]
    [InlineData("Odebrane")]
    public void UpdateStatusForInvalidOrder_InvalidData_ShouldThrow(string status)
    {
        // Arrange
        var order = OnlineOrderTestData.TestOnlineOrder.Invoke();

        // Act
        Action updateAction = status != "Potwierdzone ze zmianami" ?
            () => order.UpdateStatus(status, null) :
            () => order.UpdateStatus(status, new List<OrderItem>() { order.Items.First() });

        // Assert
        Assert.Throws<DomainException<OnlineOrder>>(updateAction);
    }

    [Theory]
    [InlineData("Oczekuje na potwierdzenie")]
    [InlineData("Potwierdź kod SMS")]
    [InlineData("Anulowane")]
    [InlineData("Potwierdzone")]
    [InlineData("Potwierdzone ze zmianami")]
    [InlineData("Odrzucone")]
    public void UpdateStatusForAcceptedOrder_InvalidData_ShouldThrow(string status)
    {
        // Arrange
        var order = OnlineOrderTestData.TestOnlineOrder.Invoke();
        order.Validate(true);
        order.UpdateStatus("Potwierdzone", null);

        // Act
        Action updateAction = status != "Potwierdzone ze zmianami" ?
            () => order.UpdateStatus(status, null) :
            () => order.UpdateStatus(status, new List<OrderItem>() { order.Items.First() });

        // Assert
        Assert.Throws<DomainException<OnlineOrder>>(updateAction);
    }
}