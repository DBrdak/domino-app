using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineShop.Catalog.Application.Features.Admin.Products.Queries.GetProducts;
using OnlineShop.Catalog.Domain.Products;

namespace OnlineShop.Catalog.IntegrationTests.FeatureTests.Admin.Products
{
    public class GetProductsAdminTests : BaseIntegrationTest
    {
        public GetProductsAdminTests(IntegrationTestWebAppFactory factory) : base(factory)
        {
        }

        [Theory]
        [InlineData("")]
        [InlineData("prod")]
        public async Task GetProducts_ShouldReturnListOfProducts(string searchPhrase)
        {
            // Arrange
            var query = new GetProductsAdminQuery(searchPhrase);

            // Act
            var result = await Sender.Send(query);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
            Assert.False(result.IsFailure);
            Assert.NotNull(result.Value);
            Assert.IsType<List<Product>>(result.Value);
        }

    }
}
