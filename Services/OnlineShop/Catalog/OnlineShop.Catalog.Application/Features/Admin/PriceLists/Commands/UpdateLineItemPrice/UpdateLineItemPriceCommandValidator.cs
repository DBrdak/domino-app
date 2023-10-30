using FluentValidation;
using MongoDB.Bson;
using Shared.Domain.Money;

namespace OnlineShop.Catalog.Application.Features.Admin.PriceLists.Commands.UpdateLineItemPrice
{
    internal class UpdateLineItemPriceCommandValidator : AbstractValidator<UpdateLineItemPriceCommand>
    {
        public UpdateLineItemPriceCommandValidator()
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

            RuleFor(x => x.NewPrice)
                .NotNull().WithMessage("Cena pozycji jest wymagana");

            RuleFor(x => x.NewPrice.Amount)
                .GreaterThan(0).WithMessage("Cena pozycji musi być większa od 0");

            RuleFor(x => x.NewPrice.Currency)
                .Must(currency => Currency.All.Contains(currency)).WithMessage(
                    $"Waluta musi mieć jedną z wartości: [{string.Join(',', Currency.All.Select(u => u.Code))}]");

            RuleFor(x => x.NewPrice.Unit)
                .Must(unit => Unit.All.Contains(unit)).WithMessage(
                    $"Jednostka wagi musi mieć jedną z wartości: [{string.Join(',', Unit.All.Select(u => u.Code))}]");
        }
    }
}
