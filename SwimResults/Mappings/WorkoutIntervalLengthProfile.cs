namespace SwimResults.Mappings
{
    using AutoMapper;
    using DataAccess.Models;
    using SwimResults.Models;

    public class WorkoutIntervalLengthProfile : Profile
    {
        public WorkoutIntervalLengthProfile()
        {
            CreateMap<WorkoutIntervalLength, WorkoutIntervalLengthViewModel>();
        }
    }
}
