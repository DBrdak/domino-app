using System.Text.Json.Serialization;
using EventBus.Domain.Events.ShoppingCartCheckout;
using Shared.Domain.Abstractions.Entities;
using Shared.Domain.Money;
using Shared.Domain.Quantity;

namespace OnlineShop.Order.Domain.OrderItems;

public sealed class OrderItem : Entity
{
    public Quantity Quantity { get; init; }
    public Money Price { get; init; }
    public string ProductName { get; init; }
    public Money TotalValue { get; init; }
    public string OrderId { get; init; }

    [JsonConstructor]
    public OrderItem(
        Quantity quantity,
        Money price,
        string productName,
        Money totalValue,
        string orderId,
        string id)
        : base(id)
    {
        Quantity = quantity;
        Price = price;
        ProductName = productName;
        TotalValue = totalValue;
        OrderId = orderId;
    }

    private OrderItem(
        Quantity quantity,
        Money price,
        string productName,
        Money totalValue, 
        string orderId) 
        : base(Guid.NewGuid().ToString())
    {
        Quantity = quantity;
        Price = price;
        ProductName = productName;
        TotalValue = totalValue;
        OrderId = orderId;
    }

    private OrderItem() : base(Guid.NewGuid().ToString())
    {}

    public static List<OrderItem> CreateFromShoppingCartItems(string orderId, List<ShoppingCartCheckoutItem> shoppingCartItems)
    {
        return shoppingCartItems.Select(i =>
            new OrderItem(
                    i.Quantity,
                    i.Price,
                    i.ProductName,
                    i.TotalValue,
                    orderId)).ToList();
    }
}