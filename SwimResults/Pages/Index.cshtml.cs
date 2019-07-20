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
        private readonly int _pageSize;

        public IndexModel(WorkoutRepository repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(WorkoutRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _pageSize = 10;
        }

        public IList<WorkoutViewModel> Workouts { get;set; }
        public int PageNo { get; set; }
        public int TotalPages { get; set; }
        public bool HasPreviousPage => PageNo > 1;
        public bool HasNextPage => PageNo < TotalPages; 

        public async Task OnGetAsync(int? pageNo)
        {
            var pageIndex = pageNo.HasValue ? pageNo.Value > 1 ? pageNo.Value - 1 : 0 : 0;
            PageNo = pageIndex + 1;
            var workoutList = (await _repository.GetList(w => w.Start, true, _pageSize, pageIndex)).ToList();
            var totalCount = await _repository.GetCount();
            TotalPages = (int)Math.Ceiling(totalCount / (double)_pageSize);

            Workouts = new List<WorkoutViewModel>();
            foreach (var item in workoutList)
            {
                Workouts.Add(_mapper.Map<WorkoutViewModel>(item));
            }
        }
    }
}
