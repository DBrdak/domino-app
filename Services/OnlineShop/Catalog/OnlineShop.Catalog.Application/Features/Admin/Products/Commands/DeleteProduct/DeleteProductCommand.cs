using Shared.Domain.Abstractions.Messaging;

namespace OnlineShop.Catalog.Application.Features.Admin.Products.Commands.DeleteProduct
{
    public sealed record DeleteProductCommand(string ProductId) : ICommand;
}