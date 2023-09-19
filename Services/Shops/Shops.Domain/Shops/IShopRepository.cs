using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shops.Domain.Shops
{
    public interface IShopRepository
    {
        Task<List<SalePoint>> GetSalePoints(CancellationToken cancellationToken);
    }
}