namespace SwimResults.Pages
{
    using System;
    using System.Threading.Tasks;
    using AutoMapper;
    using DataModels;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.Extensions.Configuration;
    using NP.DataTemplates.Interfaces;
    using NP.Helpers;
    using SwimResults.Core;
    using SwimResults.Models;
    using SwimResults.Tools;

    [SmartBreadcrumbs.Attributes.Breadcrumb("Intervals")]
    public class CreateModel : MyPageModel
    {
        private readonly IRepository<Workout> _workoutRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public CreateModel(IRepository<Workout> workoutRepository, IMapper mapper, IConfiguration configuration)
        {
            _workoutRepository = Validators.ValidateNotNull(workoutRepository, nameof(workoutRepository));
            _mapper = Validators.ValidateNotNull(mapper, nameof(mapper));
            _configuration = Validators.ValidateNotNull(configuration, nameof(configuration));
        }

        [BindProperty]
        public WorkoutCreateViewModel Workout { get; set; }

        public IActionResult OnGet()
        {
            var time = DateTime.Now;
            var isMorning = time.Hour < 12;
            Workout = new WorkoutCreateViewModel
            {
                Date = time.Date,
                Name = ValuesHelper.ComposeWorkoutName(time, isMorning),
                Place = _configuration["DefaultSwimPlace"],
                IsMorning = isMorning
            };
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var workout = _mapper.Map<Workout>(Workout);
            workout.Start = new DateTime(workout.WorkoutDate.Year, workout.WorkoutDate.Month, workout.WorkoutDate.Day, DateTime.Now.Hour, 0, 0);
            await _workoutRepository.Add(workout)
                .ConfigureAwait(false);

            return RedirectToPage("./Index");
        }
    }
}