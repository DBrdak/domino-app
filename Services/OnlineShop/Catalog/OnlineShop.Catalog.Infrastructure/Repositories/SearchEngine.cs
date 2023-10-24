using MongoDB.Driver;
using OnlineShop.Catalog.Domain.Products;
using OnlineShop.Catalog.Domain.Shared;
using System.Globalization;
using System.Text;

namespace OnlineShop.Catalog.Infrastructure.Repositories
{
    internal static class SearchEngine
    {
        internal static FindOptions<Product> ApplySorting(string sortOrder, string sortBy)
        {
            sortBy = NormailzeSortPropertyName(sortBy);

            var sort = sortOrder == "asc"
                ? Builders<Product>.Sort.Ascending(sortBy)
                : Builders<Product>.Sort.Descending(sortBy);
            var options = new FindOptions<Product>
            {
                Sort = sort
            };
            return options;
        }

        private static string NormailzeSortPropertyName(string sortBy)
        {
            var normailzedSortBy = string.Empty;

            for (int i = 0; i < sortBy.Length; i++)
            {
                normailzedSortBy += (i == 0 || sortBy[i-1] == '.') ? 
                    sortBy[i].ToString().ToUpper() : 
                    sortBy[i];
            }

            return normailzedSortBy;
        }

        internal static List<Product> ApplySearch(string name, List<Product> products)
        {
            if (string.IsNullOrWhiteSpace(name))
                return products;

            return products.Where(p =>
            {
                name = RemovePolishAccents(name.ToLower());
                var productName = RemovePolishAccents(p.Name.ToLower());

                if (productName.Contains(' '))
                {
                    var words = productName.Split(" ");
                    var result = new bool[words.Length];

                    for (int i = 0; i < words.Length; i++)
                    {
                        result[i] = words[i].StartsWith(name);
                    }

                    return result.Any(r => r == true);
                }

                return productName.StartsWith(name);
            }).ToList();
        }

        internal static FilterDefinition<Product> ApplyFiltering(string category, decimal? minPrice, decimal? maxPrice, bool? isAvailable, bool? isDiscounted)
        {
            var filter = FilterDefinition<Product>.Empty;

            if (category.ToLower() == "meat" || category.ToLower() == "sausage")
            {
                filter &= Builders<Product>.Filter.Eq(p => p.Category, Category.FromValue(category));
            }
            else
            {
                throw new ApplicationException("Category is required");
            }

            if (minPrice is > 0 && maxPrice > minPrice)
            {
                filter &= Builders<Product>.Filter.Where(
                    p => !p.Details.IsDiscounted ? p.Price.Amount >= minPrice : p.DiscountedPrice.Amount >= minPrice);
            }

            if (maxPrice is > 0 && maxPrice > minPrice)
            {
                filter &= Builders<Product>.Filter.Where(
                    p => !p.Details.IsDiscounted ? p.Price.Amount <= maxPrice : p.DiscountedPrice.Amount <= maxPrice);
            }

            if (isDiscounted == true)
            {
                filter &= Builders<Product>.Filter.Eq(p => p.Details.IsDiscounted, true);
            }

            if (isAvailable == true)
            {
                filter &= Builders<Product>.Filter.Eq(p => p.Details.IsAvailable, true);
            }

            return filter;
        }

        private static string RemovePolishAccents(string input)
        {
            string normalized = input.Normalize(NormalizationForm.FormD);
            StringBuilder result = new StringBuilder();

            for (int i = 0; i < normalized.Length; i++)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(normalized[i]) != UnicodeCategory.NonSpacingMark)
                {
                    if (normalized[i] != 322)
                        result.Append(normalized[i]);
                    else
                        result.Append('l');
                }
            }

            return result.ToString();
        }
    }
}