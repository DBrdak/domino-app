using Microsoft.AspNetCore.Http;
using Shared.Domain.Abstractions.Messaging;

namespace OnlineShop.Catalog.Application.Features.Admin.PriceLists.Commands.UploadPriceListAsExcel
{
    public sealed record UploadPriceListSpreadsheetCommand(string PriceListId, IFormFile PriceListFile) : ICommand
    {
    }
}
