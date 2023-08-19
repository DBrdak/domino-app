using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Domain.Money;

namespace Shared.Domain.Quantity
{
    public sealed record Quantity(decimal Value, Unit Unit)
    {
        public static Quantity operator +(Quantity first, Quantity second)
        {
            if (first.Unit != second.Unit)
            {
                throw new InvalidOperationException("Units have to be equal");
            }

            return new Quantity(first.Value + second.Value, first.Unit);
        }

        public static Quantity Zero() => new(0, Unit.None);

        public static Quantity Zero(Unit unit) => new(0, unit);

        public bool IsZero() => this == Zero(Unit);
    }
}