namespace Order.Domain.Common;

public class OrderItem
{
    public decimal Quantity { get; set; }
    public string Unit { get; set; }
    public string ProductName { get; set; }
    public string IsConfirmed { get; set; }
}