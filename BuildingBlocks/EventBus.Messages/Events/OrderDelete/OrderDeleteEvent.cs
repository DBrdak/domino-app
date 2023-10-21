using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Domain.Events.OrderDelete
{
    public sealed record OrderDeleteEvent(string ShopId, string OrderId)
    {
    }
}
