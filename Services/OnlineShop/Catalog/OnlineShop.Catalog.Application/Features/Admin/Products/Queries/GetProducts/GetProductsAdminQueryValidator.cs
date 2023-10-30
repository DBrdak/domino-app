using FluentValidation;

namespace OnlineShop.Catalog.Application.Features.Admin.Products.Queries.GetProducts
{
    internal class GetProductsAdminQueryValidator : AbstractValidator<GetProductsAdminQuery>
    {
        public GetProductsAdminQueryValidator()
        {
            RuleFor(x => x.SearchPhrase)
                .MaximumLength(100);
        }
    }
}
