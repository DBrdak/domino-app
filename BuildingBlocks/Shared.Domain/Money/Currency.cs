using Shared.Domain.Exceptions;

namespace Shared.Domain.Money
{
    public sealed record Currency
    {
        internal static readonly Currency None = new("");
        public static readonly Currency Pln = new("PLN");
        private static readonly Currency altPln = new("ZŁ");

        public Currency()
        { }

        private Currency(string code) => Code = code;

        public string Code { get; init; }

        public static Currency FromCode(string code)
        {
            var result = All.FirstOrDefault(c => c.Code.ToLower() == code.ToLower()) ??
                   throw new DomainException<Currency>("The currency code is invalid");

            return result == altPln ? 
                    Pln : result;
        }

        public static readonly IReadOnlyCollection<Currency> All = new[]
        {
            Pln,
            altPln
        };
    }
}