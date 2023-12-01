using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using Shops.Application.Features.Commands.DeleteShop;
using Shops.Domain.Shops;

namespace Shops.IntegrationTests.FeatureTests.CommandTests
{
    public class DeleteShopTests : BaseIntegrationTest
    {
        public DeleteShopTests(IntegrationTestWebAppFactory factory) : base(factory)
        {
        }

        [Fact]
        public async Task DeleteShop_ValidData_ShouldDelete()
        {
            // Arrange
            var shopId = (await Context.Shops.FindAsync(FilterDefinition<Shop>.Empty)).First().Id;
            var command = new DeleteShopCommand(shopId);

            // Act
            var result = await Sender.Send(command);
            var isShopDeleted = (await Context.Shops.FindAsync(s => s.Id == shopId)).FirstOrDefault() is null;

            // Assert
            Assert.True(result.IsSuccess);
            Assert.True(isShopDeleted);
        }

        [Fact]
        public async Task DeleteShop_InvalidData_ShouldFail()
        {
            // Arrange
            var command = new DeleteShopCommand("Wrong Id");

            // Act
            var result = await Sender.Send(command);

            // Assert
            Assert.False(result.IsSuccess);
        }
    }
}
