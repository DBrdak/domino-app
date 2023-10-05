using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Domain.Location;

namespace Shops.Application.Features.Commands.AddShop
{
    public sealed record StationaryShopDto(Location Location);
}