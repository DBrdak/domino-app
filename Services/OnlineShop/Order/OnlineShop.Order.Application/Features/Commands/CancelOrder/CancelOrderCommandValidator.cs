using FluentValidation;

namespace OnlineShop.Order.Application.Features.Commands.CancelOrder
{
    internal class CancelOrderCommandValidator : AbstractValidator<CancelOrderCommand>
    {
        public CancelOrderCommandValidator()
        {
            RuleFor(x => x.OrderId)
                .NotEmpty().WithMessage("ID zamówienia jest wymagane");
        }
    }
}
