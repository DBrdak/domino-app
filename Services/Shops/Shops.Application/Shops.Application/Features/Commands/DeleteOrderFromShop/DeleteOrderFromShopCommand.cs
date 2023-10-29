using Shared.Domain.Abstractions.Messaging;

namespace Shops.Application.Features.Commands.DeleteOrderFromShop
{
    public sealed record DeleteOrderFromShopCommand(string ShopId, string OrderId) : ICommand
    {
    }
}
