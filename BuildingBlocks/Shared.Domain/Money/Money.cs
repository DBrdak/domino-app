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

        public override string ToString() => Unit is null ? 
            $"{Amount} {Currency.Code}" :
            $"{Amount} {Currency.Code}/{Unit.Code}";

        public static Money FromString(string moneyString)
        {
            var amount = decimal.Parse(moneyString.Split(' ')[0]);
            var amountUnit = moneyString.Split(' ')[1].Split("/");
            var currencyCode = amountUnit[0];
            var currency = Currency.FromCode(currencyCode);

            Unit? unit = null;

            if (amountUnit.Length == 2)
            {
                var unitCode = amountUnit[1];
                unit = Unit.FromCode(unitCode);
            }

            return new Money(amount, currency, unit);
        }
    }
}