using ClosedXML.Excel;

namespace OnlineShop.Catalog.Application.Features.Admin.PriceLists.Queries.DownloadPriceListAsExcel
{
    public sealed record PriceListSpreadsheetResponse(string FileName, XLWorkbook Spreadsheet);
}
