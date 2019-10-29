namespace SwimResults.Pages
{
    using System;
    using System.Threading.Tasks;
    using AutoMapper;
    using DataModels;
    using DataTemplates.Interfaces;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using SwimResults.Models;
    using SwimResults.Tools;

    public class IndexModel : PageModel
    {
        private readonly IRepository<Workout> _repository;
        private readonly IMapper _mapper;
        private readonly int _pageSize;

        public IndexModel(IRepository<Workout> repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _pageSize = 10;
        }

        public ListWithPaging<WorkoutViewModel> Workouts { get; internal set; }


        public async Task OnGetAsync(int? pageNo)
        {
            var pageIndex = ValuesHelper.GetPageIndex(pageNo);

            var workoutList = await _repository.GetList(w => w.Start, true, _pageSize, pageIndex);
            var totalCount = await _repository.GetCount();

            Workouts = new ListWithPaging<WorkoutViewModel>(totalCount, pageIndex, _pageSize, nameof(pageNo));

            foreach (var item in workoutList)
            {
                Workouts.Add(_mapper.Map<WorkoutViewModel>(item));
            }
        }
    }
}
