using FluentValidation;

namespace OnlineShop.Order.Application.Features.Queries.GetOrdersAsPdf
{
    internal class GetOrdersAsPdfQueryValidator : AbstractValidator<GetOrdersAsPdfQuery>
    {
        public GetOrdersAsPdfQueryValidator()
        {
            RuleFor(x => x.OrdersId)
                .NotEmpty().WithMessage("ID zamówień są wymagane");
        }
    }
}
