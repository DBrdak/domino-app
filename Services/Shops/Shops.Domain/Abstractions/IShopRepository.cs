using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Domain.ResponseTypes;
using Shops.Domain.MobileShops;
using Shops.Domain.Shared;
using Shops.Domain.StationaryShops;

namespace Shops.Domain.Abstractions
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