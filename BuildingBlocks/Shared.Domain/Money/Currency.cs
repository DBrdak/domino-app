using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Domain.Money
{
    public sealed record Currency
    {
        internal static readonly Currency None = new("");
        public static readonly Currency Pln = new("PLN");

        private Currency(string code) => Code = code;

        public string Code { get; init; }

        public static Currency FromCode(string code)
        {
            return All.FirstOrDefault(c => c.Code == code) ??
                   throw new ApplicationException("The currency code is invalid");
        }

        public static readonly IReadOnlyCollection<Currency> All = new[]
        {
            Pln
        };
    }
}