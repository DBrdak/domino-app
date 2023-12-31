﻿using Shared.Domain.Exceptions;

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
                   throw new DomainException<Unit>("The unit code is invalid");
        }
    }
}