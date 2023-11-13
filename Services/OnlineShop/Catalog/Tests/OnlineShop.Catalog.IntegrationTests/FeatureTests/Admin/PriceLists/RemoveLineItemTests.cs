using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using MongoDB.Bson;
using MongoDB.Driver;
using OnlineShop.Catalog.Application.Features.Admin.PriceLists.Commands.RemoveLineItem;
using OnlineShop.Catalog.Domain.PriceLists;
using Shared.Domain.Errors;

namespace OnlineShop.Catalog.IntegrationTests.FeatureTests.Admin.PriceLists
{
    public class RemoveLineItemTests : BaseIntegrationTest
    {
        public RemoveLineItemTests(IntegrationTestWebAppFactory factory) : base(factory)
        {
        }

        [Fact]
        public async Task RemoveLineItem_ValidData_ShouldRemoveLineItemAndAggregatedProduct()
        {
            // Arrange
            var priceList = (await Context.PriceLists.FindAsync(FilterDefinition<PriceList>.Empty)).ToList()[0];
            var priceListId = priceList.Id;
            var lineItemName = priceList.LineItems.FirstOrDefault(li => li.ProductId is not null)!.Name;
            var command = new RemoveLineItemCommand(priceListId, lineItemName);

            // Act
            var result = await Sender.Send(command);
            var isAggregatedProductRemoved = (await Context.Products.FindAsync(p => p.Name == lineItemName)).FirstOrDefault() is null;

            // Assert
            Assert.True(result.IsSuccess);
            Assert.False(result.IsFailure);
            Assert.Equal(result.Error, Error.None);
            Assert.True(result.Value.LineItems.All(li => li.Name != lineItemName));
            Assert.True(isAggregatedProductRemoved);
        }

        [Theory]
        [InlineData("", "")]
        [InlineData("", "id")]
        public async Task RemoveLineItem_InvalidData_ShouldThrow(string lineItemName, string priceListId)
        {
            // Arrange
            var command = new RemoveLineItemCommand(priceListId, lineItemName);

            // Act
            var removeFunc = async () => await Sender.Send(command);

            // Assert
            await Assert.ThrowsAsync<ValidationException>(removeFunc);
        }
    }
}
