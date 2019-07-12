namespace SwimResults.Pages
{
    using AutoMapper;
    using DataAccess.Data;
    using DataAccess.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using SwimResults.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class IntervalDetailsModel : PageModel
    {
        private readonly WorkoutIntervalRepository _intervalRepository;
        private readonly IMapper _mapper;

        public IntervalDetailsModel(WorkoutIntervalRepository intervalRepository, IMapper mapper)
        {
            _intervalRepository = intervalRepository;
            _mapper = mapper;
        }

        public WorkoutIntervalViewModel Interval { get; set; }
        public int IntervalNo { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id, int? intervalNo)
        {
            if (id == null)
            {
                return NotFound();
            }

            var storedInterval = await _intervalRepository.Get(id.Value, i => i.Lengths, i => i.WorkoutIntervalType); 
            if (storedInterval == null)
            {
                return NotFound();
            }

            Interval = _mapper.Map<WorkoutIntervalViewModel>(storedInterval);
            IntervalNo = intervalNo ?? 0;
            return Page();
        }
    }
}