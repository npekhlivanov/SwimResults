namespace SwimResults.Pages
{
    using AutoMapper;
    using DataAccess.Data;
    using DataAccess.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.Extensions.Configuration;
    using SwimResults.Models;
    using System;
    using System.Globalization;
    using System.Threading.Tasks;

    public class CreateModel : PageModel
    {
        private readonly WorkoutRepository _workoutRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public CreateModel(WorkoutRepository workoutRepository, IMapper mapper, IConfiguration configuration)
        {
            _workoutRepository = workoutRepository;
            _mapper = mapper;
            _configuration = configuration;
        }

        [BindProperty]
        public WorkoutCreateViewModel Workout { get; set; }

        public IActionResult OnGet()
        {
            var time = DateTime.Now;
            var morningOrAfternoon = time.Hour < 12 ? "Morning" : "Afternoon";
            Workout = new WorkoutCreateViewModel
            {
                Date = time.Date,
                Name = $"{time.ToString("dddd", CultureInfo.InvariantCulture)} {morningOrAfternoon} Swim {time.ToString("dd.MM.yyyy")}",
                Place = _configuration["DefaultSwimPlace"]
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
            workout.Start = DateTime.Now;
            //workout.Date = DateTime.Today;
            await _workoutRepository.Add(workout);

            return RedirectToPage("./Index");
        }
    }
}