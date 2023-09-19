using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Domain.Abstractions.Entities;

namespace Shops.Domain.Shops
{
    public sealed class Shop : Entity
    {
        public static Shop Create()
        {
            return new Shop();
        }
    }
}