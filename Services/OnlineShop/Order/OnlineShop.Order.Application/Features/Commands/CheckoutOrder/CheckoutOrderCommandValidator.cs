using FluentValidation;
using Shared.Domain.Money;

namespace OnlineShop.Order.Application.Features.Commands.CheckoutOrder
{
    internal class CheckoutOrderCommandValidator : AbstractValidator<CheckoutOrderCommand>
    {
        public CheckoutOrderCommandValidator()
        {
            RuleFor(x => x.CheckoutOrder)
                .NotNull().WithMessage("Zamówienie jest wymagane");

            RuleFor(x => x.CheckoutOrder.Items)
                .NotEmpty().WithMessage("Lista produktów jest wymagana");

            RuleFor(x => x.CheckoutOrder.TotalPrice.Amount)
                .GreaterThan(0).WithMessage("Cena zamówienia musi być większa od 0");

            RuleFor(x => x.CheckoutOrder.TotalPrice.Unit)
                .Must(unit => Unit.All.Contains(unit)).WithMessage("Nieprawidłowa jednostka");

            RuleFor(x => x.CheckoutOrder.TotalPrice.Currency)
                .Must(currency => Currency.All.Contains(currency)).WithMessage("Nieprawidłowa waluta");

            RuleFor(x => x.CheckoutOrder.FirstName)
                .NotEmpty().WithMessage("Imię zamawiającego jest wymagane");

            RuleFor(x => x.CheckoutOrder.LastName)
                .NotEmpty().WithMessage("Nazwisko zamawiającego jest wymagane");

            RuleFor(x => x.CheckoutOrder.PhoneNumber)
                .Matches("^[0-9]{9}$").WithMessage("Nieprawidłowy format numeru telefonu");

            RuleFor(x => x.CheckoutOrder.DeliveryLocation)
                .NotNull().WithMessage("Lokalizacja dostawy jest wymagana");

            RuleFor(x => x.CheckoutOrder.DeliveryDate)
                .NotNull().WithMessage("Data dostawy jest wymagana");
        }
    }
}
