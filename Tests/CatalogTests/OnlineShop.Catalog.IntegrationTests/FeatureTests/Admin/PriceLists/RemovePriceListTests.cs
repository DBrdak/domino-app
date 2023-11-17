using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using OnlineShop.Catalog.Application.Features.Admin.PriceLists.Commands.RemovePriceList;
using OnlineShop.Catalog.Domain.PriceLists;
using Shared.Domain.Errors;

namespace OnlineShop.Catalog.IntegrationTests.FeatureTests.Admin.PriceLists
{
    public class RemovePriceListTests : BaseIntegrationTest
    {
        public RemovePriceListTests(IntegrationTestWebAppFactory factory) : base(factory)
        {
        }

        [Fact]
        public async Task RemovePriceList_ValidData_ShouldRemove()
        {
            // Arrange
            var priceListId = (await Context.PriceLists.FindAsync(pl => pl.Contractor != Contractor.Retail)).ToList()[0].Id;
            var initCount = await Context.PriceLists.EstimatedDocumentCountAsync();
            var command = new RemovePriceListCommand(priceListId);

            // Act
            var result = await Sender.Send(command);
            var endCount = await Context.PriceLists.EstimatedDocumentCountAsync();
            var isSuccessfullyRemoved = endCount == initCount - 1;

            // Assert
            Assert.True(isSuccessfullyRemoved);
            Assert.True(result.IsSuccess);
            Assert.False(result.IsFailure);
            Assert.Equal(result.Error, Error.None);
        }

        [Fact]
        public async Task RemovePriceList_InvalidData_ShouldThrowOnRetailRemove()
        {
            // Arrange
            var priceListId = (await Context.PriceLists.FindAsync(pl => pl.Contractor == Contractor.Retail)).ToList()[0].Id;
            var initCount = await Context.PriceLists.EstimatedDocumentCountAsync();
            var command = new RemovePriceListCommand(priceListId);

            // Act
            var result = await Sender.Send(command);
            var endCount = await Context.PriceLists.EstimatedDocumentCountAsync();
            var isNotRemoved = endCount == initCount;

            // Assert
            Assert.True(isNotRemoved);
            Assert.False(result.IsSuccess);
            Assert.True(result.IsFailure);
            Assert.NotEqual(result.Error, Error.None);
        }
    }
}
