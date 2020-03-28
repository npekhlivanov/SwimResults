namespace DataAccess.Specifications
{
    using DataModels;
    using Microsoft.EntityFrameworkCore;
    using NP.DataTemplates.Specifications;

    public class WorkoutWithIntervalsSpecification : IncludeSpecification<Workout>
    {
        public WorkoutWithIntervalsSpecification() : base(q => q.Include(w => w.Intervals))
        {
        }
    }
}
