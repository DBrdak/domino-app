using IntegrationEvents.Domain.Events.ShoppingCartCheckout;
using OnlineShop.Order.Domain.OnlineOrders;
using OnlineShop.Order.Domain.OnlineOrders.Events;
using Shared.Domain.Date;

namespace OnlineShop.Order.Domain.Tests.OnlineOrders;

public class CreateTests
{
    [Theory]
    [ClassData(typeof(OrderCreateSuccessTestData))]
    public void OrderCreate_ValidData_ShouldCreateOrder(ShoppingCartCheckoutEvent shoppingCart)
    {
        // Arrange
        
        // Act
        var order = OnlineOrder.Create(shoppingCart);

        // Assert
        Assert.NotNull(order);
        Assert.True(order.GetDomainEvents().First() is OrderCreatedDomainEvent );
        Assert.Null(order.CompletionDate);
        Assert.Null(order.ExpiryDate);
        Assert.False(order.IsPrinted);
        Assert.NotEmpty(order.Items);
    }
}