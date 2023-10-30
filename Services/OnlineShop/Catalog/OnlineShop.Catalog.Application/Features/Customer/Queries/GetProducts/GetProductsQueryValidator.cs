using FluentValidation;
using OnlineShop.Catalog.Domain.Shared;

namespace OnlineShop.Catalog.Application.Features.Customer.Queries.GetProducts
{
    internal class GetProductsQueryValidator : AbstractValidator<GetProductsQuery>
    {
        public GetProductsQueryValidator()
        {
            RuleFor(x => x.Category)
                .NotEmpty().WithMessage("Kategoria jest wymagana.")
                .Must(c => Category.All.Contains(Category.FromValue(c)))
                .WithMessage($"Kategoria musi mieć jedną z wartości: [{string.Join(',', Category.All.Select(c => c.Value))}]")
                .MaximumLength(100);

            RuleFor(x => x.MinPrice)
                .GreaterThanOrEqualTo(0).WithMessage("Minimalna cena nie może być mniejsza niż 0.");

            RuleFor(x => x.MaxPrice)
                .GreaterThanOrEqualTo(0).WithMessage("Maksymalna cena nie może być mniejsza niż 0.");

            RuleFor(x => x)
                .Must(query => query.MaxPrice >= query.MinPrice)
                .WithMessage("Cena maksymalna musi być wyższa lub równa minimalnej");

            RuleFor(x => x.PageSize < 16);

            RuleFor(x => x.SearchPhrase)
                .MaximumLength(100);

            RuleFor(x => x.SortOrder)
                .Must(s => s == "asc" || s == "desc");
        }
    }
}
