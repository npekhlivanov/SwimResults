﻿namespace SwimResults.Pages
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using AutoMapper;
    using DataModels;
    using DataTemplates.Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using NP.Helpers.Extensions;
    using SwimResults.Models;
    using SwimResults.Tools;

    public class IntervalsModel : PageModel
    {
        private readonly IRepository<WorkoutInterval> _intervalRepository;
        private readonly IRepository<WorkoutIntervalType> _intervalTypeRepository;
        private readonly IMapper _mapper;
        private readonly int _pageSize;

        public ListWithPagingAndSorting<WorkoutIntervalViewModel> Intervals { get; private set; }

        //public string SortBy { get; set; }
        //public bool SortDescending { get; set; }

        public SelectList WorkoutIntervalTypeSelectList { get; private set; }

        [BindProperty(SupportsGet = true)]
        public int SelectedIntervalType { get; set; }
        //[TempData]
        //public string CurrentSortOrder { get; set; }

        //[TempData]
        //public bool ReversedOrder { get; set; }

        public static string DateFieldName => nameof(WorkoutIntervalViewModel.WorkoutDate);
        public static string DistanceFieldName => nameof(WorkoutIntervalViewModel.Distance);
        public static string DurationFieldName => nameof(WorkoutIntervalViewModel.Duration);
        public static string StrokeCountFieldName => nameof(WorkoutIntervalViewModel.StrokeCount);
        public static string PaceFieldName => nameof(WorkoutIntervalViewModel.Pace);
        public static string SwolfFieldName => nameof(WorkoutIntervalViewModel.Swolf);

        private const string DateSortKey = "Date";
        private const string DistanceSortKey = "Distance";
        private const string DurationSortKey = "Duration";
        private const string StrokeCountSortKey = "StrokeCount";
        private const string PaceSortKey = "Pace";
        private const string SwolfSortKey = "Swolf";

        public IntervalsModel(IRepository<WorkoutInterval> intervalRepository, IRepository<WorkoutIntervalType> intervalTypeRepository, IMapper mapper)
        {
            _intervalRepository = intervalRepository ?? throw new ArgumentNullException(nameof(intervalRepository));
            _intervalTypeRepository = intervalTypeRepository ?? throw new ArgumentNullException(nameof(intervalTypeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _pageSize = 10;
        }

        public async Task OnGet(int? pageNo, string sortBy, bool? descending)
        {
            Expression<Func<WorkoutInterval, dynamic>> sortSelector;
            switch (sortBy)
            {
                case DistanceSortKey: { sortSelector = i => i.Distance; break; }
                case DurationSortKey: { sortSelector = i => i.Duration; break; }
                case StrokeCountSortKey: { sortSelector = i => i.StrokeCount; break; }
                case PaceSortKey: { sortSelector = i => i.Pace; break; }
                case SwolfSortKey: { sortSelector = i => i.Swolf; break; }
                default: { sortSelector = i => i.Workout.WorkoutDate; break; }
            }

            var newSortOrder = string.IsNullOrWhiteSpace(sortBy) ? DateSortKey : sortBy;
            var sortDescending = descending ?? true;

            var pageIndex = ValuesHelper.GetPageIndex(pageNo);

            var intervals = await _intervalRepository.GetList(i => i.WorkoutIntervalTypeId == SelectedIntervalType, sortSelector, sortDescending, _pageSize, pageIndex, i => i.WorkoutIntervalType, i => i.Workout);
            var totalCount = await _intervalRepository.GetCount(i => i.WorkoutIntervalTypeId == SelectedIntervalType);

            var sortKeys = new Dictionary<string, string>
            {
                { DateFieldName, DateSortKey },
                { DistanceFieldName, DistanceSortKey },
                { DurationFieldName, DurationSortKey },
                { StrokeCountFieldName, StrokeCountSortKey },
                { PaceFieldName, PaceSortKey },
                { SwolfFieldName, SwolfSortKey }
            };

            Intervals = new ListWithPagingAndSorting<WorkoutIntervalViewModel>(totalCount, pageIndex, _pageSize, nameof(pageNo), nameof(sortBy), nameof(descending), newSortOrder, sortDescending, sortKeys);


            foreach (var interval in intervals)
            {
                var displayItem = _mapper.Map<WorkoutIntervalViewModel>(interval);
                displayItem.WorkoutName = interval.Workout.Name;
                displayItem.WorkoutDate = interval.Workout.WorkoutDate;
                Intervals.Add(displayItem);
            }

            Intervals.FirstPageRouteValues.Add(nameof(SelectedIntervalType), SelectedIntervalType.ToStringInvariant());
            Intervals.LastPageRouteValues.Add(nameof(SelectedIntervalType), SelectedIntervalType.ToStringInvariant());
            Intervals.PrevPageRouteValues.Add(nameof(SelectedIntervalType), SelectedIntervalType.ToStringInvariant());
            Intervals.NextPageRouteValues.Add(nameof(SelectedIntervalType), SelectedIntervalType.ToStringInvariant());
            foreach (var routeValues in Intervals.SortRouteValues)
            {
                routeValues.Value.Add(nameof(SelectedIntervalType), SelectedIntervalType.ToStringInvariant());
            }

            var intervalTypes = await _intervalTypeRepository.GetList();
            WorkoutIntervalTypeSelectList = new SelectList(intervalTypes, "Id", "Name");
        }
    }
}