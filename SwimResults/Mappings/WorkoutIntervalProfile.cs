namespace SwimResults.Mappings
{
    using System.Linq;
    using AutoMapper;
    using DataModels;
    using SwimResults.Models;

    public class WorkoutIntervalProfile : Profile
    {
        public WorkoutIntervalProfile()
        {
            CreateMap<WorkoutInterval, WorkoutIntervalViewModel>()
                .ForMember(dest => dest.Lengths, opt => opt.MapFrom(src => src.Lengths.OrderBy(l => l.LengthNo)));
            CreateMap<WorkoutInterval, WorkoutIntervalEditModel>();
            CreateMap<WorkoutIntervalEditModel, WorkoutInterval>();
            CreateMap<WorkoutInterval, WorkoutInterval>();
        }
    }
}
