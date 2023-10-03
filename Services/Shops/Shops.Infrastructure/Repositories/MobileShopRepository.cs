using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shops.Domain.MobileShops;

namespace Shops.Infrastructure.Repositories
{
    public sealed class MobileShopRepository : IMobileShopRepository
    {
        public async Task<List<SalePoint>> GetSalePoints(CancellationToken cancellationToken)
        {
            return null;
        }
    }
}