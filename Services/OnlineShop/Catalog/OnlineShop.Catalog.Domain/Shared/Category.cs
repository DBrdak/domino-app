﻿namespace OnlineShop.Catalog.Domain.Shared
{
    public sealed record Category
    {
        public string Value { get; init; }
        private readonly string _engValue;

        private Category(string value, string engValue)
        {
            Value = value;
            _engValue = engValue;
        }

        public static readonly Category Meat = new("Mięso", "Meat");
        public static readonly Category Sausage = new("Wędlina", "Sausage");

        public static readonly IReadOnlyCollection<Category> All = new[]
        {
            Meat, Sausage
        };

        public static Category FromValue(string value)
            => All.FirstOrDefault(c => c.Value.ToLower() == value.ToLower() ||
                                       c._engValue.ToLower() == value.ToLower()) ??
               throw new ApplicationException($"Category {value} not found");
    }
}