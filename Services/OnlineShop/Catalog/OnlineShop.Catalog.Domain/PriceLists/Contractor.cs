using Shared.Domain.Money;

namespace OnlineShop.Catalog.Domain.PriceLists;

public sealed record Contractor
{
    public string Name { get; init; }

    private Contractor(string name) => Name = name;

    public static readonly Contractor Retail = new("Retail");
    public static Contractor Business(string name) => new(name);
}