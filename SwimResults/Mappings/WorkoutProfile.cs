namespace SwimResults.Mappings
{
    using AutoMapper;
    using DataAccess.Models;
    using SwimResults.Models;

    public class WorkoutProfile : Profile
    {
        public WorkoutProfile()
        {
            CreateMap<Workout, Workout>();
            CreateMap<Workout, WorkoutViewModel>();
            CreateMap<Workout, WorkoutEditViewModel>();
            CreateMap<WorkoutEditViewModel, Workout>();
            CreateMap<Workout, WorkoutCreateViewModel>();
            CreateMap<WorkoutCreateViewModel, Workout>();
        }
    }
}
