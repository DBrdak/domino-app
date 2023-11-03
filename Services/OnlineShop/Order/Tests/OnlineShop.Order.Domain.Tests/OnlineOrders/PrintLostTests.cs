using OnlineShop.Order.Domain.OnlineOrders;
using OnlineShop.Order.Domain.OrderItems;
using Shared.Domain.Exceptions;

namespace OnlineShop.Order.Domain.Tests.OnlineOrders;

public class PrintLostTests
{
    [Theory]
    [InlineData("Potwierdzone")]
    [InlineData("Potwierdzone ze zmianami")]
    public void OrderPrintLost_ValidData_ShouldUpdatePrintToLost(string orderStatus)
    {
        // Arrange
        var order = OnlineOrderTestData.TestOnlineOrder.Invoke();
        order.Validate(true);
        if(orderStatus == "Potwierdzone") order.UpdateStatus(orderStatus, null);
        if(orderStatus == "Potwierdzone ze zmianami") order.UpdateStatus(orderStatus, new List<OrderItem>(){order.Items.First()});
        order.Print();
        
        // Act
        order.PrintLost();
        
        // Assert
        Assert.False(order.IsPrinted);
    }
    
    [Theory]
    [InlineData("Odrzucone")]
    [InlineData("Odebrane")]
    [InlineData("Anulowane")]
    [InlineData("Potwierdź kod SMS")]
    public void OrderPrintLost_InvalidData_ShouldThrow(string orderStatus)
    {
        // Arrange
        var order = OnlineOrderTestData.TestOnlineOrder.Invoke();
        if(orderStatus != "Potwierdź kod SMS" && orderStatus != "Anulowane")
        {
            order.Validate(true);
            if(orderStatus == "Odebrane") order.UpdateStatus("Potwierdzone", null);
            order.UpdateStatus(orderStatus, null);
        }
        else
        {
            order.Cancel();
        }

        // Act
        var printLostFunc = () => order.PrintLost();
        
        // Assert
        Assert.Throws<DomainException<OnlineOrder>>(printLostFunc);
    }
}