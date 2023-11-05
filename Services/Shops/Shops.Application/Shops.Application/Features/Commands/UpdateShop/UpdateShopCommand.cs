using Shared.Domain.Abstractions.Messaging;
using Shops.Domain.Shared;
using Shops.Domain.Shops;

namespace Shops.Application.Features.Commands.UpdateShop
{
    public sealed record UpdateShopCommand(
        string ShopToUpdateId,
        Seller? NewSeller,
        Seller? SellerToDelete,
        MobileShopUpdateValues? MobileShopUpdateValues,
        StationaryShopUpdateValues? StationaryShopUpdateValues
    ) : ICommand<Shop>;
}