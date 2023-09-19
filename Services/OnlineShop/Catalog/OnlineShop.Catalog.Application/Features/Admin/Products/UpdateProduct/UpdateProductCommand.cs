using Microsoft.AspNetCore.Http;
using OnlineShop.Catalog.Domain.Products;
using Shared.Domain.Abstractions.Messaging;

namespace OnlineShop.Catalog.Application.Features.Admin.Products.UpdateProduct
{
    public sealed record UpdateProductCommand(UpdateValues NewValues, IFormFile? PhotoFile) : ICommand<Product>;
}