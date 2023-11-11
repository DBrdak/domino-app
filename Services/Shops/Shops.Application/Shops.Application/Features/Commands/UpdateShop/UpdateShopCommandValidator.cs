using FluentValidation;

namespace Shops.Application.Features.Commands.UpdateShop
{
    public class UpdateShopCommandValidator : AbstractValidator<UpdateShopCommand>
    {
        public UpdateShopCommandValidator()
        {
            RuleFor(x => x.ShopToUpdateId)
                .NotEmpty().WithMessage("ID sklepu jest wymagane");

            RuleFor(x => x)
                .Must(
                    command => (command.StationaryShopUpdateValues is null || command.MobileShopUpdateValues is null) &&
                               (command.StationaryShopUpdateValues is not null || command.MobileShopUpdateValues is not null))
                .WithMessage("Podano nieprawidłowe wartości");
        }
    }
}
