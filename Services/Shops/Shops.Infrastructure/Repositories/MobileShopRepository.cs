using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using Shops.Domain.Abstractions;
using Shops.Domain.MobileShops;

namespace Shops.Infrastructure.Repositories
{
    public sealed class MobileShopRepository : IMobileShopRepository
    {
        private readonly ShopsContext _context;

        public MobileShopRepository(ShopsContext context)
        {
            _context = context;
        }

        public async Task<List<SalePoint>> GetSalePoints(CancellationToken cancellationToken)
        {
            var filter = Builders<Shop>.Filter.Where(s => s is MobileShop);

            var mobileShops = await (await _context.Shops.FindAsync(filter, new FindOptions<Shop, MobileShop>(), cancellationToken))
                .ToListAsync(cancellationToken);

            var salePoints = mobileShops.SelectMany(ms => ms.SalePoints);

            return salePoints.ToList();
        }
    }
}