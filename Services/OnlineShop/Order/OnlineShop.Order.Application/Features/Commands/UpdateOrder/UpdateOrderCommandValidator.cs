using FluentValidation;
using OnlineShop.Order.Domain.OnlineOrders;

namespace OnlineShop.Order.Application.Features.Commands.UpdateOrder
{
    internal class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
    {
        public UpdateOrderCommandValidator()
        {
            RuleFor(x => x.OrderId)
                .NotEmpty().WithMessage("ID zamówienia jest wymagane");

            RuleFor(x => x.Status)
                .Must(status => status is null || OrderStatus.All.Contains(OrderStatus.FromMessage(status)))
                .WithMessage("Nieprawidłowy status zamówienia");

            RuleFor(x => x)
                .Must(
                    command => command.ModifiedOrderItems is null ||
                               OrderStatus.FromMessage(command.Status) == OrderStatus.Modified)
                .WithMessage("Nieprawidłowa modyfikacja zamówienia");
        }
    }
}
