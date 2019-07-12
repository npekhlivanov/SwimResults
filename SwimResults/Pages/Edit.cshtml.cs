namespace SwimResults.Pages
{
    using AutoMapper;
    using DataAccess.Data;
    using DataAccess.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.EntityFrameworkCore;
    using SwimResults.Models;
    using System.Threading.Tasks;

    public class EditModel : PageModel
    {
        private readonly WorkoutRepository _workoutRepository;
        private readonly IMapper _mapper;

        public EditModel(WorkoutRepository workoutRepository, IMapper mapper)
        {
            _workoutRepository = workoutRepository;
            _mapper = mapper;
        }

        [BindProperty]
        public WorkoutEditViewModel Workout { get; set; }

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

            Workout = _mapper.Map<WorkoutEditViewModel>(storedWorkout);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var storedWorkout = await _workoutRepository.Get(Workout.Id);
            var modifiedWorkout = _mapper.Map<Workout>(storedWorkout);
            _mapper.Map(Workout, modifiedWorkout);

            //_context.Attach(Workout).State = EntityState.Modified;

            try
            {
                await _workoutRepository.UpdateModifiedFields(modifiedWorkout, storedWorkout);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkoutExists(Workout.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool WorkoutExists(int id)
        {
            var workout = _workoutRepository.Get(id).Result;
            return workout != null;
        }
    }
}
