using FluentValidation;
using MongoDB.Bson;

namespace OnlineShop.Catalog.Application.Features.Admin.PriceLists.Commands.UploadPriceListAsExcel
{
    public class UploadPriceListSpreadsheetCommandValidator : AbstractValidator<UploadPriceListSpreadsheetCommand>
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
