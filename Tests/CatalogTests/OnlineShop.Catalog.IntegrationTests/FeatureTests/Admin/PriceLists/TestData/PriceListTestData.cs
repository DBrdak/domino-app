using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using MongoDB.Driver.Core.Operations;
using OnlineShop.Catalog.Application.Features.Admin.PriceLists.Queries.DownloadPriceListAsExcel;
using OnlineShop.Catalog.Domain.PriceLists;
using OnlineShop.Catalog.Infrastructure.Repositories;
using Org.BouncyCastle.Asn1.Ocsp;
using Shared.Domain.Errors;
using Shared.Domain.Money;
using Shared.Domain.ResponseTypes;

namespace OnlineShop.Catalog.IntegrationTests.FeatureTests.Admin.PriceLists.TestData
{
    internal class PriceListTestData
    {
        internal class AddLineItemValidTestData : TheoryData<int, string, Money>
        {
            public AddLineItemValidTestData()
            {
                Add(0, "new item 1", new Money(12.5m, Currency.Pln, Unit.Kg));
                Add(1, "new item 2", new Money(9.9m, Currency.Pln, Unit.Pcs));
            }
        }

        internal class AddLineItemInvalidTestData : TheoryData<int, string, Money?>
        {
            public AddLineItemInvalidTestData()
            {
                Add(0, "", new Money(12.5m, Currency.Pln, Unit.Kg));
                Add(1, "new item 3", null);
            }
        }

        internal static class FileFactory
        {
            public static XLWorkbook CreateValidExcelWorkbook()
            {
                var lineItems = new List<LineItem> { new ("Line Item lol", new Money(15.9m, Currency.Pln, Unit.Pcs)) };
                
                var workbook = new XLWorkbook();
                CreateWorkSheet(ref workbook, lineItems, "Cennik");

                return workbook;
            }

            public static XLWorkbook CreateInvalidItemsExcelWorkbook(PriceList priceList)
            {
                var lineItems = priceList.LineItems;

                var workbook = new XLWorkbook();
                CreateWorkSheet(ref workbook, lineItems, "Cennik");

                return workbook;
            }

            public static XLWorkbook CreateInvalidNamedWorksheetExcelWorkbook()
            {
                var lineItems = new List<LineItem> { new("Line Item lol", new Money(15.9m, Currency.Pln, Unit.Pcs)) };

                var workbook = new XLWorkbook();
                CreateWorkSheet(ref workbook, lineItems, "Nie Cennik");

                return workbook;
            }

            private static void CreateWorkSheet(ref XLWorkbook workbook, List<LineItem> lineItems, string worksheetName)
            {
                var worksheet = workbook.Worksheets.Add(worksheetName);

                worksheet.Cell(1, 1).Value = "Produkt";
                worksheet.Cell(1, 2).Value = "Cena";
                worksheet.Cell(1, 1).Style.Font.Bold = true;
                worksheet.Cell(1, 2).Style.Font.Bold = true;

                var row = 2;
                foreach (var item in lineItems)
                {
                    worksheet.Cell(row, 1).Value = item.Name;
                    worksheet.Cell(row, 2).Value = item.Price.ToString();
                    row++;
                }
                worksheet.Columns().AdjustToContents();
            }
        }
    }
}
