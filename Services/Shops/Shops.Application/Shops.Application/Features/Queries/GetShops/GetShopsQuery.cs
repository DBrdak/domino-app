using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Domain.Abstractions.Messaging;
using Shops.Domain.Abstractions;

namespace Shops.Application.Features.Queries.GetShops
{
    public sealed record GetShopsQuery() : IQuery<List<Shop>>;
}