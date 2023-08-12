using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Order.Domain.Common
{
    public sealed class Money
    {
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string Unit { get; set; }

        public Money(decimal amount, string currency = "PLN", string unit = "kg")
        {
            Amount = amount;
            Currency = currency;
            Unit = unit;
        }
    }
}