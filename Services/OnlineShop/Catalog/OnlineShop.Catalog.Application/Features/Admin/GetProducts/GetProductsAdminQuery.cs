using OnlineShop.Catalog.Application.Abstractions.Messaging;
using OnlineShop.Catalog.Domain;
using Shared.Domain.ResponseTypes;

namespace OnlineShop.Catalog.Application.Features.Admin.GetProducts
{
    public sealed record GetProductsAdminQuery(string SearchPhrase = "") : IQuery<List<Product>>;
}