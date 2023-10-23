using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Shared.Domain.Money
{
    public sealed record Money(
        [property: BsonRepresentation(BsonType.Double)] decimal Amount,
        Currency Currency, 
        Unit? Unit = null)
    {
        public static Money operator +(Money first, Money second)
        {
            if (first.Currency != second.Currency)
            {
                throw new InvalidOperationException("Currencies have to be equal");
            }

            if (first.Unit != second.Unit)
            {
                throw new InvalidOperationException("Units have to be equal");
            }

            return new Money(first.Amount + second.Amount, first.Currency, first.Unit);
        }

        public static Money Zero() => new(0, Currency.None, Unit.None);

        public static Money Zero(Currency currency, Unit? unit) => new(0, currency, unit);

        public bool IsZero() => this == Zero(Currency, Unit);
    }
}