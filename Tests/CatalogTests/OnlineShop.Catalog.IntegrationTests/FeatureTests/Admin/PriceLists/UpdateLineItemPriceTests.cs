using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using MongoDB.Driver;
using OnlineShop.Catalog.Application.Features.Admin.PriceLists.Commands.UpdateLineItemPrice;
using OnlineShop.Catalog.Domain.PriceLists;
using Shared.Domain.Errors;
using Shared.Domain.Money;

namespace OnlineShop.Catalog.IntegrationTests.FeatureTests.Admin.PriceLists
{
    public class UpdateLineItemPriceTests : BaseIntegrationTest
    {
        public UpdateLineItemPriceTests(IntegrationTestWebAppFactory factory) : base(factory)
        {
        }

        [Fact]
        public async Task UpdateLineItemPrice_ValidData_ShouldUpdateLineItemPriceAndProductPriceIfAggregated()
        {
            // Arrange
            var price = 15.7m;
            var priceList = (await Context.PriceLists.FindAsync(FilterDefinition<PriceList>.Empty)).First();
            var lineItems = priceList.LineItems;
            var lineItemWithAggregatedProduct = lineItems.First(li => li.ProductId is not null);
            var lineItemWithoutAggregatedProduct = lineItems.First(li => li.ProductId is null);

            var commandAggregated = new UpdateLineItemPriceCommand(
                lineItemWithAggregatedProduct.Name,
                new Money(price, Currency.Pln, Unit.Kg),
                priceList.Id);
            var commandNonAggregated = new UpdateLineItemPriceCommand(
                lineItemWithoutAggregatedProduct.Name,
                new Money(price, Currency.Pln, Unit.Kg),
                priceList.Id);

            // Act
            var resultNonAggregated = await Sender.Send(commandNonAggregated);
            var resultAggregated = await Sender.Send(commandAggregated);
            var updatedLineItemWithAggregatedProduct =
                resultAggregated.Value.LineItems.First(li => li.Name == lineItemWithAggregatedProduct.Name);
            var updatedLineItemWithoutAggregatedProduct =
                resultNonAggregated.Value.LineItems.First(li => li.Name == lineItemWithoutAggregatedProduct.Name);
            var aggregatedProduct = (await
                Context.Products.FindAsync(p => p.Id == lineItemWithAggregatedProduct.ProductId)).First();

            // Assert
            Assert.True(resultAggregated.IsSuccess && resultNonAggregated.IsSuccess);
            Assert.False(resultAggregated.IsFailure && resultNonAggregated.IsFailure);
            Assert.Equal(new []{ resultAggregated.Error, resultNonAggregated.Error }, new [] { Error.None, Error.None, });
            Assert.True(resultAggregated.Value is not null && resultNonAggregated.Value is not null);
            Assert.Equal(aggregatedProduct.Price, updatedLineItemWithAggregatedProduct.Price);
            Assert.NotEqual(updatedLineItemWithAggregatedProduct.Price, lineItemWithAggregatedProduct.Price);
            Assert.NotEqual(updatedLineItemWithoutAggregatedProduct.Price, lineItemWithoutAggregatedProduct.Price);
            Assert.Equal(price, updatedLineItemWithAggregatedProduct.Price.Amount);
            Assert.Equal(price, updatedLineItemWithoutAggregatedProduct.Price.Amount);
        }

        [Fact]
        public async Task UpdateLineItemPrice_InvalidPriceData_ShouldThrow()
        {
            // Arrange
            var priceList = (await Context.PriceLists.FindAsync(FilterDefinition<PriceList>.Empty)).First();
            var lineItems = priceList.LineItems;
            var lineItem = lineItems.First();

            var command = new UpdateLineItemPriceCommand(
                lineItem.Name,
                new Money(15.2m, Currency.Pln),
                priceList.Id);

            // Act
            var updateFunc = async () => await Sender.Send(command);

            // Assert
            await Assert.ThrowsAsync<ValidationException>(updateFunc);
        }

        [Fact]
        public async Task UpdateLineItemPrice_InvalidLineItemNameData_ShouldThrow()
        {
            // Arrange
            var priceList = (await Context.PriceLists.FindAsync(FilterDefinition<PriceList>.Empty)).First();
            var lineItems = priceList.LineItems;
            var lineItem = lineItems.First();

            var command = new UpdateLineItemPriceCommand(
                "",
                new Money(15.2m, Currency.Pln, Unit.Kg),
                priceList.Id);

            // Act
            var updateFunc = async () => await Sender.Send(command);

            // Assert
            await Assert.ThrowsAsync<ValidationException>(updateFunc);
        }

        [Theory]
        [InlineData("notPriceListId")]
        [InlineData("")]
        public async Task UpdateLineItemPrice_InvalidPriceListId_ShouldThrow(string priceListId)
        {
            // Arrange
            var priceList = (await Context.PriceLists.FindAsync(FilterDefinition<PriceList>.Empty)).First();
            var lineItems = priceList.LineItems;
            var lineItem = lineItems.First();

            var command = new UpdateLineItemPriceCommand(
                lineItem.Name,
                new Money(15.2m, Currency.Pln, Unit.Kg),
                priceListId);

            // Act
            var updateFunc = async () => await Sender.Send(command);

            // Assert
            await Assert.ThrowsAsync<ValidationException>(updateFunc);
        }
    }
}
