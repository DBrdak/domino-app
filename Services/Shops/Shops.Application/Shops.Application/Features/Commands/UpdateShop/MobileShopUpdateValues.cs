using Shops.Domain.MobileShops;

namespace Shops.Application.Features.Commands.UpdateShop;

public sealed record MobileShopUpdateValues(
    SalePoint? NewSalePoint,
    SalePoint? SalePointToRemove,
    SalePoint? SalePointToDisable,
    SalePoint? SalePointToEnable);