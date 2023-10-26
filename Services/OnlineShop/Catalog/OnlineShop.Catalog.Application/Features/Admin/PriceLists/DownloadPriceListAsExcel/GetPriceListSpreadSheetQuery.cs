using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ClosedXML.Excel;
using Shared.Domain.Abstractions.Messaging;

namespace OnlineShop.Catalog.Application.Features.Admin.PriceLists.DownloadPriceListAsExcel
{
    public sealed record GetPriceListSpreadsheetQuery(string PriceListId) : IQuery<PriceListSpreadsheetResponse> 
    {
    }
}
