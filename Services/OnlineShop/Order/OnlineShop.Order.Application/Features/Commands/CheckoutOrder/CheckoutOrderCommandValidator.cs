using FluentValidation;
using OnlineShop.Order.Application.Core.GlobalValidators;

namespace OnlineShop.Order.Application.Features.Commands.CheckoutOrder
{
    public class CheckoutOrderCommandValidator : AbstractValidator<CheckoutOrderCommand>
    {
        public CheckoutOrderCommandValidator()
        {
            RuleFor(co => co.CheckoutOrder.DeliveryDate.Start)
                .SetValidator(new DateTimeWithTimeZoneValidator());

            RuleFor(co => co.CheckoutOrder.DeliveryDate.End)
                .SetValidator(new DateTimeWithTimeZoneValidator());
        }
    }
}