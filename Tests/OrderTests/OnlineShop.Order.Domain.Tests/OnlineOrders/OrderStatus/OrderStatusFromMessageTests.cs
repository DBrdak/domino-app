using Shared.Domain.Exceptions;

namespace OnlineShop.Order.Domain.Tests.OnlineOrders.OrderStatus;

public class OrderStatusFromMessageTests
{
    [Theory]
    [InlineData("Potwierdź kod SMS")]
    [InlineData("Oczekuje na potwierdzenie")]
    [InlineData("Potwierdzone")]
    [InlineData("Potwierdzone ze zmianami")]
    [InlineData("Anulowane")]
    [InlineData("Odrzucone")]
    [InlineData("Odebrane")]
    public void StatusFromMessage_ValidData_ShouldCreateStatus(string message)
    {
        // Arrange
        
        // Act
        var status = Domain.OnlineOrders.OrderStatus.FromMessage(message);

        // Assert
        Assert.NotNull(status);
        Assert.Equal(status.StatusMessage, message);
    }
    
    [Theory]
    [InlineData("Potwierdź SMS")]
    [InlineData("Oczekuje")]
    [InlineData("Potwierdzono")]
    [InlineData("Potwierdzono ze zmianami")]
    [InlineData("Anulowano")]
    [InlineData("Odrzucono")]
    [InlineData("Odebrano")]
    public void StatusFromMessage_InvalidData_ShouldThrow(string message)
    {
        // Arrange
        
        // Act
        var statusCreateFunc = () => Domain.OnlineOrders.OrderStatus.FromMessage(message);

        // Assert
        Assert.Throws<DomainException<Domain.OnlineOrders.OrderStatus>>(statusCreateFunc);
    }
}