namespace Shared.Domain.Money
{
    public sealed record Currency
    {
        internal static readonly Currency None = new("");
        public static readonly Currency Pln = new("PLN");

        public Currency()
        { }

        private Currency(string code) => Code = code;

        public string Code { get; init; }

        public static Currency FromCode(string code)
        {
            return All.FirstOrDefault(c => c.Code.ToLower() == code.ToLower()) ??
                   throw new ApplicationException("The currency code is invalid");
        }

        public static readonly IReadOnlyCollection<Currency> All = new[]
        {
            Pln
        };
    }
}