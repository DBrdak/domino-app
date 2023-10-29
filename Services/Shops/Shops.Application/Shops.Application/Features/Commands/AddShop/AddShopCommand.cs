using Shared.Domain.Abstractions.Messaging;
using Shops.Domain.Abstractions;

namespace Shops.Application.Features.Commands.AddShop
{
    public sealed record AddShopCommand(
        string ShopName,
        MobileShopDto? MobileShopData,
        StationaryShopDto? StationaryShopData
        ) : ICommand<Shop>;
}