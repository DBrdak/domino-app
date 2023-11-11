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
        public async Task UpdateProduct_ValidData_ShouldUpdate(string description, bool isWeightSwitchAllowed, decimal? singleWeight, bool isAvailable)
        {
            // Arrange
            var productsId = (await (await Context.Products
                .FindAsync(FilterDefinition<Product>.Empty))
                .ToListAsync())
                .Select(p => p.Id)
                .ToList();
            var count = productsId.Count;
            var rand = new Random().Next(0, count - 1);
            var productToUpdate = (await Context.Products.FindAsync(p => p.Id == productsId[rand])).First();

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

            // Assert
            Assert.NotNull(result.Value);

            // Ensure product is deleted - obligatory for keeping Cloudinary clean
            await Sender.Send(new DeleteProductCommand(productToUpdate.Id));
        }
    }
}
