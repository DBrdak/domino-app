using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shops.Domain.Abstractions;

namespace Shops.Infrastructure.Repositories
{
    public sealed class ShopRepository : IShopRepository
    {
        public async Task<List<Shop>> GetShops(CancellationToken cancellationToken)
        {
            return null;
        }

        public async Task<Shop?> AddShop(Shop newShop, CancellationToken cancellationToken)
        {
            return null;
        }

        public async Task<Shop?> UpdateShop(Shop updatedShop, CancellationToken cancellationToken)
        {
            return null;
        }

        public async Task<bool> DeleteShop(string requestShopId, CancellationToken cancellationToken)
        {
            return false;
        }
    }
}