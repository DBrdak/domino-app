using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineShop.Catalog.Application.Abstractions.Messaging;

namespace OnlineShop.Catalog.Application.Features.Admin.PriceLists.AddPriceList
{
    public sealed record AddBusinessPriceListCommand(
        string Name,
        string ContractorName) : ICommand;
}