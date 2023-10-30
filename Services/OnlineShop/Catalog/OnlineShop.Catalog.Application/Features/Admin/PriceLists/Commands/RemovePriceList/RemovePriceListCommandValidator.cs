using FluentValidation;
using MongoDB.Bson;

namespace OnlineShop.Catalog.Application.Features.Admin.PriceLists.Commands.RemovePriceList
{
    internal class RemovePriceListCommandValidator : AbstractValidator<RemovePriceListCommand>
    {
        public RemovePriceListCommandValidator()
        {
            RuleFor(x => x.PriceListId)
                .NotEmpty()
                .WithMessage("ID cennika wymagane")
                .Must(id => ObjectId.TryParse(id, out _))
                .WithMessage("Nieprawidłowy format ID");
        }
    }
}
