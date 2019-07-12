namespace SwimResults.Pages
{
    using AutoMapper;
    using DataAccess.Data;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using SwimResults.Models;
    using System.Threading.Tasks;

    public class DeleteModel : PageModel
    {
        private WorkoutRepository _workoutRepository;
        private IMapper _mapper;

        public DeleteModel(WorkoutRepository workoutRepository, IMapper mapper)
        {
            _workoutRepository = workoutRepository;
            _mapper = mapper;
        }

        [BindProperty]
        public WorkoutViewModel Workout { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var storedWorkout = await _workoutRepository.Get(id.Value);
            if (storedWorkout == null)
            {
                return NotFound();
            }

            Workout = _mapper.Map<WorkoutViewModel>(storedWorkout);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            await _workoutRepository.Delete(id.Value);
            return RedirectToPage("./Index");
        }
    }
}
