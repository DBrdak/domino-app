﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Domain.Abstractions;

namespace OnlineShop.Catalog.Domain.PriceLists.Events
{
    public sealed record LineItemDeletedDomainEvent(string ProductId) : IDomainEvent;
}