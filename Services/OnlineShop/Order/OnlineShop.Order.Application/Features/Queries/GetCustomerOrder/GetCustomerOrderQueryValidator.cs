using FluentValidation;

namespace OnlineShop.Order.Application.Features.Queries.GetCustomerOrder
{
    internal class GetCustomerOrderQueryValidator : AbstractValidator<GetCustomerOrderQuery>
    {
        public GetCustomerOrderQueryValidator()
        {
            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Numer telefonu jest wymagany")
                .Matches("^[0-9]{9}$").WithMessage("Nieprawidłowy numer telefonu");
            
            RuleFor(x => x.OrderId)
                .NotEmpty().WithMessage("ID zamówienia jest wymagane");
        }
    }
}
