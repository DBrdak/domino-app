using FluentValidation;
using MongoDB.Bson;
using Shared.Domain.Money;

namespace OnlineShop.Catalog.Application.Features.Admin.PriceLists.Commands.AddLineItem
{
    public class AddLineItemCommandValidator : AbstractValidator<AddLineItemCommand>
    {
        public AddLineItemCommandValidator()
        {
            RuleFor(x => x.PriceListId)
                .NotEmpty()
                .WithMessage("ID cennika jest wymagane")
                .Must(id => ObjectId.TryParse(id, out _))
                .WithMessage("Nieprawidłowy format ID");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Nazwa pozycji jest wymagana")
                .MaximumLength(100);

            RuleFor(x => x.Price)
                .NotNull().WithMessage("Cena pozycji jest wymagana");
        }
    }
}
