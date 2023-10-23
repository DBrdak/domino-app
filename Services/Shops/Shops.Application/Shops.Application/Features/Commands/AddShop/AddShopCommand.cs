using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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