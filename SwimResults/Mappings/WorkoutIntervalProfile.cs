namespace SwimResults.Mappings
{
    using AutoMapper;
    using DataAccess.Models;
    using SwimResults.Models;

    public class WorkoutIntervalProfile : Profile
    {
        public WorkoutIntervalProfile()
        {
            CreateMap<WorkoutInterval, WorkoutIntervalViewModel>();
            CreateMap<WorkoutInterval, WorkoutIntervalEditModel>();
            CreateMap<WorkoutIntervalEditModel, WorkoutInterval>();
            CreateMap<WorkoutInterval, WorkoutInterval>();
        }
    }
}
