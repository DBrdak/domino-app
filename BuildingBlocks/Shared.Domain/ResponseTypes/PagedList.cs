using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Domain.ResponseTypes
{
    public class PagedList<T>
    {
        public List<T> Items { get; }
        public int Page { get; }
        public int PageSize { get; }
        public int TotalCount { get; }
        public int TotalPages => (int)Math.Ceiling((decimal)TotalCount / PageSize);
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

            return new(items, page, pageSize, totalCount);
        }
    }
}