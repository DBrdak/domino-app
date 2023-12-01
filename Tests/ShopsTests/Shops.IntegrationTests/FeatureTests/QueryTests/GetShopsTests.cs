using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Domain.Errors;
using Shops.Application.Features.Queries.GetShops;

namespace Shops.IntegrationTests.FeatureTests.QueryTests
{
    public class GetShopsTests : BaseIntegrationTest
    {
        public GetShopsTests(IntegrationTestWebAppFactory factory) : base(factory)
        {
        }

        [Fact]
        public async Task GetShops_ValidQuery_ShouldReturnShops()
        {
            // Arrange
            var query = new GetShopsQuery();

            // Act
            var result = await Sender.Send(query);

            // Assert
            Assert.NotEmpty(result.Value);
        }
    }
}
