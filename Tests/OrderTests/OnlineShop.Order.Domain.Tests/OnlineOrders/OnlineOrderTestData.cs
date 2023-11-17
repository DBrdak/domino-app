using IntegrationEvents.Domain.Events.ShoppingCartCheckout;
using OnlineShop.Order.Domain.OnlineOrders;
using Shared.Domain.DateTimeRange;
using Shared.Domain.Location;
using Shared.Domain.Money;
using Shared.Domain.Quantity;

namespace OnlineShop.Order.Domain.Tests.OnlineOrders;

public static class OnlineOrderTestData
{
    public static Func<OnlineOrder> TestOnlineOrder = () =>
    {
        var price1 = new Money(10.9m, Currency.Pln);
        var price2 = new Money(19m, Currency.Pln);
        var price3 = new Money(25.9m, Currency.Pln);
        var quantity1 = new Quantity(2.5m, Unit.Kg);
        var quantity2 = new Quantity(12, Unit.Pcs);
        var quantity3 = new Quantity(5.9m, Unit.Kg);
        var totalValue1 = price1 * quantity1.Value;
        var totalValue2 = price2 * quantity2.Value;
        var totalValue3 = price3 * quantity3.Value;
        var orderValue = totalValue1 + totalValue2 + totalValue3;
        var location = new Location("Sample Location", "latitude", "longitude");
        var dateTimeRange = new DateTimeRange(DateTime.Now, DateTime.Now.AddHours(1));
        var items = new List<ShoppingCartCheckoutItem>
        {
            new(quantity1, price1, totalValue1, "sampleProductId1", "Sample Product 1"),
            new(quantity2, price2, totalValue2, "sampleProductId2", "Sample Product 2"),
            new(quantity3, price3, totalValue3, "sampleProductId3", "Sample Product 3"),
        };

        var shoppingCart = new ShoppingCartCheckoutEvent(
            "sampleShoppingCartId",
            orderValue,
            items,
            "samplePhoneNumber",
            "sampleFirstName",
            "sampleLastName",
            location,
            dateTimeRange
        );

        return OnlineOrder.Create(shoppingCart);
    };
}

public class OrderCreateSuccessTestData : TheoryData<ShoppingCartCheckoutEvent>
{
    public OrderCreateSuccessTestData()
    {
        var price1 = new Money(10.9m, Currency.Pln);
        var price2 = new Money(19m, Currency.Pln);
        var price3 = new Money(25.9m, Currency.Pln);       
        var quantity1 = new Quantity(2.5m, Unit.Kg);
        var quantity2 = new Quantity(12, Unit.Pcs);
        var quantity3 = new Quantity(5.9m, Unit.Kg);
        var totalValue1 = price1 * quantity1.Value;
        var totalValue2 = price2 * quantity2.Value;
        var totalValue3 = price3 * quantity3.Value;
        var orderValue = totalValue1 + totalValue2 + totalValue3;
        var location = new Location("Sample Location", "latitude", "longitude");
        var dateTimeRange = new DateTimeRange(DateTime.Now, DateTime.Now.AddHours(1));
        var items = new List<ShoppingCartCheckoutItem>
        {
            new(quantity1, price1, totalValue1, "sampleProductId1", "Sample Product 1"),
            new(quantity2, price2, totalValue2, "sampleProductId2", "Sample Product 2"),
            new(quantity3, price3, totalValue3, "sampleProductId3", "Sample Product 3"),
        };

        Add(new ShoppingCartCheckoutEvent(
            "sampleShoppingCartId",
            orderValue,
            items,
            "samplePhoneNumber",
            "sampleFirstName",
            "sampleLastName",
            location,
            dateTimeRange
        ));
    }
}