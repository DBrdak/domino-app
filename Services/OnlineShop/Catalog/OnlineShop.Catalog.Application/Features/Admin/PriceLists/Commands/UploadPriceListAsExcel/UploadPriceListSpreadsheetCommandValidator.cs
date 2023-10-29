using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using MongoDB.Bson;

namespace OnlineShop.Catalog.Application.Features.Admin.PriceLists.Commands.UploadPriceListAsExcel
{
    internal class UploadPriceListSpreadsheetCommandValidator : AbstractValidator<UploadPriceListSpreadsheetCommand>
    {
        public UploadPriceListSpreadsheetCommandValidator()
        {
            RuleFor(x => x.PriceListFile)
                .NotNull()
                .WithMessage("Plik jest wymagany");

            RuleFor(x => x.PriceListId)
                .NotEmpty()
                .WithMessage("ID cennika wymagane")
                .Must(id => ObjectId.TryParse(id, out _))
                .WithMessage("Nieprawidłowy format ID");
        }
    }
}
