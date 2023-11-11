using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using OnlineShop.Catalog.Domain.Products;

namespace OnlineShop.Catalog.IntegrationTests.FeatureTests.Admin.Products.TestData
{
    internal class UpdateProductTestData
    {
        internal class UpdateProductValidTestData : TheoryData<int, string, bool, decimal?, bool>
        {
            public UpdateProductValidTestData()
            {
                Add(0, "new description", true, 15.3m, false);
                Add(1, "", true, 15.3m, true);
                Add(2, "", false, null, true);
                Add(3, "new description", false, null, true);
            }
        }

        internal class UpdateProductInvalidTestData : TheoryData<int, string, bool, decimal?, bool>
        {
            public UpdateProductInvalidTestData()
            {
                Add(0, "new description", false, 15.3m, false);
                Add(2, "", true, null, true);
            }
        }
    }
}
