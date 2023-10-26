using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Domain.Events.OrderShopQuery
{
    public sealed record OrderShopQueryEvent(IEnumerable<string> ShopsId)
    {
    }
}
