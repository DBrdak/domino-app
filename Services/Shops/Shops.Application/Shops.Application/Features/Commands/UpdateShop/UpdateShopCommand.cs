﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Domain.Abstractions.Messaging;
using Shops.Domain.Abstractions;
using Shops.Domain.Shared;

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