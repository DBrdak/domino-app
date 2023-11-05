using IntegrationEvents.Domain.Events.ShoppingCartCheckout;
using Shared.Domain.Money;
using Shared.Domain.Quantity;

namespace OnlineShop.Order.Domain.Tests.OrderItems;

public class OrderItemsCreateFromShoppingCartTestData : TheoryData<string, List<ShoppingCartCheckoutItem>>
{
    public OrderItemsCreateFromShoppingCartTestData()
    {
        Add("orderId1", new List<ShoppingCartCheckoutItem>
        {
            new(new Quantity(2, Unit.Kg), 
                new Money(12, Currency.Pln, Unit.Kg), 
                new Money(24, Currency.Pln),
                "productId1", "productName1")
        });
    }
}