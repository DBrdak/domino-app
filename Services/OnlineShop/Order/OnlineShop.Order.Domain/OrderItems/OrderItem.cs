using System.Collections;
using System.Runtime.InteropServices.JavaScript;
using Shared.Domain.Abstractions;
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

    public static IReadOnlyList<OrderItem> CreateItemList(
        string orderId,
        Quantity[] quantities,
        Money[] prices,
        string[] productsNames,
        Money[] totalValues)
    {
        if (AllArraysLengthEqual(new List<object[]>() { quantities, prices, productsNames, totalValues }))
        {
            throw new ArgumentException("All property values arrays must have the same length.");
        }

        var items = new List<OrderItem>();
        var count = quantities.Length;

        for (int i = 0; i < count; i++)
        {
            items.Add(new(orderId, quantities[i], prices[i], productsNames[i], totalValues[i]));
        }

        if (items.Count != count)
        {
            throw new ApplicationException("Order items cannot be mapped");
        }

        return items;
    }

    private static bool AllArraysLengthEqual(List<object[]> arrays)
    {
        var expectedLength = arrays[0].Length;

        return arrays.All(a => a.Length == expectedLength);
    }
}