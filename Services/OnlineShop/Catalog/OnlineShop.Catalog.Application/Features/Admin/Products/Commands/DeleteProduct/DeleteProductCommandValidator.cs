using FluentValidation;
using MongoDB.Bson;

namespace OnlineShop.Catalog.Application.Features.Admin.Products.Commands.DeleteProduct
{
    public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductCommandValidator()
        {
            RuleFor(x => x.ProductId)
                .NotEmpty()
                .WithMessage("ID produktu wymagane")
                .Must(id => ObjectId.TryParse(id, out _))
                .WithMessage("Nieprawidłowy format ID");
        }
    }
}
