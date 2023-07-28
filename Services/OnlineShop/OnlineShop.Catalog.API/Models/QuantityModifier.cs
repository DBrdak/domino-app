namespace OnlineShop.Catalog.API.Models;

public class QuantityModifier
{
    public bool IsPcsAllowed { get; private set; }
    public decimal? KgPerPcs { get; private set; }

    public QuantityModifier()
    {
        IsPcsAllowed = false;
    }

    public QuantityModifier(decimal? kgPerPcs)
    {
        if (kgPerPcs == null)
            IsPcsAllowed = false;

        if (kgPerPcs.HasValue)
        {
            IsPcsAllowed = true;
            KgPerPcs = kgPerPcs.Value;
        }
    }
}