namespace SwimResults.Pages
{
    using AutoMapper;
    using DataAccess.Data;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using SwimResults.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class IndexModel : PageModel
    {
        private readonly WorkoutRepository _repository;
        private readonly IMapper _mapper;

        public IndexModel(WorkoutRepository repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(WorkoutRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public IList<WorkoutViewModel> Workouts { get;set; }

        public async Task OnGetAsync()
        {
            var workoutList = (await _repository.GetList(w => w.Start, true, 10, 0)).ToList();
            Workouts = new List<WorkoutViewModel>();
            foreach (var item in workoutList)
            {
                Workouts.Add(_mapper.Map<WorkoutViewModel>(item));
            }
        }
    }
}
