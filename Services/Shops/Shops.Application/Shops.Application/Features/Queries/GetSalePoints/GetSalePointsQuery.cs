using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Domain.Abstractions.Messaging;
using Shops.Domain.Shops;

namespace Shops.Application.Features.Queries.GetSalePoints
{
    public sealed record GetSalePointsQuery : IQuery<List<SalePoint>>;
}