using System.Globalization;
using System.Text.RegularExpressions;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Shared.Domain.Exceptions;

namespace Shared.Domain.Money
{
    public sealed record Money
    {
        [BsonRepresentation(BsonType.Decimal128)]
        public decimal Amount { get; init; }

        public Currency Currency { get; init; }
        public Unit? Unit { get; init; }

        private const string moneyFormatRegex = 
            "^\\d+(\\.\\d+)?\\s*[Pp][Ll][nN]\\/[Kk][gG]$|[Pp][Ll][nN]\\/[Ss][zZ][tT]$|[Zz][Łł]\\/[Ss][Zz][Tt]$|[Zz][Łł]\\/[Kk][gG]$|[Zz][Łł]$|[Pp][Ll][Nn]$";
        
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
                throw new DomainException<Money>("Currencies have to be equal");
            }

            if (first.Unit != second.Unit)
            {
                throw new DomainException<Money>("Units have to be equal");
            }

            return new Money(first.Amount + second.Amount, first.Currency, first.Unit);
        }
        
        public static Money operator +(Money first, decimal secondAmount) =>
            new (first.Amount + secondAmount, first.Currency, first.Unit);
        
        public static Money operator -(Money first, decimal secondAmount) =>
            new(first.Amount - secondAmount, first.Currency, first.Unit);
        
        
        public static Money operator /(Money first, decimal secondAmount) =>
            new(first.Amount / secondAmount, first.Currency, first.Unit);
        
        
        public static Money operator *(Money first, decimal secondAmount) =>
            new(first.Amount * secondAmount, first.Currency, first.Unit);

        public override string ToString() => Unit is null ? 
            $"{Amount} {Currency.Code}" :
            $"{Amount} {Currency.Code}/{Unit.Code}";

        public static Money FromString(string moneyString)
        {
            ValidateMoneyString(ref moneyString);
            
            var amount = decimal.Parse(moneyString.Split(' ')[0], NumberStyles.Number, new CultureInfo("en"));
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

        private static void ValidateMoneyString(ref string moneyString)
        {
            moneyString = ReformatSlightlyInvalidMoneyString(moneyString);
            var isValid = Regex.IsMatch(moneyString, moneyFormatRegex);

            if (!isValid)
            {
                throw new DomainException<Money>($"{moneyString} is invalid string format for String -> Money convert");
            }
        }
        
        private static string ReformatSlightlyInvalidMoneyString(string moneyString)
        {
            moneyString = moneyString.Replace(',', '.');
            var isSlightlyInvalid = moneyString.IndexOf(' ') == -1;

            if (!isSlightlyInvalid)
            {
                return moneyString;
            }
            
            for (int i = 0; i < moneyString.Length - 1; i++)
            {
                if (char.IsDigit(moneyString[i]) && char.IsLetter(moneyString[i + 1]))
                {
                    moneyString = moneyString.Insert(i + 1, " ");
                }
            }

            return moneyString;
        }

        public void Deconstruct(out decimal Amount, out Currency Currency, out Unit? Unit)
        {
            Amount = this.Amount;
            Currency = this.Currency;
            Unit = this.Unit;
        }
    }
}