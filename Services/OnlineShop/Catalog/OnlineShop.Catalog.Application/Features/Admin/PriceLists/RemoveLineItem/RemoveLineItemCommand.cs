﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineShop.Catalog.Application.Abstractions.Messaging;
using OnlineShop.Catalog.Domain.PriceLists;

namespace OnlineShop.Catalog.Application.Features.Admin.PriceLists.RemoveLineItem
{
    public sealed record RemoveLineItemCommand(
        string PriceListId,
        string LineItemName) : ICommand<PriceList>;
}