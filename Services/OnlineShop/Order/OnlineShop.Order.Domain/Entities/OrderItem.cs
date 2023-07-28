namespace OnlineShop.Order.Domain.Entities;

public class OrderItem
{
    public Guid Id { get; set; }
    public string OrderId { get; set; }
    public decimal Quantity { get; set; }
    public string Unit { get; set; }
    public string ProductName { get; set; }
    public string Status { get; set; } = "Oczekuje na potwierdzenie";
}