using MongoDB.Driver;
using OnlineShop.Catalog.API.Entities;
using System.Globalization;

namespace OnlineShop.Catalog.API.CustomTypes
{
    public class PagedList<T>
    {
        public List<T> Items { get; }
        public int Page { get; }
        public int PageSize { get; }
        public int TotalCount { get; }
        public bool HasNextPage => Page * PageSize < TotalCount;
        public bool HasPreviousPage => Page > 1;

        private PagedList(List<T> items, int page, int pageSize, int totalCount)
        {
            Items = items;
            Page = page;
            PageSize = pageSize;
            TotalCount = totalCount;
        }

        public static async Task<PagedList<T>> CreateAsync(
            List<T> collection, int page, int pageSize)
        {
            var totalCount = collection.Count();

            var skipAmount = (page - 1) * pageSize;

            var items = collection
                .Skip(skipAmount)
                .Take(pageSize)
                .ToList();

            return new PagedList<T>(items, page, pageSize, totalCount);
        }
    }
}