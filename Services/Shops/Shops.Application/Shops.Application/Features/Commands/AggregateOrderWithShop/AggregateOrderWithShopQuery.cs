using Shared.Domain.Abstractions.Messaging;

namespace Shops.Application.Features.Commands.AggregateOrderWithShop
{
    public sealed record AggregateOrderWithShopCommand(string OrderId, string ShopId) : ICommand
    {
    }
}
