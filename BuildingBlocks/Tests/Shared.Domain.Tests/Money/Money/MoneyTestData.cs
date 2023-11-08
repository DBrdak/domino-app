using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Domain.Tests.Money.Money
{
    public class MoneyConstructorValidTestData : TheoryData<decimal, Domain.Money.Currency, Domain.Money.Unit?>
    {
        public MoneyConstructorValidTestData()
        {
            Add(12, Domain.Money.Currency.Pln, Domain.Money.Unit.Kg);
            Add(24.5m, Domain.Money.Currency.Pln, Domain.Money.Unit.Pcs);
            Add(24.5m, Domain.Money.Currency.Pln, null);
        }
    }
}
