using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Domain.Money;

namespace OnlineShop.Catalog.IntegrationTests.FeatureTests.Admin.PriceLists.TestData
{
    internal class PriceListTestData
    {
        internal class AddLineItemValidTestData : TheoryData<int, string, Money>
        {
            public AddLineItemValidTestData()
            {
                Add(0, "new item 1", new Money(12.5m, Currency.Pln, Unit.Kg));
                Add(1, "new item 2", new Money(9.9m, Currency.Pln, Unit.Pcs));
            }
        }

        internal class AddLineItemInvalidTestData : TheoryData<int, string, Money?>
        {
            public AddLineItemInvalidTestData()
            {
                Add(0, "", new Money(12.5m, Currency.Pln, Unit.Kg));
                Add(1, "new item 3", null);
            }
        }
    }
}
