namespace SwimResults.Pages
{
    using System;
    using System.Threading.Tasks;
    using AutoMapper;
    using Constants;
    using DataModels;
    using DataTemplates.Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using SwimResults.Models;

    public class DeleteModel : PageModel
    {
        private readonly IRepository<Workout> _workoutRepository;
        private readonly IMapper _mapper;

        public DeleteModel(IRepository<Workout> workoutRepository, IMapper mapper)
        {
            _workoutRepository = workoutRepository ?? throw new ArgumentNullException(nameof(workoutRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [BindProperty]
        public WorkoutViewModel Workout { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id, string returnPath)
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

            TempData[ValueKeys.TempDataReturnPathKey] = returnPath;
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

            var returnPath = (string)TempData[ValueKeys.TempDataReturnPathKey];
            if (string.IsNullOrWhiteSpace(returnPath))
            {
                return RedirectToPage("./Index");
            }

            return Redirect(returnPath);
        }
    }
}
