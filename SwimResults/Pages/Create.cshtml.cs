﻿namespace SwimResults.Pages
{
    using System;
    using System.Threading.Tasks;
    using AutoMapper;
    using DataModels;
    using DataTemplates.Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.Extensions.Configuration;
    using SwimResults.Models;
    using SwimResults.Tools;

    public class CreateModel : PageModel
    {
        private readonly IRepository<Workout> _workoutRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public CreateModel(IRepository<Workout> workoutRepository, IMapper mapper, IConfiguration configuration)
        {
            _workoutRepository = workoutRepository ?? throw new ArgumentNullException(nameof(workoutRepository)); 
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        [BindProperty]
        public WorkoutCreateViewModel Workout { get; set; }

        public IActionResult OnGet()
        {
            var time = DateTime.Now;
            Workout = new WorkoutCreateViewModel
            {
                Date = time.Date,
                Name = ValuesHelper.ComposeWorkoutName(time),
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
            workout.Start = new DateTime(workout.WorkoutDate.Year, workout.WorkoutDate.Month, workout.WorkoutDate.Day, DateTime.Now.Hour, 0, 0);
            await _workoutRepository.Add(workout);

            return RedirectToPage("./Index");
        }
    }
}