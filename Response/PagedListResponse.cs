using Azure.Core;
using Microsoft.EntityFrameworkCore;

namespace MultipleDBContext.Api.Response
{
    public class PagedListResponse<T>
    {

        private PagedListResponse(List<T> items, int page, int pageSize, int totalCount, int totalPage)
        {

            Items = items;
            Page = page;
            PageSize = pageSize;
            TotalCount = totalCount;
            TotalPage = totalPage;
        }

        public List<T> Items { get; }
        public int Page { get; }

        public int PageSize { get; }
        public int TotalCount { get; }

        public int TotalPage { get; }

        public bool HasNextPage => Page * PageSize < TotalCount;
        public bool HasPreviousPage => PageSize > 1;


        public static async Task<PagedListResponse<T>> CreateAsync(IQueryable<T> query, int page, int pageSize,CancellationToken token)
        {
            var totalCount = await query.CountAsync(token);

            var skip = checked((page - 1) * pageSize);

            var items = await query.Skip(skip).Take(pageSize).ToListAsync(token);

            int totalPage = totalCount % pageSize;

            return new PagedListResponse<T>(items, page, pageSize, totalCount,totalPage);
        }
    }
}
