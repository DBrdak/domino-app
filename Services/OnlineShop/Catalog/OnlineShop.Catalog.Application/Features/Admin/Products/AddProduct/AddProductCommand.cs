using Microsoft.AspNetCore.Http;
using OnlineShop.Catalog.Domain.Products;
using Shared.Domain.Abstractions.Messaging;

namespace OnlineShop.Catalog.Application.Features.Admin.Products.AddProduct
{
    public sealed record AddProductCommand(CreateValues Values, IFormFile PhotoFile) : ICommand<Product>;
}