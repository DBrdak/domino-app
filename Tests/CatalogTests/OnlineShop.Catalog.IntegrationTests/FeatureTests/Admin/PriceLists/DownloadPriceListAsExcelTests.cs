using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClosedXML.Excel;
using MongoDB.Driver;
using OnlineShop.Catalog.Application.Features.Admin.PriceLists.Queries.DownloadPriceListAsExcel;
using OnlineShop.Catalog.Domain.PriceLists;
using Shared.Domain.Errors;
using Shared.Domain.Money;

namespace OnlineShop.Catalog.IntegrationTests.FeatureTests.Admin.PriceLists
{
    public class DownloadPriceListAsExcelTests : BaseIntegrationTest
    {
        public DownloadPriceListAsExcelTests(IntegrationTestWebAppFactory factory) : base(factory)
        {
        }

        [Fact]
        public async Task DownloadPriceList_ValidData_ShouldReturnValidFile()
        {
            // Arrange
            var priceList = (await Context.PriceLists.FindAsync(FilterDefinition<PriceList>.Empty)).First();
            var expectedLineItems = priceList.LineItems.Select(li => new {name = li.Name, price = li.Price});
            var command = new GetPriceListSpreadsheetQuery(priceList.Id);

            // Act
            var result = await Sender.Send(command);

            var worksheet = result.Value.Spreadsheet.Worksheets.FirstOrDefault(ws => ws.Name == "Cennik");
            Assert.NotNull(worksheet); // In need of retriving line items

            var lineItems = RetriveLineItemsNames(worksheet);
            var actualLineItems = lineItems?.Select(li => new { name = li.Name, price = li.Price });

            // Assert
            Assert.True(result.IsSuccess);
            Assert.False(result.IsFailure);
            Assert.Equal(result.Error, Error.None);
            Assert.Equal(expectedLineItems, actualLineItems);
        }

        [Fact]
        public async Task DownloadPriceList_InvalidData_ShouldFail()
        {
            // Arrange
            var command = new GetPriceListSpreadsheetQuery("priceList.Id");

            // Act
            var result = await Sender.Send(command);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.True(result.IsFailure);
            Assert.NotEqual(result.Error, Error.None);
        }

        private List<LineItem>? RetriveLineItemsNames(IXLWorksheet worksheet)
        {
            if (string.IsNullOrWhiteSpace(worksheet.Cell(1, 1).GetText()) ||
                string.IsNullOrWhiteSpace(worksheet.Cell(1, 2).GetText()))
            {
                return null;
            }

            var names = new List<string>();
            var prices = new List<Money>();
            var lineItems = new List<LineItem>();
            var row = 2;
            var isCellEmpty = worksheet.Cell(2, 1).IsEmpty();

            while (!isCellEmpty)
            {
                try
                {
                    names.Add(worksheet.Cell(row, 1).GetText());
                    var moneyString = worksheet.Cell(row, 2).GetText();
                    prices.Add(Money.FromString(moneyString));
                }
                catch (Exception)
                {
                    return null;
                }

                row++;
                isCellEmpty = worksheet.Cell(row, 1).IsEmpty() || worksheet.Cell(row, 2).IsEmpty();
            }

            for (int i = 0; i < names.Count && i < prices.Count; i++)
            {
                lineItems.Add(new(names[i], prices[i]));
            }

            return lineItems;
        }
    }
}
