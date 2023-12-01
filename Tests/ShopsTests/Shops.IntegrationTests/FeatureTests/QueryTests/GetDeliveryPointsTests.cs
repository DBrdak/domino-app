using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shops.Application.Features.Queries.GetDeliveryPoints;

namespace Shops.IntegrationTests.FeatureTests.QueryTests
{
    public class GetDeliveryPointsTests : BaseIntegrationTest
    {
        public GetDeliveryPointsTests(IntegrationTestWebAppFactory factory) : base(factory)
        {
        }

        [Fact]
        public async Task GetDeliveryPoints_ValidQuery_ShouldReturnDeliveryPointsForToday()
        {
            // Arrange
            var query = new GetDeliveryPointsQuery();

            // Act
            var result = await Sender.Send(query);
            var pickupDates = result.Value.SelectMany(dp => dp.PossiblePickupDate);

            // Assert
            Assert.NotEmpty(result.Value);
            Assert.True(pickupDates.All(d => d.Start < DateTime.UtcNow.AddDays(7)));
        }
    }
}
