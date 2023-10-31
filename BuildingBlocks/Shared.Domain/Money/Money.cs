using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Shared.Domain.Exceptions;

namespace Shared.Domain.Money
{
    public sealed record Money
    {
        [BsonRepresentation(BsonType.Double)]
        public decimal Amount { get; init; }

        public Currency Currency { get; init; }
        public Unit? Unit { get; init; }
        
        public Money(decimal Amount,
            Currency Currency, 
            Unit? Unit = null)
        {
            if (Amount < 0)
            {
                throw new DomainException<Money>("Amount of price cannot be negative");
            }
            
            this.Amount = Amount;
            this.Currency = Currency;
            this.Unit = Unit;
        }

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
        
        public static Money operator +(Money first, decimal secondAmount)
        {
            return new Money(first.Amount + secondAmount, first.Currency, first.Unit);
        }

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

            return new Money(amount, currency, unit) ?? throw new DomainException<Money>("Wrong price format");
        }

        public void Deconstruct(out decimal Amount, out Currency Currency, out Unit? Unit)
        {
            Amount = this.Amount;
            Currency = this.Currency;
            Unit = this.Unit;
        }
    }
}