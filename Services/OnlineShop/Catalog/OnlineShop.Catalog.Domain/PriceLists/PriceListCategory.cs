namespace OnlineShop.Catalog.Domain.PriceLists;

public record PriceListCategory
{
    public string Value { get; init; }

    private PriceListCategory(string value)
    {
        var isValid = value == Sausage.Value || value == Meat.Value;

        if(!isValid)
            throw new ApplicationException($"Invalid value [{value}] for PriceListCategory instance");

        Value = value;
    }

    public static readonly PriceListCategory Sausage = new("Cennik wędlin");

    public static readonly PriceListCategory Meat = new("Cennik mięsa");
    public static PriceListCategory FromValue(string value) => new(value);
}