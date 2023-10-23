using Shops.Domain.MobileShops;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shops.Domain.StationaryShops
{
    public interface IStationaryShopRepository
    {
        Task<List<StationaryShop>> GetStationarySalePoints(CancellationToken cancellationToken);
    }
}