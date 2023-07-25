namespace OnlineShop.Catalog.API.CustomTypes;

public sealed record Money(decimal Amount, string Currency = "PLN", string Unit = "kg");