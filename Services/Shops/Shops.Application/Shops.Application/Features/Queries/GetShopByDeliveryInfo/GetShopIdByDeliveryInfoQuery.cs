using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Domain.Abstractions.Messaging;
using Shared.Domain.DateTimeRange;
using Shared.Domain.Location;

namespace Shops.Application.Features.Queries.GetShopByDeliveryInfo
{
    public sealed record GetShopIdByDeliveryInfoQuery(Location DeliveryLocation, DateTimeRange DeliveryDate) : IQuery<string>
    {
    }
}
