using FluentValidation;

namespace Shops.Application.Features.Commands.AggregateOrderWithShop
{
    public class DeleteOrderFromShopCommandValidator : AbstractValidator<AggregateOrderWithShopCommand>
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
