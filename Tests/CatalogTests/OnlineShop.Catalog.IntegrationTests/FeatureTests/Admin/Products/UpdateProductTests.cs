using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudinaryDotNet.Actions;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
using MongoDB.Driver;
using OnlineShop.Catalog.Application.Features.Admin.Products.Commands.DeleteProduct;
using OnlineShop.Catalog.Application.Features.Admin.Products.Commands.UpdateProduct;
using OnlineShop.Catalog.Domain.Products;
using OnlineShop.Catalog.IntegrationTests.FeatureTests.Admin.Products.TestData;
using Shared.Domain.Errors;
using Shared.Domain.ResponseTypes;

namespace OnlineShop.Catalog.IntegrationTests.FeatureTests.Admin.Products
{
    public class UpdateProductTests : BaseIntegrationTest
    {
        public UpdateProductTests(IntegrationTestWebAppFactory factory) : base(factory)
        {
        }

        [Theory]
        [ClassData(typeof(ProductTestData.UpdateProductValidTestData))]
        public async Task UpdateProduct_ValidData_ShouldUpdate(int index, string description, bool isWeightSwitchAllowed, decimal? singleWeight, bool isAvailable)
        {
            // Arrange
            var productsId = (await (await Context.Products
                .FindAsync(FilterDefinition<Product>.Empty))
                .ToListAsync())
                .Select(p => p.Id)
                .ToList();
            var productToUpdate = (await Context.Products.FindAsync(p => p.Id == productsId[index])).First();

            var file = await ProductTestData.CreateImageFile();
            
            var command = new UpdateProductCommand(
                new (productToUpdate.Id,
                    description, 
                    productToUpdate.Image.Url,
                    isWeightSwitchAllowed, 
                    singleWeight,
                    isAvailable),
                file
                );

            // Act
            var result = await Sender.Send(command);
            var isPhotoChanged = (await PhotoRepository.GetPhotosUrl()).Any(uri => uri == result.Value.Image.Url);
            var isPhotoDeleted = await PhotoRepository.DeletePhoto(result.Value.Image.Url);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.False(result.IsFailure);
            Assert.NotNull(result.Value);
            Assert.Equal(result.Value.Details.IsAvailable, isAvailable);
            Assert.Equal(result.Value.Details.IsWeightSwitchAllowed, isWeightSwitchAllowed);
            Assert.Equal(result.Value.Details.SingleWeight?.Value, singleWeight);
            Assert.True(result.Value.Description == description ||
                        (string.IsNullOrWhiteSpace(description) &&
                         result.Value.Description == productToUpdate.Description));
            Assert.True(isPhotoChanged);
            Assert.True(isPhotoDeleted);

            
        }

        [Theory]
        [ClassData(typeof(ProductTestData.UpdateProductInvalidTestData))]
        public async Task UpdateProduct_InvalidData_ShouldThrowValidationException(int index, string description, bool isWeightSwitchAllowed, decimal? singleWeight, bool isAvailable)
        {
            // Arrange
            var productsId = (await (await Context.Products
                .FindAsync(FilterDefinition<Product>.Empty))
                .ToListAsync())
                .Select(p => p.Id)
                .ToList();
            var productToUpdate = (await Context.Products.FindAsync(p => p.Id == productsId[index])).First();

            var file = await ProductTestData.CreateImageFile();

            var command = new UpdateProductCommand(
                new("productToUpdate.Id",
                    description,
                    productToUpdate.Image.Url,
                    isWeightSwitchAllowed,
                    singleWeight,
                    isAvailable),
                file
                );

            // Act
            var sendFunc = async () => await Sender.Send(command);

            // Assert
            await Assert.ThrowsAsync<ValidationException>(sendFunc);

            
        }

        [Fact]
        public async Task UpdateProduct_InvalidIdType_ShouldThrowValidationException()
        {
            // Arrange
            
            var command = new UpdateProductCommand(
                new("productToUpdate.Id",
                    "description",
                    "productToUpdate.Image.Url",
                    false,
                    null,
                    true),
                null
            );

            // Act
            var sendFunc = async () => await Sender.Send(command);

            // Assert
            await Assert.ThrowsAsync<ValidationException>(sendFunc);

            
        }

        [Fact]
        public async Task UpdateProduct_InvalidId_ShouldReturnFailure()
        {
            // Arrange

            var command = new UpdateProductCommand(
                new(ObjectId.GenerateNewId().ToString(),
                    "description",
                    "productToUpdate.Image.Url",
                    false,
                    null,
                    true),
                null
            );

            // Act
            var result = await Sender.Send(command);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.True(result.IsFailure);
            Assert.Throws<InvalidOperationException>(() => result.Value);

            
        }
    }
}
