using FluentValidation;
using MongoDB.Bson;

namespace OnlineShop.Catalog.Application.Features.Admin.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(x => x.NewValues.Id)
                .NotEmpty()
                .WithMessage("ID produktu wymagane")
                .Must(id => ObjectId.TryParse(id, out _))
                .WithMessage("Nieprawidłowy format ID");

            RuleFor(x => x.NewValues)
                .Must(
                    v => (v.IsWeightSwitchAllowed && v.SingleWeight.HasValue) ||
                         (!v.IsWeightSwitchAllowed && !v.SingleWeight.HasValue))
                .WithMessage("Niewłaściwe wartości dla alternatywnej jednostki");
        }
    }
}
