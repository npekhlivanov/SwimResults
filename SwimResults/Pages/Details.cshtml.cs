namespace SwimResults.Pages
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using DataAccess.Specifications;
    using DataModels;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using NP.DataTemplates.Interfaces;
    using SwimResults.Core;
    using SwimResults.Models;
    using SwimResults.Models.Api;
    using SwimResults.Tools;

    //[SmartBreadcrumbs.Attributes.Breadcrumb("Workout details")]
    public class DetailsModel : MyPageModel
    {
        private readonly IRepository<Workout> _workoutRepository;
        private readonly IMapper _mapper;
        private readonly IOptions<MvcOptions> options;

        public DetailsModel(IRepository<Workout> workoutRepository, IMapper mapper, IOptions<MvcOptions> options)
        {
            _workoutRepository = workoutRepository ?? throw new ArgumentNullException(nameof(workoutRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.options = options;
        }

        public WorkoutViewModel Workout { get; set; }

        public string ReturnPath { get; set; }

        public string SerializedIntervals { get; private set; }

        public async Task<IActionResult> OnGetAsync(int? id, string returnPath)
        {
            if (id == null)
            {
                return NotFound();
            }

            var storedWorkout = await _workoutRepository.GetById(id.Value, new WorkoutWithIntervalsAndLengthsSpecification())//WorkoutWithIntervalsAndIntervalTypeSpecification
                .ConfigureAwait(false);
            //var storedWorkout = await _workoutRepository.GetById(id.Value, w => w.Intervals, wi => ((WorkoutInterval)wi).WorkoutIntervalType); // i => ((WorkoutInterval)i).Lengths
            if (storedWorkout == null)
            {
                return NotFound();
            }

            //TempData[ValueKeys.TempDataReturnPathKey] = returnPath;
            ReturnPath = string.IsNullOrWhiteSpace(returnPath) ? Url.Page("Index") : returnPath;

            //storedWorkout.Intervals = await _workoutIntervalRepository.GetList(i => i.WorkoutId == storedWorkout.Id, i => i.Lengths);
            Workout = _mapper.Map<WorkoutViewModel>(storedWorkout);
            var intervals = Workout.Intervals
                .OrderBy(wi => wi.IntervalNo)
                .ToList();

            InsertRests(intervals);
            Workout.Intervals.Clear();
            Workout.Intervals.AddRange(intervals);

            var displayIntervals = new List<IntervalDisplayData>();
            foreach (var interval in storedWorkout.Intervals)
            {
                var obj = DisplayDataHelper.CreateIntervalDisplayData(interval);
                displayIntervals.Add(obj);
            }

            //var formatter = (Microsoft.AspNetCore.Mvc.Formatters.NewtonsoftJsonOutputFormatter)options.Value.OutputFormatters.First(f => f.GetType() == typeof(Microsoft.AspNetCore.Mvc.Formatters.NewtonsoftJsonOutputFormatter));
            var settings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };
            this.SerializedIntervals = JsonConvert.SerializeObject(displayIntervals, settings);//, );
            // config.Formatters.JsonFormatter.SerializerSettings.ContractResolver

            //var requestWithParams = QueryHelpers.ParseQuery(Request.QueryString.Value)
            //    .Select(p => new { p.Key, Value = p.Value.ToString() })
            //    .ToDictionary(a => a.Key, v => v.Value);
            //requestWithParams.Remove(nameof(showRests));

            return Page();
        }

        private static void InsertRests(System.Collections.Generic.List<WorkoutIntervalViewModel> intervals)
        {
            var i = 0;
            while (++i < intervals.Count)
            {
                var interval = intervals[i];
                if (i > 0)
                {
                    var prevInterval = intervals[i - 1];
                    var prevIntervalEnd = prevInterval.TimeOffset + prevInterval.Duration;
                    var delta = interval.TimeOffset - prevIntervalEnd;
                    if (delta > 1)
                    {
                        intervals.Insert(i++, new WorkoutIntervalViewModel { TimeOffset = prevIntervalEnd, Duration = delta, Notes = "Rest" });
                    }
                }
            }
        }
    }
}
