using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Domain.Results
{
    public sealed record ShopNameWithId(string Id, string Name);

    public sealed record OrderShopQueryResult(IEnumerable<ShopNameWithId> ShopNameWithId)
    {
    }
}
