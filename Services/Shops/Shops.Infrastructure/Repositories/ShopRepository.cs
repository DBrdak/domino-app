using MongoDB.Driver;
using Shops.Domain.MobileShops;
using Shops.Domain.Shops;
using Shops.Domain.StationaryShops;

namespace Shops.Infrastructure.Repositories
{
    public sealed class ShopRepository : IShopRepository
    {
        private readonly ShopsContext _context;

        public ShopRepository(ShopsContext context)
        {
            _context = context;
        }

        public async Task<List<Shop>> GetShops(CancellationToken cancellationToken)
        {
            var mobileShopFilter = Builders<Shop>.Filter.Where(s => s is MobileShop);
            var stationaryShopFilter = Builders<Shop>.Filter.Where(s => s is StationaryShop);

            var mobileShops = await (await _context.Shops.FindAsync(
                    mobileShopFilter,
                    new FindOptions<Shop, MobileShop>(),
                    cancellationToken))
                .ToListAsync(cancellationToken);

            var stationaryShops = await (await _context.Shops.FindAsync(
                    stationaryShopFilter,
                    new FindOptions<Shop, StationaryShop>(),
                    cancellationToken))
                .ToListAsync(cancellationToken);

            var shops = new List<Shop>();

            shops.AddRange(mobileShops);
            shops.AddRange(stationaryShops);

            return shops.ToList();
        }

        public async Task<List<Shop>> GetShops(IEnumerable<string> shopsId, CancellationToken cancellationToken)
        {
            var mobileShopFilter = Builders<Shop>.Filter.Where(s => s is MobileShop && shopsId.Any(id => id == s.Id));
            var stationaryShopFilter = Builders<Shop>.Filter.Where(s => s is StationaryShop && shopsId.Any(id => id == s.Id));

            var mobileShops = await (await _context.Shops.FindAsync(
                    mobileShopFilter,
                    new FindOptions<Shop, MobileShop>(),
                    cancellationToken))
                .ToListAsync(cancellationToken);

            var stationaryShops = await (await _context.Shops.FindAsync(
                    stationaryShopFilter,
                    new FindOptions<Shop, StationaryShop>(),
                    cancellationToken))
                .ToListAsync(cancellationToken);

            var shops = new List<Shop>();

            shops.AddRange(mobileShops);
            shops.AddRange(stationaryShops);

            return shops.ToList();
        }

        public async Task<Shop?> AddShop(Shop newShop, CancellationToken cancellationToken)
        {
            var isDuplicate = await (await _context.Shops.FindAsync(s => 
                s.ShopName == newShop.ShopName, null, cancellationToken))
                .AnyAsync(cancellationToken);

            if (isDuplicate)
            {
                return null;
            }

            await _context.Shops.InsertOneAsync(newShop, null, cancellationToken);

            return newShop;
        }

        public async Task<Shop?> UpdateShop(Shop updatedShop, CancellationToken cancellationToken)
        {
            var updateResult = await _context.Shops.ReplaceOneAsync(
                s => s.Id == updatedShop.Id,
                updatedShop,
                new ReplaceOptions(),
                cancellationToken);

            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0 ?
                    updatedShop :
                    null;
        }

        public async Task<bool> DeleteShop(string requestShopId, CancellationToken cancellationToken)
        {
            var isValid = await CheckIfShopIsValidForDeletion(requestShopId, cancellationToken);

            if (!isValid)
            {
                return false;
            }

            var deletionResult = await _context.Shops.DeleteOneAsync(
                s => s.Id == requestShopId,
                null,
                cancellationToken);

            return deletionResult.IsAcknowledged && deletionResult.DeletedCount > 0;
        }

        private async Task<bool> CheckIfShopIsValidForDeletion(string requestShopId, CancellationToken cancellationToken)
        {
            var shop = await (await _context.Shops.FindAsync(
                s => s.Id == requestShopId,
                null,
                cancellationToken))
                .FirstOrDefaultAsync(cancellationToken);

            return !shop.OrdersId.Any();
        }
    }
}