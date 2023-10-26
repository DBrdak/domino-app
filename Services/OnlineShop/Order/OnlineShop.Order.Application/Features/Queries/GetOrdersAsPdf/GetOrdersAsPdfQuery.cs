using Shared.Domain.Abstractions.Messaging;

namespace OnlineShop.Order.Application.Features.Queries.GetOrdersAsPdf
{
    public sealed record GetOrdersAsPdfQuery(string[] OrdersId) : IQuery<byte[]>;
}
