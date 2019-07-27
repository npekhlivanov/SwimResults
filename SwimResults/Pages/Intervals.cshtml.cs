namespace SwimResults.Pages
{
    using AutoMapper;
    using DataAccess.Data;
    using DataAccess.Models;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using SwimResults.Models;
    using SwimResults.Tools;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public class IntervalsModel : PageModel
    {
        private readonly WorkoutIntervalRepository _intervalRepository;
        private readonly IMapper _mapper;
        private readonly int _pageSize;

        public ListWithPagingAndSorting<WorkoutIntervalViewModel> Intervals { get; private set; }

        public string SortBy { get; set; }
        public bool SortDescending { get; set; }

        //[TempData]
        //public string CurrentSortOrder { get; set; }

        //[TempData]
        //public bool ReversedOrder { get; set; }

        public const string DateSortKey = "Date";
        public const string DistanceSortKey = "Distance";
        public const string DurationSortKey = "Duration";
        public const string StrokeCountSortKey = "StrokeCount";

        public IntervalsModel(WorkoutIntervalRepository intervalRepository, IMapper mapper)
        {
            _intervalRepository = intervalRepository;
            _mapper = mapper;
            _pageSize = 14;
        }

        public async Task OnGet(int? pageNo, string sortBy, bool? descending)
        {
            Expression<Func<WorkoutInterval, dynamic>> sortSelector;
            switch (sortBy)
            {
                case DistanceSortKey: { sortSelector = i => i.Distance; break; }
                case DurationSortKey: { sortSelector = i => i.Duration; break; }
                case StrokeCountSortKey: { sortSelector = i => i.StrokeCount; break; }
                default: { sortSelector = i => i.Workout.Date; break; }
            }

            var newSortOrder = string.IsNullOrWhiteSpace(sortBy) ? DateSortKey : sortBy;
            var sortDescending = descending ?? true;

            var pageIndex = ValuesHelper.GetPageIndex(pageNo);

            var intervals = await _intervalRepository.GetList(sortSelector, sortDescending, _pageSize, pageIndex, i => i.WorkoutIntervalType, i => i.Workout);
            var totalCount = await _intervalRepository.GetCount();

            var sortKeys = new Dictionary<string, string>
            {
                { nameof(WorkoutIntervalViewModel.WorkoutDate), DateSortKey },
                { nameof(WorkoutIntervalViewModel.Distance), DistanceSortKey },
                { nameof(WorkoutIntervalViewModel.Duration), DurationSortKey },
                { nameof(WorkoutIntervalViewModel.StrokeCount), StrokeCountSortKey }
            };

            Intervals = new ListWithPagingAndSorting<WorkoutIntervalViewModel>(totalCount, pageIndex, _pageSize, nameof(pageNo), nameof(sortBy), nameof(descending), newSortOrder, sortDescending, sortKeys);

            foreach (var interval in intervals)
            {
                var displayItem = _mapper.Map<WorkoutIntervalViewModel>(interval);
                displayItem.WorkoutName = interval.Workout.Name;
                displayItem.WorkoutDate = interval.Workout.Date;
                Intervals.Add(displayItem);
            }
        }
    }
}