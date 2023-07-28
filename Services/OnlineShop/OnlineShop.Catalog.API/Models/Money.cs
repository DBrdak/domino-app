namespace OnlineShop.Catalog.API.Models;

public sealed record Money(decimal Amount, string Currency = "PLN", string Unit = "kg");