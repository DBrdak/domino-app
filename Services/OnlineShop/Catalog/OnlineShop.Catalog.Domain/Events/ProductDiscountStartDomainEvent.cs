using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Domain.Abstractions;
using Shared.Domain.Money;

namespace OnlineShop.Catalog.Domain.DomainEvents
{
    public sealed record ProductDiscountStartDomainEvent(string Id, Money NewPrice) : IDomainEvent
    {
    }
}