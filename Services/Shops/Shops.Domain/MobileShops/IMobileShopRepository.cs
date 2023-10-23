using Shops.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shops.Domain.MobileShops
{
    public interface IMobileShopRepository
    {
        Task<List<SalePoint>> GetSalePoints(CancellationToken cancellationToken);
    }
}