namespace OnlineShop.Catalog.API.Models;

public class QuantityModifier
{
    public bool IsPcsAllowed { get; set; }
    public decimal? KgPerPcs { get; set; }

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