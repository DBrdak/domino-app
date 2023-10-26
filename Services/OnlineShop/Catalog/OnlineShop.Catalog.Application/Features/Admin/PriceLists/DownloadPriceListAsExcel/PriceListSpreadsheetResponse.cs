using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClosedXML.Excel;

namespace OnlineShop.Catalog.Application.Features.Admin.PriceLists.DownloadPriceListAsExcel
{
    public sealed record PriceListSpreadsheetResponse(string FileName, XLWorkbook Spreadsheet);
}
