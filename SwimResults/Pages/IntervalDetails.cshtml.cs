namespace SwimResults.Pages
{
    using System.Threading.Tasks;
    using AutoMapper;
    using DataAccess.Specifications;
    using DataModels;
    using Microsoft.AspNetCore.Mvc;
    using NP.DataTemplates.Interfaces;
    using NP.Helpers;
    using SwimResults.Core;
    using SwimResults.Models;

    //[SmartBreadcrumbs.Attributes.Breadcrumb("Interval details")]
    public class IntervalDetailsModel : MyPageModel
    {
        private readonly IRepository<WorkoutInterval> _intervalRepository;
        private readonly IMapper _mapper;

        public IntervalDetailsModel(IRepository<WorkoutInterval> intervalRepository, IMapper mapper)
        {
            _intervalRepository = Validators.ValidateNotNull(intervalRepository, nameof(intervalRepository));
            _mapper = Validators.ValidateNotNull(mapper, nameof(mapper));
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

            var storedInterval = await _intervalRepository.GetById(id.Value, new IntervalWithTypeAndLengthsAndWorkoutSpecification())
                .ConfigureAwait(false);
            //var storedInterval = await _intervalRepository.GetById(id.Value, i => i.Lengths, i => i.WorkoutIntervalType, i => i.Workout);
            if (storedInterval == null)
            {
                return NotFound();
            }

            ReturnPath = string.IsNullOrWhiteSpace(returnPath) ? Url.Page("Details", new { id }) : returnPath;

            Interval = _mapper.Map<WorkoutIntervalViewModel>(storedInterval);
            //var lengths = Interval.Lengths.OrderBy(l => l.LengthNo).ToList();
            //Interval.Lengths.Clear();
            //Interval.Lengths.AddRange(lengths);

            //IntervalNo = intervalNo ?? 0;
            return Page();
        }
    }
}