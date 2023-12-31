﻿namespace Shared.Domain.ResponseTypes
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

        private PagedList(List<T> items, Page page, PageSize pageSize, int totalCount)
        {
            Items = items;
            Page = page;
            PageSize = pageSize;
            TotalCount = totalCount;
        }

        public static PagedList<T> Create(
            List<T> collection, Page page, PageSize pageSize)
        {
            var totalCount = collection.Count;

            var skipAmount = (page - 1) * pageSize;

            var items = collection
                .Skip(skipAmount)
                .Take(pageSize)
                .ToList();

            return new(items, page, pageSize, totalCount);
        }
    }
}