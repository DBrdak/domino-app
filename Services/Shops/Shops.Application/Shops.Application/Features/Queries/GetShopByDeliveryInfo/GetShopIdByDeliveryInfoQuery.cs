using Shared.Domain.Abstractions.Messaging;
using Shared.Domain.DateTimeRange;
using Shared.Domain.Location;

namespace Shops.Application.Features.Queries.GetShopByDeliveryInfo
{
    public sealed record GetShopIdByDeliveryInfoQuery(Location DeliveryLocation, DateTimeRange DeliveryDate) : IQuery<string>
    {
    }
}
