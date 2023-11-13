using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using MongoDB.Driver;
using OnlineShop.Catalog.Application.Features.Admin.PriceLists.Commands.AddLineItem;
using OnlineShop.Catalog.Domain.PriceLists;
using OnlineShop.Catalog.IntegrationTests.FeatureTests.Admin.PriceLists.TestData;
using Shared.Domain.Errors;
using Shared.Domain.Money;

namespace OnlineShop.Catalog.IntegrationTests.FeatureTests.Admin.PriceLists
{
    public class AddLineItemTests : BaseIntegrationTest
    {
        public AddLineItemTests(IntegrationTestWebAppFactory factory) : base(factory)
        {
        }

        [Theory]
        [ClassData(typeof(PriceListTestData.AddLineItemValidTestData))]
        public async Task AddLineItem_ValidData_ShouldAdd(int priceListIndex, string name, Money price)
        {
            // Arrange
            var priceListId =
                (await Context.PriceLists.FindAsync(pl => pl.Contractor == Contractor.Retail)).ToList()[priceListIndex].Id;
            var command = new AddLineItemCommand(priceListId, name, price);

            // Act
            var result = await Sender.Send(command);
            var isAddedToDb = (await Context.PriceLists.FindAsync(pl => pl.Id == priceListId && pl.LineItems.Any(li => li.Name == name))).FirstOrDefault() is not null;

            // Assert
            Assert.True(isAddedToDb);
            Assert.True(result.IsSuccess);
            Assert.False(result.IsFailure);
            Assert.Equal(result.Error, Error.None);
        }

        [Theory]
        [ClassData(typeof(PriceListTestData.AddLineItemInvalidTestData))]
        public async Task AddLineItem_InvalidNameOrPrice_ShouldThrow(int priceListIndex, string name, Money? price)
        {
            // Arrange
            var priceListId =
                (await Context.PriceLists.FindAsync(pl => pl.Contractor == Contractor.Retail)).ToList()[priceListIndex].Id;
            var command = new AddLineItemCommand(priceListId, name, price);

            // Act
            var addFunc = async () => await Sender.Send(command);
            // Assert
            await Assert.ThrowsAsync<ValidationException>(addFunc);
        }

        [Theory]
        [ClassData(typeof(PriceListTestData.AddLineItemValidTestData))]
        public async Task AddLineItem_InvalidPriceListId_ShouldThrow(int priceListIndex, string name, Money? price)
        {
            // Arrange
            var priceListId = "";
            var command = new AddLineItemCommand(priceListId, name, price);

            // Act
            var addFunc = async () => await Sender.Send(command);
            var isNotAddedToDb = (await Context.PriceLists.FindAsync(pl => pl.Id == priceListId && pl.LineItems.Any(li => li.Name == name))).FirstOrDefault() is null;

            // Assert
            Assert.True(isNotAddedToDb);
            await Assert.ThrowsAsync<ValidationException>(addFunc);
        }
    }
}
