using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shops.Domain.StationaryShops;

namespace Shops.Infrastructure.Repositories
{
    public sealed class StationaryShopRepository : IStationaryShopRepository
    {
        public async Task<List<StationaryShop>> GetStationarySalePoints(CancellationToken cancellationToken)
        {
            return null;
        }
    }
}