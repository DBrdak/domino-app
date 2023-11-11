using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using MongoDB.Driver;
using Moq;
using OnlineShop.Catalog.Application.Features.Admin.Products.Commands.DeleteProduct;
using OnlineShop.Catalog.Application.Features.Admin.Products.Commands.UpdateProduct;
using OnlineShop.Catalog.Domain.Products;
using OnlineShop.Catalog.IntegrationTests.FeatureTests.Admin.Products.TestData;

namespace OnlineShop.Catalog.IntegrationTests.FeatureTests.Admin.Products
{
    public class UpdateProductTests : BaseIntegrationTest
    {
        public UpdateProductTests(IntegrationTestWebAppFactory factory) : base(factory)
        {
        }

        [Theory]
        [ClassData(typeof(UpdateProductTestData.UpdateProductValidTestData))]
        public async Task UpdateProduct_ValidData_ShouldUpdate(int index, string description, bool isWeightSwitchAllowed, decimal? singleWeight, bool isAvailable)
        {
            // Arrange
            var productsId = (await (await Context.Products
                .FindAsync(FilterDefinition<Product>.Empty))
                .ToListAsync())
                .Select(p => p.Id)
                .ToList();
            var count = productsId.Count;
            var productToUpdate = (await Context.Products.FindAsync(p => p.Id == productsId[index])).First();

            var sourceImg = File.OpenRead(@"D:\Programownie\Projekty\Domino Projekt\domino-app\Services\OnlineShop\Catalog\Tests\OnlineShop.Catalog.IntegrationTests\FeatureTests\Admin\Products\TestData\exampleImage.jpg");
            var stream = new MemoryStream();
            await sourceImg.CopyToAsync(stream);
            stream.Position = 0;
            var file = new FormFile(stream, 0, stream.Length, "exampleFile.jpg", "exampleFile.jpg");
            
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
            var deletrionResult = await PhotoRepository.DeletePhoto(result.Value.Image.Url);

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
            Assert.True(deletrionResult);
        }

        [Theory]
        [ClassData(typeof(UpdateProductTestData.UpdateProductInvalidTestData))]
        public async Task UpdateProduct_InvalidData_ShouldUpdate(int index, string description, bool isWeightSwitchAllowed, decimal? singleWeight, bool isAvailable)
        {
            // Arrange
            var productsId = (await (await Context.Products
                .FindAsync(FilterDefinition<Product>.Empty))
                .ToListAsync())
                .Select(p => p.Id)
                .ToList();
            var count = productsId.Count;
            var productToUpdate = (await Context.Products.FindAsync(p => p.Id == productsId[index])).First();

            var sourceImg = File.OpenRead(@"D:\Programownie\Projekty\Domino Projekt\domino-app\Services\OnlineShop\Catalog\Tests\OnlineShop.Catalog.IntegrationTests\FeatureTests\Admin\Products\TestData\exampleImage.jpg");
            var stream = new MemoryStream();
            await sourceImg.CopyToAsync(stream);
            stream.Position = 0;
            var file = new FormFile(stream, 0, stream.Length, "exampleFile.jpg", "exampleFile.jpg");

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
            var result = await Sender.Send(command);
            var deletrionResult = await PhotoRepository.DeletePhoto(result.Value.Image.Url);

            // Assert
            Assert.True(deletrionResult);
        }
    }
}
