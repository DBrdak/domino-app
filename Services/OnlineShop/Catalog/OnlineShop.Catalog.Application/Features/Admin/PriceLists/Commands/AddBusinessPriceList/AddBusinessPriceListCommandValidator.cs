using FluentValidation;
using OnlineShop.Catalog.Domain.Shared;

namespace OnlineShop.Catalog.Application.Features.Admin.PriceLists.Commands.AddBusinessPriceList
{
    public class AddBusinessPriceListCommandValidator : AbstractValidator<AddBusinessPriceListCommand>
    {
        public AddBusinessPriceListCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Nazwa cennika jest wymagana.")
                .MaximumLength(100);

            RuleFor(x => x.ContractorName)
                .NotEmpty().WithMessage("Nazwa kontrahenta jest wymagana.")
                .MaximumLength(100);

            RuleFor(x => x.Category)
                .NotEmpty().WithMessage("Kategoria jest wymagana.")
                .Must(c => Category.All.Contains(Category.FromValue(c)))
                .WithMessage($"Kategoria musi mieć jedną z wartości: [{string.Join(',', Category.All.Select(c => c.Value))}]")
                .MaximumLength(100);
        }
    }
}
