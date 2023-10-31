using Shared.Domain.Exceptions;
using Shared.Domain.Money;

namespace Shared.Domain.Quantity
{
    public sealed record Quantity
    {
        public Quantity(decimal value, Unit unit)
        {
            if (value < 0)
            {
                throw new DomainException<Quantity>("Value of quantity cannot be negative");
            }
            
            this.Value = value;
            this.Unit = unit;
        }

        public static Quantity operator +(Quantity first, Quantity second)
        {
            if (first.Unit != second.Unit)
            {
                throw new InvalidOperationException("Units have to be equal");
            }

            return new Quantity(first.Value + second.Value, first.Unit);
        }

        public decimal Value { get; init; }
        public Unit Unit { get; init; }

        public static Quantity Zero() => new(0, Unit.None);

        public static Quantity Zero(Unit unit) => new(0, unit);

        public bool IsZero() => this == Zero(Unit);

        public override string ToString() => $"{Value} {Unit.Code}";

        public void Deconstruct(out decimal value, out Unit unit)
        {
            value = this.Value;
            unit = this.Unit;
        }
    }
}