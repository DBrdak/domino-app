namespace Shops.Domain.Shops
{
    public interface IShopRepository
    {
        Task<List<Shop>> GetShops(CancellationToken cancellationToken);
        Task<List<Shop>> GetShops(IEnumerable<string> shopsId,CancellationToken cancellationToken);

        Task<Shop?> AddShop(Shop newShop, CancellationToken cancellationToken);

        Task<Shop?> UpdateShop(Shop updatedShop, CancellationToken cancellationToken);

        Task<bool> DeleteShop(string requestShopId, CancellationToken cancellationToken);
    }
}