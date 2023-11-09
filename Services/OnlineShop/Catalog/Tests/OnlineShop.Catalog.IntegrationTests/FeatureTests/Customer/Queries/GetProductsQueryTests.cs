using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineShop.Catalog.Application.Features.Customer.Queries.GetProducts;

namespace OnlineShop.Catalog.IntegrationTests.FeatureTests.Customer.Queries
{
    public class GetProductsQueryTests : BaseIntegrationTest
    {
        [Fact]
        public async void GetProductsAsCustomer_ValidQuery_ShouldReturnListOfProducts()
        {
            var command = new GetProductsQuery("Mięso");

            var products = await Sender.Send(command);

            Assert.NotNull(products);
        }

        public GetProductsQueryTests(IntegrationTestWebAppFactory factory) : base(factory)
        {
        }
    }
}
