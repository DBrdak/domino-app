using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Domain.ResponseTypes;

namespace Shops.Domain.Shops
{
    public interface IShopRepository
    {
        Task<List<SalePoint>> GetSalePoints(CancellationToken cancellationToken);

        Task<List<Shop>> GetShops(CancellationToken cancellationToken);

        Task<Shop?> AddShop(Shop newShop, CancellationToken cancellationToken);

        Task<Shop?> UpdateShop(Shop updatedShop, CancellationToken cancellationToken);

        Task<bool> DeleteShop(string requestShopId, CancellationToken cancellationToken);
    }
}