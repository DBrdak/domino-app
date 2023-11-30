using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using MongoDB.Driver;
using OnlineShop.Catalog.Application.Features.Admin.Products.Commands.AddProduct;
using OnlineShop.Catalog.Domain.PriceLists;
using OnlineShop.Catalog.Domain.Products;
using OnlineShop.Catalog.IntegrationTests.FeatureTests.Admin.Products.TestData;
using Shared.Domain.Errors;

namespace OnlineShop.Catalog.IntegrationTests.FeatureTests.Admin.Products
{
    public class AddProductTests : BaseIntegrationTest
    {
        public AddProductTests(IntegrationTestWebAppFactory factory) : base(factory)
        {
        }

        [Theory]
        [ClassData(typeof(ProductTestData.AddProductValidTestData))]
        public async Task AddProduct_ValidData_ShouldCreateAndInsert(int index, string description, bool isWeightSwitchAllowed, decimal? singleWeight)
        {
            // Arrange
            var productName = (await Context.PriceLists.FindAsync(FilterDefinition<PriceList>.Empty)).ToList()[index].LineItems.FirstOrDefault(li => li.ProductId is null)?.Name;

            if (productName == null)
            {
                return;
            }

            var file = await ProductTestData.CreateImageFile();
            var command = new AddProductCommand(new CreateValues(productName, description, isWeightSwitchAllowed, singleWeight), file);

            // Act
            var result = await Sender.Send(command);
            var (isAddedToDb, isPhotoUploaded, isAggregatedWithPriceList, isPriceAttachedCorrectly) = (false, false, false, false);

            if (result.IsSuccess)
            {
                isAddedToDb = (await Context.Products.FindAsync(p => p.Id == result.Value.Id)).FirstOrDefault() is not null;
                isPhotoUploaded = (await PhotoRepository.GetPhotosUrl()).SingleOrDefault(uri => uri == result.Value.Image.Url) is not null;
                isAggregatedWithPriceList =
                    (await Context.PriceLists.FindAsync(pl => pl.LineItems.Any(li => li.ProductId == result.Value.Id)))
                    .FirstOrDefault().LineItems.FirstOrDefault(li => li.ProductId == result.Value.Id) is not null;
                isPriceAttachedCorrectly =
                    (await Context.PriceLists.FindAsync(pl => pl.LineItems.Any(li => li.ProductId == result.Value.Id)))
                    .FirstOrDefault().LineItems.FirstOrDefault(li => li.ProductId == result.Value.Id)?.Price == result.Value.Price;

                await PhotoRepository.DeletePhoto(result.Value.Image.Url);
            }

            // Assert
            Assert.True(result.IsSuccess);
            Assert.False(result.IsFailure);
            Assert.Equal(result.Error, Error.None);
            Assert.True(result.IsFailure || result.Value is not null);
            Assert.True(isAddedToDb);
            Assert.True(isPhotoUploaded);
            Assert.True(isAggregatedWithPriceList);
            Assert.True(isPriceAttachedCorrectly);

            
        }

        [Theory]
        [ClassData(typeof(ProductTestData.AddProductInvalidTestData))]
        public async Task AddProduct_InvalidData_ShouldThrowValidationException(int index, string description, bool isWeightSwitchAllowed, decimal? singleWeight)
        {
            // Arrange
            var productName = (await Context.PriceLists.FindAsync(FilterDefinition<PriceList>.Empty)).ToList()[index].Name;

            var file = await ProductTestData.CreateImageFile();
            var command = new AddProductCommand(new CreateValues(productName, description, isWeightSwitchAllowed, singleWeight), file);

            // Act
            var createFunc = async () => await Sender.Send(command);

            // Assert
            await Assert.ThrowsAsync<ValidationException>(createFunc);
            
        }

        [Fact]
        public async Task AddProduct_InvalidImage_ShouldThrowValidationException()
        {
            // Arrange
            var productName = (await Context.PriceLists.FindAsync(FilterDefinition<PriceList>.Empty)).ToList()[0].Name;

            var command = new AddProductCommand(new CreateValues(productName, "description", false, null), null);

            // Act
            var createFunc = async () => await Sender.Send(command);

            // Assert
            await Assert.ThrowsAsync<ValidationException>(createFunc);

            
        }

        [Fact]
        public async Task AddProduct_InvalidName_ShouldFail()
        {
            // Arrange
            var file = await ProductTestData.CreateImageFile();
            var command = new AddProductCommand(new CreateValues("productName", "description", false, null), file);

            // Act
            var result = await Sender.Send(command);

            // Assert
            Assert.True(result.IsFailure);
            Assert.False(result.IsSuccess);
            Assert.NotEqual(result.Error, Error.None);
            Assert.Throws<InvalidOperationException>(() => result.Value);

            
        }

        [Fact]
        public async Task AddProduct_InvalidDuplicateName_ShouldFail()
        {
            // Arrange
            var productName = (await Context.Products.FindAsync(FilterDefinition<Product>.Empty)).First().Name;
            var file = await ProductTestData.CreateImageFile();
            var command = new AddProductCommand(new CreateValues(productName, "description", false, null), file);

            // Act
            var result = await Sender.Send(command);

            // Assert
            Assert.True(result.IsFailure);
            Assert.False(result.IsSuccess);
            Assert.NotEqual(result.Error, Error.None);
            Assert.Throws<InvalidOperationException>(() => result.Value);

            
        }
    }
}
