using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using MongoDB.Driver;
using OnlineShop.Catalog.Application.Features.Admin.PriceLists.Commands.AddBusinessPriceList;
using Shared.Domain.Errors;

namespace OnlineShop.Catalog.IntegrationTests.FeatureTests.Admin.PriceLists
{
    public class AddBusinessPriceListTests : BaseIntegrationTest
    {
        public AddBusinessPriceListTests(IntegrationTestWebAppFactory factory) : base(factory)
        {
        }


        [Fact]
        public async Task AddBusinessPriceList_ValidData_ShouldCreateAndAddToDb()
        {
            // Arrange
            var command = new AddBusinessPriceListCommand("example name 1", "example contractor 1", "Meat");

            // Act
            var result = await Sender.Send(command);
            var isAppendedToDb = (await Context.PriceLists.FindAsync(p => p.Name == command.Name)).FirstOrDefault() is not null;

            // Assert
            Assert.True(result.IsSuccess);
            Assert.False(result.IsFailure);
            Assert.Equal(result.Error, Error.None);
            Assert.True(isAppendedToDb);

            CleanAndSeedDatabase();
        }

        [Theory]
        [InlineData("", "example contractor 2", "Meat")]
        [InlineData("example name 2", "", "Meat")]
        [InlineData("example name 3", "example contractor 3", "")]
        public async Task AddBusinessPriceList_InvalidData_ShouldThrowValidationException(string name, string contractor, string category)
        {
            // Arrange
            var command = new AddBusinessPriceListCommand(name, contractor, category);

            // Act
            var addFunc = async () => await Sender.Send(command);

            // Assert
            await Assert.ThrowsAsync<ValidationException>(addFunc);

            CleanAndSeedDatabase();
        }
    }
}
