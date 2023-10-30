using FluentValidation;

namespace Shops.Application.Features.Commands.DeleteOrderFromShop
{
    internal class DeleteOrderFromShopCommandValidator : AbstractValidator<DeleteOrderFromShopCommand>
    {
        public DeleteOrderFromShopCommandValidator()
        {
            RuleFor(x => x)
                .Must(
                    command => !string.IsNullOrWhiteSpace(command.ShopId) &&
                               !string.IsNullOrWhiteSpace(command.OrderId))
                .WithMessage("ID sklepu i zamówienia są wymagane");
        }
    }
}
