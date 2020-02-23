namespace SwimResults.Tools
{
    using System;
    using System.Collections.Generic;
    using NP.Helpers.Extensions;

    public class ListWithPaging<T> : List<T>
    {
        public int PageNo { get; private set; }

        public int TotalPages { get; private set; }

        public bool HasPreviousPage => PageNo > 1;

        public bool HasNextPage => PageNo < TotalPages;

        public Dictionary<string, string> FirstPageRouteValues { get; internal set; }

        public Dictionary<string, string> LastPageRouteValues { get; internal set; }

        public Dictionary<string, string> NextPageRouteValues { get; internal set; }

        public Dictionary<string, string> PrevPageRouteValues { get; internal set; }

        public string PrevPageDisabledClass { get; protected set; }

        public string NextPageDisabledClass { get; protected set; }

        public ListWithPaging(List<T> items, int totalCount, int pageIndex, int pageSize, string pageNoParamName, string disabledClass = "disabled")
        {
            Initialize(totalCount, pageIndex, pageSize, pageNoParamName, disabledClass);
            this.AddRange(items);
        }

        public ListWithPaging(int totalCount, int pageIndex, int pageSize, string pageNoParamName, string disabledClass = "disabled")
        {
            Initialize(totalCount, pageIndex, pageSize, pageNoParamName, disabledClass);
        }

        private void Initialize(int totalCount, int pageIndex, int pageSize, string pageNoParamName, string disabledClass)
        {
            PageNo = pageIndex + 1;
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            FirstPageRouteValues = new Dictionary<string, string> { { pageNoParamName, "1" } };
            LastPageRouteValues = new Dictionary<string, string> { { pageNoParamName, TotalPages.ToStringInvariant() } };
            NextPageRouteValues = new Dictionary<string, string> { { pageNoParamName, (HasNextPage ? PageNo + 1 : TotalPages).ToStringInvariant() } };
            PrevPageRouteValues = new Dictionary<string, string> { { pageNoParamName, (HasPreviousPage ? PageNo - 1 : 1).ToStringInvariant() } };

            NextPageDisabledClass = HasNextPage ? "" : disabledClass;
            PrevPageDisabledClass = HasPreviousPage ? "" : disabledClass;
        }

        //public static async Task<ListWithPaging<T>> CreateAsync(
        //    IQueryable<T> source, int pageIndex, int pageSize)
        //{
        //    var count = await source.CountAsync();
        //    var items = await source.Skip(
        //        (pageIndex - 1) * pageSize)
        //        .Take(pageSize).ToListAsync();
        //    return new ListWithPaging<T>(items, count, pageIndex, pageSize);
        //}
    }
}
