using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Domain.Money
{
    public sealed record Unit
    {
        public string Code { get; init; }

        internal static readonly Unit None = new("");
        public static readonly Unit Kg = new("kg");
        public static readonly Unit Pcs = new("szt");

        public static readonly IReadOnlyCollection<Unit> All = new[]
        {
            Kg,
            Pcs
        };

        public Unit()
        { }

        private Unit(string code) => Code = code;

        public Unit AlternativeUnit() => this == Kg ? Pcs : Kg;

        public static Unit FromCode(string code)
        {
            return All.FirstOrDefault(u => u.Code.ToLower() == code.ToLower()) ??
                   throw new ApplicationException("The unit code is invalid");
        }
    }
}