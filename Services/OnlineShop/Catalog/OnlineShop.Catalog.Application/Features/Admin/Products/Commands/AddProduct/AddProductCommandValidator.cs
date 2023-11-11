using FluentValidation;

namespace OnlineShop.Catalog.Application.Features.Admin.Products.Commands.AddProduct
{
    public class AddProductCommandValidator : AbstractValidator<AddProductCommand>
    {
        public AddProductCommandValidator()
        {
            RuleFor(x => x.Values.Name)
                .NotEmpty().WithMessage("Nazwa produktu jest wymagana")
                .MaximumLength(100);

            RuleFor(x => x.Values.Description)
                .NotEmpty().WithMessage("Opis produktu jest wymagany");

            RuleFor(x => x.Values)
                .Must(x => (x.IsWeightSwitchAllowed && x.SingleWeight.HasValue) ||
                           (!x.IsWeightSwitchAllowed && !x.SingleWeight.HasValue))
                .WithMessage("Wartość wagi jednej jednostki produktu jest wymagana");

            RuleFor(x => x.PhotoFile)
                .NotNull().WithMessage("Zdjęcie produktu jest wymagane");
        }
    }
}
