﻿namespace SwimResults.Tools
{
    using System;
    using System.Collections.Generic;
    using NP.Helpers.Extensions;

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
            if (sortKeys == null)
            {
                throw new ArgumentNullException(nameof(sortKeys));
            }

            CurrentSortOrder = sortBy;
            ReversedOrder = descending;

            IsCurrentSortOrder = new Dictionary<string, bool>();
            foreach (var sortKey in sortKeys)
            {
                IsCurrentSortOrder.Add(sortKey.Key, sortKey.Value == sortBy);
            }

            var descendingValue = ReversedOrder.ToStringInvariant();

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
                var routeValues = new Dictionary<string, string> { { sortByParamName, sortKey.Value }, { descendingParamName, (!descending).ToStringInvariant() } };
                SortRouteValues.Add(sortKey.Key, routeValues);
            }
        }
    }
}
