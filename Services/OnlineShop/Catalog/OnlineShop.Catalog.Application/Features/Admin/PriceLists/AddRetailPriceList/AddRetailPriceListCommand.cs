using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineShop.Catalog.Application.Abstractions.Messaging;

namespace OnlineShop.Catalog.Application.Features.Admin.PriceLists.AddRetailPriceList
{
    public sealed record AddRetailPriceListCommand() : ICommand;
}