using Shared.Domain.Abstractions.Messaging;
using Shops.Domain.Shops;

namespace Shops.Application.Features.Commands.AddShop
{
    public sealed record AddShopCommand(
        string ShopName,
        MobileShopDto? MobileShopData,
        StationaryShopDto? StationaryShopData
        ) : ICommand<Shop>;
}