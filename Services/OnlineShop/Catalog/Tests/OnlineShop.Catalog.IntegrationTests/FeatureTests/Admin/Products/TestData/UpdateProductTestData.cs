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
        internal class UpdateProductValidTestData : TheoryData<string, bool, decimal?, bool>
        {
            public UpdateProductValidTestData()
            {
                Add("new description", true, 15.3m, false);
                Add("", true, 15.3m, true);
                Add("", false, null, true);
                Add("new description", false, null, true);
            }
        }
    }
}
