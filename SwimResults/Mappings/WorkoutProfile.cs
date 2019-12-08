namespace SwimResults.Mappings
{
    using AutoMapper;
    using DataModels;
    using SwimResults.Models;

    public class WorkoutProfile : Profile
    {
        public WorkoutProfile()
        {
            CreateMap<Workout, Workout>();
            CreateMap<Workout, WorkoutViewModel>()
                .ForMember(dest => dest.Date, opt => opt.MapFrom(source => source.WorkoutDate));
            CreateMap<Workout, WorkoutEditViewModel>();
            CreateMap<WorkoutEditViewModel, Workout>();
            //CreateMap<Workout, WorkoutCreateViewModel>();
            CreateMap<WorkoutCreateViewModel, Workout>()
                .ForMember(dest => dest.WorkoutDate, opt => opt.MapFrom(source => source.Date));
        }
    }
}
