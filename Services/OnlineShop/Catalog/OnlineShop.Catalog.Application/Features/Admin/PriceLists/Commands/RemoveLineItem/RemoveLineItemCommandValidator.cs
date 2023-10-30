using FluentValidation;
using MongoDB.Bson;

namespace OnlineShop.Catalog.Application.Features.Admin.PriceLists.Commands.RemoveLineItem
{
    internal class RemoveLineItemCommandValidator : AbstractValidator<RemoveLineItemCommand>
    {
        public RemoveLineItemCommandValidator()
        {
            RuleFor(x => x.PriceListId)
                .NotEmpty()
                .WithMessage("ID cennika wymagane")
                .Must(id => ObjectId.TryParse(id, out _))
                .WithMessage("Nieprawidłowy format ID");

            RuleFor(x => x.LineItemName)
                .NotEmpty()
                .WithMessage("Nazwa pozycji wymagana")
                .MaximumLength(100);
        }
    }
}
