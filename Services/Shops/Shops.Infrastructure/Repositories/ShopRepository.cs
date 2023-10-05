using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using Shops.Domain.Abstractions;
using Shops.Domain.MobileShops;
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
            var deletionResult = await _context.Shops.DeleteOneAsync(
                s => s.Id == requestShopId,
                null,
                cancellationToken);

            return deletionResult.IsAcknowledged && deletionResult.DeletedCount > 0;
        }
    }
}