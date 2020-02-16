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
    using Microsoft.EntityFrameworkCore;
    using SwimResults.Models;

    public class EditModel : PageModel
    {
        private readonly IRepository<Workout> _workoutRepository;
        private readonly IMapper _mapper;

        public EditModel(IRepository<Workout> workoutRepository, IMapper mapper)
        {
            _workoutRepository = workoutRepository ?? throw new ArgumentNullException(nameof(workoutRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper)); 
        }

        [BindProperty]
        public WorkoutEditViewModel Workout { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id, string returnPath)
        {
            if (id == null)
            {
                return NotFound();
            }

            var storedWorkout = await _workoutRepository.GetById(id.Value);

            if (storedWorkout == null)
            {
                return NotFound();
            }

            TempData[ValueKeys.TempDataReturnPathKey] = returnPath;
            Workout = _mapper.Map<WorkoutEditViewModel>(storedWorkout);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var storedWorkout = await _workoutRepository.GetById(Workout.Id);
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

            var returnPath = (string)TempData[ValueKeys.TempDataReturnPathKey];
            if (string.IsNullOrEmpty(returnPath))
            {
                return RedirectToPage("./Index");
            }

            return Redirect(returnPath);
        }

        private bool WorkoutExists(int id)
        {
            var workout = _workoutRepository.GetById(id).Result;
            return workout != null;
        }
    }
}
