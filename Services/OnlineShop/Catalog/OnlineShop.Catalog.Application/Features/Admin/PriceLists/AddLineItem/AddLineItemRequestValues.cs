using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Domain.Money;

namespace OnlineShop.Catalog.Application.Features.Admin.PriceLists.AddLineItem
{
    public sealed record AddLineItemRequestValues(string LineItemName, Money Price);
}