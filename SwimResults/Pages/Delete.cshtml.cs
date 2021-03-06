﻿namespace SwimResults.Pages
{
    using System.Threading.Tasks;
    using AutoMapper;
    using Constants;
    using DataModels;
    using Microsoft.AspNetCore.Mvc;
    using NP.DataTemplates.Interfaces;
    using NP.Helpers;
    using SwimResults.Core;
    using SwimResults.Models;

    //[SmartBreadcrumbs.Attributes.Breadcrumb("Delete workout")]
    public class DeleteModel : MyPageModel
    {
        private readonly IRepository<Workout> _workoutRepository;
        private readonly IMapper _mapper;

        public DeleteModel(IRepository<Workout> workoutRepository, IMapper mapper)
        {
            _workoutRepository = Validators.ValidateNotNull(workoutRepository, nameof(workoutRepository));
            _mapper = Validators.ValidateNotNull(mapper, nameof(mapper));
        }

        [BindProperty]
        public WorkoutViewModel Workout { get; set; }

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
            Workout = _mapper.Map<WorkoutViewModel>(storedWorkout);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            await _workoutRepository.Delete(id.Value)
                .ConfigureAwait(false);

            var returnPath = (string)TempData[ValueKeys.TempDataReturnPathKey];
            if (string.IsNullOrWhiteSpace(returnPath))
            {
                return RedirectToPage("./Index");
            }

            return Redirect(returnPath);
        }
    }
}
