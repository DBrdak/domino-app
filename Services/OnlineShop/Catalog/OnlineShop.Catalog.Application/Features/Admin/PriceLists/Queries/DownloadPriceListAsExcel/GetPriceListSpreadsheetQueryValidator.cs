﻿using FluentValidation;
using MongoDB.Bson;

namespace OnlineShop.Catalog.Application.Features.Admin.PriceLists.Queries.DownloadPriceListAsExcel
{
    internal class GetPriceListSpreadsheetQueryValidator : AbstractValidator<GetPriceListSpreadsheetQuery>
    {
        public GetPriceListSpreadsheetQueryValidator()
        {
            RuleFor(x => x.PriceListId)
                .NotEmpty()
                .WithMessage("ID cennika wymagane")
                .Must(id => ObjectId.TryParse(id, out _))
                .WithMessage("Nieprawidłowy format ID");
        }
    }
}