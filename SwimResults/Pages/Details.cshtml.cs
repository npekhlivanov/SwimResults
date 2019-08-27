namespace SwimResults.Pages
{
    using AutoMapper;
    using Constants;
    using DataAccess.Data;
    using DataAccess.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using SwimResults.Models;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class DetailsModel : PageModel
    {
        private readonly WorkoutRepository _workoutRepository;
        private readonly IMapper _mapper;

        public DetailsModel(WorkoutRepository workoutRepository, IMapper mapper)
        {
            _workoutRepository = workoutRepository ?? throw new ArgumentNullException(nameof(workoutRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public WorkoutViewModel Workout { get; set; }

        public string ReturnPath { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id, string returnPath, bool? showRests)
        {
            if (id == null)
            {
                return NotFound();
            }

            var storedWorkout = await _workoutRepository.Get(id.Value, w => w.Intervals, wi => ((WorkoutInterval)wi).WorkoutIntervalType); // i => ((WorkoutInterval)i).Lengths
            if (storedWorkout == null)
            {
                return NotFound();
            }

            //TempData[ValueKeys.TempDataReturnPathKey] = returnPath;
            ReturnPath = string.IsNullOrWhiteSpace(returnPath) ? Url.Page("Index") : returnPath;

            //storedWorkout.Intervals = await _workoutIntervalRepository.GetList(i => i.WorkoutId == storedWorkout.Id, i => i.Lengths);
            Workout = _mapper.Map<WorkoutViewModel>(storedWorkout);
            var intervals = Workout.Intervals.OrderBy(wi => wi.IntervalNo).ToList();
            if (showRests ?? false)
            {
                InsertRests(intervals);
            }

            Workout.Intervals = intervals;
            return Page();
        }

        private static void InsertRests(System.Collections.Generic.List<WorkoutIntervalViewModel> intervals)
        {
            var i = 0;
            while (++i < intervals.Count())
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
