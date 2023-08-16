using FluentValidation;
using OnlineShop.ShoppingCart.API.Entities;

namespace OnlineShop.ShoppingCart.API.Validators
{
    public class ShoppingCartItemValidator : AbstractValidator<ShoppingCartItem>
    {
        public ShoppingCartItemValidator()
        {
            RuleFor(x => x.Unit)
                .Must(x => x == "kg" || x == "szt")
                .WithMessage(x => $"{x.Unit} is invalid unit");
        }
    }
}