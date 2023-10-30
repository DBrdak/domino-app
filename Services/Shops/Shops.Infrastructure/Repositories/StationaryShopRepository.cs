using MongoDB.Driver;
using Shops.Domain.Abstractions;
using Shops.Domain.StationaryShops;

namespace Shops.Infrastructure.Repositories
{
    public sealed class StationaryShopRepository : IStationaryShopRepository
    {
        private readonly ShopsContext _context;

        public StationaryShopRepository(ShopsContext context)
        {
            _context = context;
        }

        public async Task<List<StationaryShop>> GetStationarySalePoints(CancellationToken cancellationToken)
        {
            var filter = Builders<Shop>.Filter.Where(s => s is StationaryShop);

            var stationaryShops = await (await _context.Shops.FindAsync(filter, new FindOptions<Shop, StationaryShop>(), cancellationToken))
                .ToListAsync(cancellationToken);

            return stationaryShops;
        }
    }
}