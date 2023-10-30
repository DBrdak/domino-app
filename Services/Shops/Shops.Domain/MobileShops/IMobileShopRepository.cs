namespace Shops.Domain.MobileShops
{
    public interface IMobileShopRepository
    {
        Task<List<SalePoint>> GetSalePoints(CancellationToken cancellationToken);
    }
}