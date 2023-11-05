using IntegrationEvents.Domain.Events.ShoppingCartCheckout;
using OnlineShop.Order.Domain.OrderItems;

namespace OnlineShop.Order.Domain.Tests.OrderItems;

public class CreateFromShoppingCartItemsTests
{
    [Theory]
    [ClassData(typeof(OrderItemsCreateFromShoppingCartTestData))]
    public void CreateFromShoppingCart_ValidData_ShouldCreateOrderItem(string orderId,
        List<ShoppingCartCheckoutItem> shoppingCartItems)
    {
        // Arrange
        
        // Act
        var orderItems = OrderItem.CreateFromShoppingCartItems(orderId, shoppingCartItems);

        //Assert
        Assert.NotEmpty(orderItems);
        Assert.True(orderItems.TrueForAll(i => i.OrderId == orderId));
    }
}