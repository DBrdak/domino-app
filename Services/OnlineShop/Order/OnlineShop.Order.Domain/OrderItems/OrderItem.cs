using EventBus.Domain.Common;
using Shared.Domain.Abstractions.Entities;
using Shared.Domain.Money;
using Shared.Domain.Quantity;

namespace OnlineShop.Order.Domain.OrderItems;

public class OrderItem : Entity
{
    public string OrderId { get; init; }
    public Quantity Quantity { get; init; }
    public Money Price { get; init; }
    public string ProductName { get; init; }
    public Money TotalValue { get; init; }

    public OrderItem()
    { }

    private OrderItem(
        string orderId,
        Quantity quantity,
        Money price,
        string productName,
        Money totalValue)
        : base(Guid.NewGuid().ToString())
    {
        OrderId = orderId;
        Quantity = quantity;
        Price = price;
        ProductName = productName;
        TotalValue = totalValue;
    }

    public static List<OrderItem> CreateFromShoppingCartItems(string orderId, List<ShoppingCartCheckoutItem> shoppingCartItems)
    {
        return shoppingCartItems.Select(i =>
            new OrderItem(
                    orderId,
                    i.Quantity,
                    i.Price,
                    i.ProductName,
                    i.TotalValue)).ToList();
    }
}