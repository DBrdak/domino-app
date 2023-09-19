using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Domain.Abstractions.Messaging;
using Shops.Domain.Shops;

namespace Shops.Application.Features.Commands.UpdateShop
{
    public sealed record UpdateShopCommand(
        string Name) : ICommand<Shop>;
}