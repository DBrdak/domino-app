using Shared.Domain.Abstractions.Messaging;

namespace Shops.Application.Features.Commands.DeleteShop
{
    public sealed record DeleteShopCommand(string ShopId) : ICommand;
}