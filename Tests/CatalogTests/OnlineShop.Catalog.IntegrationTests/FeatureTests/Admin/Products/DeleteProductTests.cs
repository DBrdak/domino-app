using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using OnlineShop.Catalog.Application.Features.Admin.Products.Commands.DeleteProduct;
using OnlineShop.Catalog.Domain.Products;
using Shared.Domain.Errors;

namespace OnlineShop.Catalog.IntegrationTests.FeatureTests.Admin.Products
{
    public class DeleteProductTests : BaseIntegrationTest
    {
        public DeleteProductTests(IntegrationTestWebAppFactory factory) : base(factory)
        {
        }

        [Fact]
        public async Task DeleteProduct_ValidData_ShouldDelete()
        {
            // Arrange
            var productToDelete = (await Context.Products.FindAsync(FilterDefinition<Product>.Empty)).First();
            var command = new DeleteProductCommand(productToDelete.Id);

            // Act
            var result = await Sender.Send(command);
            var isProductDeleted = (await Context.Products.FindAsync(p => p.Id == productToDelete.Id)).FirstOrDefault() is null;
            var isPhotoDeleted = (await PhotoRepository.GetPhotosUrl()).FirstOrDefault(uri => uri == productToDelete.Image.Url) is null;
            var isSplittedFromPriceList =
                (await Context.PriceLists.FindAsync(pl => pl.LineItems.Any(li => li.ProductId == productToDelete.Id))).ToList().Count == 0;

            // Assert
            Assert.True(result.IsSuccess);
            Assert.False(result.IsFailure);
            Assert.Equal(result.Error, Error.None);
            Assert.True(isProductDeleted);
            Assert.True(isPhotoDeleted);
            Assert.True(isSplittedFromPriceList);

            
        }

        [Fact]
        public async Task DeleteProduct_InvalidData_ShouldThrow()
        {
            // Arrange
            var command = new DeleteProductCommand(ObjectId.GenerateNewId().ToString());

            // Act
            var result = await Sender.Send(command);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.True(result.IsFailure);
            Assert.NotEqual(result.Error, Error.None);

            
        }
    }
}
