using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using Shared.Domain.DateTimeRange;
using Shared.Domain.Errors;
using Shared.Domain.Location;
using Shops.Application.Features.Queries.GetDeliveryPoints;
using Shops.Application.Features.Queries.GetShopByDeliveryInfo;

namespace Shops.IntegrationTests.FeatureTests.QueryTests
{
    public class GetShopByDeliveryPointTests : BaseIntegrationTest
    {
        public GetShopByDeliveryPointTests(IntegrationTestWebAppFactory factory) : base(factory)
        {
        }

        [Fact]
        public async Task GetShopByDeliveryPoint_ValidQuery_ShouldReturnShopWithGivenLocation()
        {
            // Arrange
            var deliveryPoint = (await Sender.Send(new GetDeliveryPointsQuery())).Value[0];
            var query = new GetShopIdByDeliveryInfoQuery(deliveryPoint.Location, deliveryPoint.PossiblePickupDate[0]);

            // Act
            var result = await Sender.Send(query);
            var shopInDb = (await Context.Shops.FindAsync(s => s.Id == result.Value)).First();

            // Assert
            Assert.NotNull(shopInDb);
        }

        [Fact]
        public async Task GetShopByDeliveryPoint_InvalidQuery_ShouldFail()
        {
            // Arrange
            var query = new GetShopIdByDeliveryInfoQuery(
                new Location("Wrong Location", "15.02", "13.02"), 
                new DateTimeRange(DateTime.UtcNow, DateTime.UtcNow.AddHours(3)));

            // Act
            var result = await Sender.Send(query);

            // Assert
            Assert.True(result.IsFailure);
            Assert.False(result.IsSuccess);
            Assert.NotEqual(result.Error, Error.None);
        }
    }
}
