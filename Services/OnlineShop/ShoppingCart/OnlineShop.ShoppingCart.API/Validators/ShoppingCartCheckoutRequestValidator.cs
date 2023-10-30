using FluentValidation;
using OnlineShop.ShoppingCart.API.Controllers.Requests;

namespace OnlineShop.ShoppingCart.API.Validators
{
    public class ShoppingCartCheckoutRequestValidator : AbstractValidator<ShoppingCartCheckoutRequest>
    {
        public ShoppingCartCheckoutRequestValidator()
        {
            RuleFor(x => x.ShoppingCart)
                .NotNull().WithMessage("Koszyk jest wymagany");


            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("Imię zamawiającego jest wymagane");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Nazwisko zamawiającego jest wymagane");

            RuleFor(x => x.PhoneNumber)
                .Matches("^[0-9]{9}$").WithMessage("Nieprawidłowy format numeru telefonu");

            RuleFor(x => x.DeliveryLocation)
                .NotNull().WithMessage("Lokalizacja dostawy jest wymagana");

            RuleFor(x => x.DeliveryDate)
                .NotNull().WithMessage("Data dostawy jest wymagana");

            RuleFor(x => x.ShoppingCart.Items)
                .NotEmpty().WithMessage("Nie można dokonać zamówienia z pustym koszykiem");
        }
    }
}
