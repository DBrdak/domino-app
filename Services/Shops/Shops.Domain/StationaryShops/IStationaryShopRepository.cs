namespace Shops.Domain.StationaryShops
{
    public interface IStationaryShopRepository
    {
        Task<List<StationaryShop>> GetStationarySalePoints(CancellationToken cancellationToken);
    }
}