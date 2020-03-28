namespace SwimResults.Pages
{
    using System;
    using System.Threading.Tasks;
    using AutoMapper;
    using DataAccess.Specifications;
    using DataModels;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using NP.DataTemplates.Interfaces;
    using SmartBreadcrumbs.Attributes;
    using SwimResults.Core;
    using SwimResults.Models;
    using SwimResults.Tools;

    [DefaultBreadcrumb("Swims")]
    public class IndexModel : MyPageModel
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

            //static IQueryable<Workout> queryModifier(IQueryable<Workout> q) => q.Include(w => w.Start).OrderByDescending(w => w.Start);
            var workoutList = await _repository.GetList(new WorkoutsSortedByDateWithPagingSpecification(pageIndex, _pageSize))
               .ConfigureAwait(false);
            //var workoutList = await _repository.GetList(w => w.Start, true, _pageSize, pageIndex)
            var totalCount = await _repository.GetCount()
                .ConfigureAwait(false);

            Workouts = new ListWithPaging<WorkoutViewModel>(totalCount, pageIndex, _pageSize, nameof(pageNo));

            foreach (var item in workoutList)
            {
                Workouts.Add(_mapper.Map<WorkoutViewModel>(item));
            }
        }
    }
}
