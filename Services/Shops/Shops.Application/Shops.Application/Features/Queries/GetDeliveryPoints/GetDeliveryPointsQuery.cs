using Shared.Domain.Abstractions.Messaging;

namespace Shops.Application.Features.Queries.GetDeliveryPoints
{
    public sealed record GetDeliveryPointsQuery : IQuery<List<DeliveryPoint>>;
}