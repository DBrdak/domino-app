using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using MongoDB.Driver;
using Shared.Domain.Location;
using Shops.Application.Features.Commands.AddShop;
using Shops.IntegrationTests.TestData;

namespace Shops.IntegrationTests.FeatureTests.CommandTests
{
    public class AddShopTests : BaseIntegrationTest
    {
        public AddShopTests(IntegrationTestWebAppFactory factory) : base(factory)
        {
        }

        [Fact]
        public async Task AddMobileShop_ValidData_ShouldAppendToDb()
        {
            // Arrange
            var command = new AddShopCommand("Test Shop", new MobileShopDto("WPN 45XS"), null);

            // Act
            var result = await Sender.Send(command);
            var isAddedToDb = (await Context.Shops.FindAsync(s => s.Id == result.Value.Id)).FirstOrDefault() is not null;

            // Assert
            Assert.True(isAddedToDb);
        }

        [Fact]
        public async Task AddStationaryShop_ValidData_ShouldAppendToDb()
        {
            // Arrange
            var command = new AddShopCommand("Test Shop", null, new StationaryShopDto(new Location("Test Location", "21.21", "51.51")));

            // Act
            var result = await Sender.Send(command);
            var isAddedToDb = (await Context.Shops.FindAsync(s => s.Id == result.Value.Id)).FirstOrDefault() is not null;

            // Assert
            Assert.True(isAddedToDb);
        }

        [Theory]
        [ClassData(typeof(AddShopTestInvalidData))]
        public async Task AddShop_InvalidData_ShouldFail(string shopName, MobileShopDto? mobileShopData, StationaryShopDto? stationaryShopData)
        {
            // Arrange
            var command = new AddShopCommand(shopName, mobileShopData, stationaryShopData);

            // Act
            var addFunc = async () => await Sender.Send(command);

            // Assert
            await Assert.ThrowsAsync<ValidationException>(addFunc);
        }
    }
}
