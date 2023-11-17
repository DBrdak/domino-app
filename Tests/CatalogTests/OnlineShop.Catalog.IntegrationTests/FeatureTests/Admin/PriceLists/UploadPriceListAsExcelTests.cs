using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using MongoDB.Driver;
using OnlineShop.Catalog.Application.Features.Admin.PriceLists.Commands.UploadPriceListAsExcel;
using OnlineShop.Catalog.Domain.PriceLists;
using OnlineShop.Catalog.IntegrationTests.FeatureTests.Admin.PriceLists.TestData;
using Shared.Domain.Errors;
using Shared.Domain.ResponseTypes;

namespace OnlineShop.Catalog.IntegrationTests.FeatureTests.Admin.PriceLists
{
    public class UploadPriceListAsExcelTests : BaseIntegrationTest
    {
        public UploadPriceListAsExcelTests(IntegrationTestWebAppFactory factory) : base(factory)
        {
        }

        [Fact]
        public async Task UploadPriceList_ValidData_ShouldUpload()
        {
            // Arrange
            var priceList = (await Context.PriceLists.FindAsync(FilterDefinition<PriceList>.Empty)).First();
            var workbook = PriceListTestData.FileFactory.CreateValidExcelWorkbook();
            
            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            var file = new FormFile(stream, 0, stream.Length, "priceListFile", "priceListFile");

            var command = new UploadPriceListSpreadsheetCommand(priceList.Id, file);

            // Act
            var result = await Sender.Send(command);
            var isAddedToDb = 
                (await Context.PriceLists.FindAsync(pl => pl.Id == priceList.Id)).First().LineItems.Count > priceList.LineItems.Count;

            // Assert
            Assert.True(isAddedToDb);
            Assert.True(result.IsSuccess);
            Assert.False(result.IsFailure);
            Assert.Equal(result.Error, Error.None);
        }

        [Fact]
        public async Task UploadPriceList_InvalidData_DuplicatedLineItems_ShouldFail()
        {
            // Arrange
            var priceList = (await Context.PriceLists.FindAsync(FilterDefinition<PriceList>.Empty)).First();
            var workbook = PriceListTestData.FileFactory.CreateInvalidItemsExcelWorkbook(priceList);

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            var file = new FormFile(stream, 0, stream.Length, "priceListFile", "priceListFile");

            var command = new UploadPriceListSpreadsheetCommand(priceList.Id, file);

            // Act
            var result = await Sender.Send(command);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.True(result.IsFailure);
            Assert.NotEqual(result.Error, Error.None);
        }

        [Fact]
        public async Task UploadPriceList_InvalidFile_ShouldFail()
        {
            // Arrange
            var priceList = (await Context.PriceLists.FindAsync(FilterDefinition<PriceList>.Empty)).First();

            using var stream = new MemoryStream();
            var file = new FormFile(stream, 0, stream.Length, "priceListFile", "priceListFile");

            var command = new UploadPriceListSpreadsheetCommand(priceList.Id, file);

            // Act
            var result = await Sender.Send(command);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.True(result.IsFailure);
            Assert.NotEqual(result.Error, Error.None);
        }

        [Fact]
        public async Task UploadPriceList_InvalidWorksheetName_ShouldFail()
        {
            // Arrange
            var priceList = (await Context.PriceLists.FindAsync(FilterDefinition<PriceList>.Empty)).First();
            var workbook = PriceListTestData.FileFactory.CreateInvalidNamedWorksheetExcelWorkbook();

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            var file = new FormFile(stream, 0, stream.Length, "priceListFile", "priceListFile");

            var command = new UploadPriceListSpreadsheetCommand(priceList.Id, file);

            // Act
            var result = await Sender.Send(command);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.True(result.IsFailure);
            Assert.NotEqual(result.Error, Error.None);
        }
    }
}
