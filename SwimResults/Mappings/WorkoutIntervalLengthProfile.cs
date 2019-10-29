namespace SwimResults.Mappings
{
    using AutoMapper;
    using DataModels;
    using SwimResults.Models;

    public class WorkoutIntervalLengthProfile : Profile
    {
        public WorkoutIntervalLengthProfile()
        {
            CreateMap<WorkoutIntervalLength, WorkoutIntervalLengthViewModel>();
        }
    }
}
