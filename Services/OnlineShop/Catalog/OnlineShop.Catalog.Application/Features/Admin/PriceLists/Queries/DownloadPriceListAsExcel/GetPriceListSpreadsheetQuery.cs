using Shared.Domain.Abstractions.Messaging;

namespace OnlineShop.Catalog.Application.Features.Admin.PriceLists.Queries.DownloadPriceListAsExcel
{
    public sealed record GetPriceListSpreadsheetQuery(string PriceListId) : IQuery<PriceListSpreadsheetResponse> 
    {
    }
}
