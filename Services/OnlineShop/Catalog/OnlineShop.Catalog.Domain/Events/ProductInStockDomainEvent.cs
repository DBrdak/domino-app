using Shared.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Catalog.Domain.DomainEvents
{
    public sealed record ProductInStockDomainEvent(string Id) : IDomainEvent
    {
    }
}