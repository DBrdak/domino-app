using FluentValidation;
using MongoDB.Bson;
using Shared.Domain.Money;

namespace OnlineShop.Catalog.Application.Features.Admin.PriceLists.Commands.AddLineItem
{
    internal class AddLineItemCommandValidator : AbstractValidator<AddLineItemCommand>
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

            RuleFor(x => x.Price.Amount)
                .GreaterThan(0).WithMessage("Cena pozycji musi być większa od 0");

            RuleFor(x => x.Price.Currency)
                .Must(currency => Currency.All.Contains(currency)).WithMessage(
                    $"Waluta musi mieć jedną z wartości: [{string.Join(',', Currency.All.Select(u => u.Code))}]");

            RuleFor(x => x.Price.Unit)
                .Must(unit => Unit.All.Contains(unit)).WithMessage(
                    $"Jednostka wagi musi mieć jedną z wartości: [{string.Join(',', Unit.All.Select(u => u.Code))}]");
        }
    }
}
