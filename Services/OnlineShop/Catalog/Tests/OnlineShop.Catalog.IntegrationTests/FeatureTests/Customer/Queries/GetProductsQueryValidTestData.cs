using OnlineShop.Catalog.Application.Features.Customer.Queries.GetProducts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Catalog.IntegrationTests.FeatureTests.Customer.Queries
{
    internal class GetProductsQueryValidTestData : TheoryData<GetProductsQuery>
    {
        public GetProductsQueryValidTestData()
        {
            Add(new ("Mięso"));

            Add(new ("Meat", 2));

            Add(new ("Wędlina", SortBy: "Price"));

            Add(new ("Wędlina", SortBy: "Price", SortOrder: "desc"));

            Add(new ("Sausage", PageSize: 20));

            Add(new ("Sausage", SearchPhrase: "3"));

            Add(new ("Wędlina", MinPrice: 10, MaxPrice: 20));

            Add(new ("Meat", IsAvailable: true));

            Add(new ("Mięso", IsDiscounted: true));

            Add(new ("Meat", 3, "desc", "Name", 15, "1", 15, 26, true, false));
        }
    }

    internal class GetProductsQueryInvalidTestData : TheoryData<GetProductsQuery>
    {
        public GetProductsQueryInvalidTestData()
        {
            Add(new("Meat", -2));

            Add(new("Wędlina", SortBy: ""));

            Add(new("Wędlina", SortBy: "Price", SortOrder: ""));

            Add(new("Sausage", PageSize: -20));

            Add(new("Wędlina", MinPrice: -10, MaxPrice: -9));

            Add(new("Wędlina", MinPrice: 10, MaxPrice: 9));
        }
    }
}
