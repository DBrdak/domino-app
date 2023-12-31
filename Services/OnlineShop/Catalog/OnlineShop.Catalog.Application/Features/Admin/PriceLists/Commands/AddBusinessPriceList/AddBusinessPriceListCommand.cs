﻿using Shared.Domain.Abstractions.Messaging;

namespace OnlineShop.Catalog.Application.Features.Admin.PriceLists.Commands.AddBusinessPriceList
{
    public sealed record AddBusinessPriceListCommand(
        string Name,
        string ContractorName,
        string Category) : ICommand;
}