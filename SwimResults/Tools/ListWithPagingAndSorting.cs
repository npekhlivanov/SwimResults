namespace SwimResults.Tools
{
    using System.Collections.Generic;

    public class ListWithPagingAndSorting<T> : ListWithPaging<T>
    {
        public ListWithPagingAndSorting(int totalCount, int pageIndex, int pageSize, string pageNoParamName, string sortByParamName, string descendingParamName, string sortBy, bool descending,
            Dictionary<string, string> sortKeys, string disabledClass = "disabled") : base(totalCount, pageIndex, pageSize, pageNoParamName, disabledClass)
        {
            Initialize(sortByParamName, descendingParamName, sortBy, descending, sortKeys);
        }

        public string CurrentSortOrder { get; private set; }

        public bool ReversedOrder { get; private set; }

        public Dictionary<string, bool> IsCurrentSortOrder { get; private set; }

        public Dictionary<string, Dictionary<string, string>> SortRouteValues { get; private set; }

        protected void Initialize(string sortByParamName, string descendingParamName, string sortBy, bool descending, Dictionary<string, string> sortKeys)
        {
            CurrentSortOrder = sortBy;
            ReversedOrder = descending;

            IsCurrentSortOrder = new Dictionary<string, bool>();
            foreach (var sortKey in sortKeys)
            {
                IsCurrentSortOrder.Add(sortKey.Key, sortKey.Value == sortBy);
            }

            var descendingValue = ReversedOrder.ToString();

            FirstPageRouteValues.Add(sortByParamName, sortBy);
            FirstPageRouteValues.Add(descendingParamName, descendingValue);

            LastPageRouteValues.Add(sortByParamName, sortBy);
            LastPageRouteValues.Add(descendingParamName, descendingValue);

            NextPageRouteValues.Add(sortByParamName, sortBy);
            NextPageRouteValues.Add(descendingParamName, descendingValue);

            PrevPageRouteValues.Add(sortByParamName, sortBy);
            PrevPageRouteValues.Add(descendingParamName, descendingValue);

            SortRouteValues = new Dictionary<string, Dictionary<string, string>>();
            foreach (var sortKey in sortKeys)
            {
                var routeValues = new Dictionary<string, string> { { sortByParamName, sortKey.Value }, { descendingParamName, (!descending).ToString() } };
                SortRouteValues.Add(sortKey.Key, routeValues);
            }
        }
    }
}
