namespace SwimResults.Pages
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using DataModels;
    using DataTemplates.Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using SwimResults.Models;

    public class IntervalDetailsModel : PageModel
    {
        private readonly IRepository<WorkoutInterval> _intervalRepository;
        private readonly IMapper _mapper;

        public IntervalDetailsModel(IRepository<WorkoutInterval> intervalRepository, IMapper mapper)
        {
            _intervalRepository = intervalRepository ?? throw new ArgumentNullException(nameof(intervalRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public WorkoutIntervalViewModel Interval { get; set; }
        //public int IntervalNo { get; set; }
        public string ReturnPath { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id, string returnPath)
        {
            if (id == null)
            {
                return NotFound();
            }

            var storedInterval = await _intervalRepository.Get(id.Value, i => i.Lengths, i => i.WorkoutIntervalType, i => i.Workout);
            if (storedInterval == null)
            {
                return NotFound();
            }

            ReturnPath = string.IsNullOrWhiteSpace(returnPath) ? Url.Page("Details", new { id }) : returnPath;

            Interval = _mapper.Map<WorkoutIntervalViewModel>(storedInterval);
            var lengths = Interval.Lengths.OrderBy(l => l.LengthNo).ToList();
            Interval.Lengths = lengths;

            //IntervalNo = intervalNo ?? 0;
            return Page();
        }
    }
}