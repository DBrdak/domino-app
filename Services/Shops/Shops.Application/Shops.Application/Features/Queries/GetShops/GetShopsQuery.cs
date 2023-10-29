using Shared.Domain.Abstractions.Messaging;
using Shops.Domain.Abstractions;

namespace Shops.Application.Features.Queries.GetShops
{
    public sealed record GetShopsQuery() : IQuery<List<Shop>>;
}