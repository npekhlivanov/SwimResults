namespace SwimResults.Pages
{
    using AutoMapper;
    using DataAccess.Data;
    using DataAccess.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using SwimResults.Models;
    using System;
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

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var storedWorkout = await _workoutRepository.Get(id.Value, w => w.Intervals, i => ((WorkoutInterval)i).WorkoutIntervalType); // i => ((WorkoutInterval)i).Lengths
            if (storedWorkout == null)
            {
                return NotFound();
            }

            //storedWorkout.Intervals = await _workoutIntervalRepository.GetList(i => i.WorkoutId == storedWorkout.Id, i => i.Lengths);
            Workout = _mapper.Map<WorkoutViewModel>(storedWorkout);
            return Page();
        }
    }
}
