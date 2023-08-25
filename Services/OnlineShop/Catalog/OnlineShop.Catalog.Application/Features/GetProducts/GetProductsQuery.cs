using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineShop.Catalog.Application.Abstractions.Messaging;
using OnlineShop.Catalog.Domain;
using Shared.Domain.ResponseTypes;

namespace OnlineShop.Catalog.Application.Features.GetProducts
{
    public sealed record GetProductsQuery(
        string Category,
        int Page = 1,
        string SortOrder = "asc",
        string SortBy = "Name",
        int PageSize = 12,
        string SearchPhrase = "",
        string Subcategory = "",
        decimal MinPrice = 0,
        decimal MaxPrice = decimal.MaxValue,
        bool IsAvailable = false,
        bool IsDiscounted = false) : IQuery<PagedList<Product>>
    {
    }
}