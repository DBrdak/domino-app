using Shared.Domain.DateTimeRange;
using Shared.Domain.Location;

namespace IntegrationEvents.Domain.Events.OrderCreate
{
    public sealed record OrderCreateEvent(Location DeliveryLocation, DateTimeRange DeliveryDate, string OrderId)
    {
    }
}
