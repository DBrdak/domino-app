using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Shared.Domain.Abstractions.Messaging;
using Shared.Domain.ResponseTypes;

namespace OnlineShop.Catalog.Application.Features.Admin.PriceLists.UploadPriceListAsExcel
{
    public sealed record UploadPriceListSpreadsheetCommand(string PriceListId, IFormFile PriceListFile) : ICommand
    {
    }
}
