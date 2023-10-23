using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Domain.DateTimeRange;
using Shared.Domain.Location;

namespace EventBus.Domain.Events.OrderCreate
{
    public sealed record OrderCreateEvent(Location DeliveryLocation, DateTimeRange DeliveryDate, string OrderId)
    {
    }
}
