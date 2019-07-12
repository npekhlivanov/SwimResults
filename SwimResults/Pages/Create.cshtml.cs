namespace SwimResults.Pages
{
    using AutoMapper;
    using DataAccess.Data;
    using DataAccess.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using SwimResults.Models;
    using System.Threading.Tasks;

    public class CreateModel : PageModel
    {
        private readonly WorkoutRepository _workoutRepository;
        private readonly IMapper _mapper;

        public CreateModel(WorkoutRepository workoutRepository, IMapper mapper)
        {
            _workoutRepository = workoutRepository;
            _mapper = mapper;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public WorkoutCreateViewModel Workout { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var workout = _mapper.Map<Workout>(Workout);
            workout.Start = System.DateTime.Now;
            workout.Date = System.DateTime.Today;
            await _workoutRepository.Add(workout);

            return RedirectToPage("./Index");
        }
    }
}