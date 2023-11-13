using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineShop.Catalog.Application.Features.Admin.PriceLists.Queries.GetPriceLists;
using OnlineShop.Catalog.Application.Features.Customer.Queries.GetProducts;
using Shared.Domain.Errors;

namespace OnlineShop.Catalog.IntegrationTests.FeatureTests.Admin.PriceLists
{
    public class GetPriceListsTests : BaseIntegrationTest
    {
        public GetPriceListsTests(IntegrationTestWebAppFactory factory) : base(factory)
        {
        }

        [Fact]
        public async Task GetPriceLists_ShoudlReturnAllPriceLists()
        {
            // Arrange
            var query = new GetPriceListsQuery();

            // Act
            var result = await Sender.Send(query);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.False(result.IsFailure);
            Assert.Equal(result.Error, Error.None);
            Assert.NotEmpty(result.Value);
        }
    }
}
