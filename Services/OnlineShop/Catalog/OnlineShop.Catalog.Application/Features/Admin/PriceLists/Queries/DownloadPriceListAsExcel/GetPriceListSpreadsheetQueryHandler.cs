﻿using ClosedXML.Excel;
using OnlineShop.Catalog.Domain.PriceLists;
using Shared.Domain.Abstractions.Messaging;
using Shared.Domain.Errors;
using Shared.Domain.ResponseTypes;

namespace OnlineShop.Catalog.Application.Features.Admin.PriceLists.Queries.DownloadPriceListAsExcel
{
    internal sealed class GetPriceListSpreadsheetQueryHandler : IQueryHandler<GetPriceListSpreadsheetQuery, PriceListSpreadsheetResponse>
    {
        private readonly IPriceListRepository _priceListRepository;

        public GetPriceListSpreadsheetQueryHandler(IPriceListRepository priceListRepository)
        {
            _priceListRepository = priceListRepository;
        }

        public async Task<Result<PriceListSpreadsheetResponse>> Handle(GetPriceListSpreadsheetQuery request, CancellationToken cancellationToken)
        {
            var priceLists = await _priceListRepository.GetPriceListsAsync(cancellationToken);
            var priceList = priceLists.FirstOrDefault(x => x.Id == request.PriceListId);

            if (priceList is null)
            {
                return Result.Failure<PriceListSpreadsheetResponse>(Error.TaskFailed(
                    $"Nie można pobrać cennika - cennik o ID: {request.PriceListId} nie istnieje."));
            }

            var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Cennik");

            worksheet.Cell(1, 1).Value = "Produkt";
            worksheet.Cell(1, 2).Value = "Cena";
            worksheet.Cell(1, 1).Style.Font.Bold = true;
            worksheet.Cell(1, 2).Style.Font.Bold = true;

            var row = 2;
            foreach (var item in priceList.LineItems)
            {
                worksheet.Cell(row, 1).Value = item.Name;
                worksheet.Cell(row, 2).Value = item.Price.ToString();
                row++;
            }

            worksheet.Columns().AdjustToContents();

            return new PriceListSpreadsheetResponse(priceList.Name, workbook);
        }


    }
}
