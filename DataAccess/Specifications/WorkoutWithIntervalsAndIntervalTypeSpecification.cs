namespace DataAccess.Specifications
{
    using DataModels;
    using Microsoft.EntityFrameworkCore;
    using NP.DataTemplates.Specifications;

    public class WorkoutWithIntervalsAndIntervalTypeSpecification : IncludeSpecification<Workout>
    {
        public WorkoutWithIntervalsAndIntervalTypeSpecification() : base(
            q => q
                .Include(w => w.Intervals)
                .ThenInclude(wi => wi.WorkoutIntervalType))
        {
        }
    }
}
