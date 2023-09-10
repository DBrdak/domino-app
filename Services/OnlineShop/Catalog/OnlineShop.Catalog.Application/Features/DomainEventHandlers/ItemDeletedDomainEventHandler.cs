using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineShop.Catalog.Domain.PriceLists.Events;
using Shared.Domain.Abstractions;

namespace OnlineShop.Catalog.Application.Features.Admin.PriceLists
{
    internal class ItemDeletedDomainEventHandler : IDomainEventHandler<ItemDeletedDomainEvent>
    {
        public async Task Handle(ItemDeletedDomainEvent notification, CancellationToken cancellationToken)
        {
        }
    }
}