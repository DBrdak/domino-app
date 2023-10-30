using System.Text.RegularExpressions;
using FluentValidation;

namespace Shops.Application.Features.Commands.AddShop
{
    internal class AddShopCommandValidator : AbstractValidator<AddShopCommand>
    {
        public AddShopCommandValidator()
        {
            RuleFor(x => x.ShopName)
                .NotEmpty().WithMessage("Nazwa sklepu jest wymagana")
                .MaximumLength(50);

            RuleFor(x => x)
                .Must(command => (command.MobileShopData is null || command.StationaryShopData is null) &&
                                 (command.MobileShopData is not null || command.StationaryShopData is not null));

            RuleFor(x => x)
                .Must(
                    command => command.MobileShopData is null ||
                               Regex.IsMatch(command.MobileShopData.VehiclePlateNumber, "^[A-Z]{2,3}\\s[A-Z0-9]{4,5}$"))
                .WithMessage("Nieprawidłowy numer rejestracyjny");
        }
    }
}
