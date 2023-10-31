using Shared.Domain.Exceptions;

namespace OnlineShop.Catalog.Domain.PriceLists;

public sealed record Contractor
{
    public string Name { get; init; }

    private Contractor(string name) => Name = name;

    public static readonly Contractor Retail = new("Retail");
    public static Contractor Business(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new DomainException<Contractor>("Contractor name is required");
        }
        
        return new(name);
    }
}