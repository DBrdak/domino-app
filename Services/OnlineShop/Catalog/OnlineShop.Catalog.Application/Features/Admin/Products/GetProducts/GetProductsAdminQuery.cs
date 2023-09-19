using OnlineShop.Catalog.Domain.Products;
using Shared.Domain.Abstractions.Messaging;

namespace OnlineShop.Catalog.Application.Features.Admin.Products.GetProducts
{
    public sealed record GetProductsAdminQuery(string SearchPhrase = "") : IQuery<List<Product>>;
}