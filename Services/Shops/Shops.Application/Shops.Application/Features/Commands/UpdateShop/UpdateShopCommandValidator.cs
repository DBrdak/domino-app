using FluentValidation;

namespace Shops.Application.Features.Commands.UpdateShop
{
    public class UpdateShopCommandValidator : AbstractValidator<UpdateShopCommand>
    {
        public UpdateShopCommandValidator()
        {
            RuleFor(x => x.ShopToUpdateId)
                .NotEmpty().WithMessage("ID sklepu jest wymagane");
        }
    }
}
