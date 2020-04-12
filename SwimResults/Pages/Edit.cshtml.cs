namespace SwimResults.Pages
{
    using System.Threading.Tasks;
    using AutoMapper;
    using Constants;
    using DataModels;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using NP.DataTemplates.Interfaces;
    using NP.Helpers;
    using SwimResults.Core;
    using SwimResults.Models;

    //[SmartBreadcrumbs.Attributes.Breadcrumb("Edit workout")]
    public class EditModel : MyPageModel
    {
        private readonly IRepository<Workout> _workoutRepository;
        private readonly IMapper _mapper;

        public EditModel(IRepository<Workout> workoutRepository, IMapper mapper)
        {
            _workoutRepository = Validators.ValidateNotNull(workoutRepository, nameof(workoutRepository));
            _mapper = Validators.ValidateNotNull(mapper, nameof(mapper));
        }

        [BindProperty]
        public WorkoutEditViewModel Workout { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id, string returnPath)
        {
            if (id == null)
            {
                return NotFound();
            }

            var storedWorkout = await _workoutRepository.GetById(id.Value)
                .ConfigureAwait(false);

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

            var storedWorkout = await _workoutRepository.GetById(Workout.Id)
                .ConfigureAwait(false);
            var modifiedWorkout = _mapper.Map<Workout>(storedWorkout);
            _mapper.Map(Workout, modifiedWorkout);

            //_context.Attach(Workout).State = EntityState.Modified;

            try
            {
                await _workoutRepository.UpdateModifiedFields(modifiedWorkout, storedWorkout)
                    .ConfigureAwait(false);
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
