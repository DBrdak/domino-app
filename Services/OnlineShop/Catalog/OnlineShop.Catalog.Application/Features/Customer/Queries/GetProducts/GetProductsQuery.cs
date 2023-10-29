using OnlineShop.Catalog.Domain.Products;
using Shared.Domain.Abstractions.Messaging;
using Shared.Domain.ResponseTypes;

namespace OnlineShop.Catalog.Application.Features.Customer.Queries.GetProducts
{
    public sealed record GetProductsQuery(
        string Category,
        int Page = 1,
        string SortOrder = "asc",
        string SortBy = "Name",
        int PageSize = 12,
        string SearchPhrase = "",
        decimal MinPrice = 0,
        decimal MaxPrice = decimal.MaxValue,
        bool IsAvailable = false,
        bool IsDiscounted = false) : IQuery<PagedList<Product>>;
}