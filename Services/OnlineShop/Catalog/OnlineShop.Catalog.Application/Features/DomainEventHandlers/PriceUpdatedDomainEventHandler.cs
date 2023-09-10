using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineShop.Catalog.Domain.PriceLists.Events;
using Shared.Domain.Abstractions;

namespace OnlineShop.Catalog.Application.Features.DomainEventHandlers
{
    internal sealed class PriceUpdatedDomainEventHandler : IDomainEventHandler<PriceUpdatedDomainEvent>
    {
        public async Task Handle(PriceUpdatedDomainEvent notification, CancellationToken cancellationToken)
        {
        }
    }
}