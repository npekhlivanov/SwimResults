namespace DataAccess.Specifications
{
    using DataModels;
    using Microsoft.EntityFrameworkCore;
    using NP.DataTemplates.Specifications;

    public class IntervalWithTypeAndLengthsAndWorkoutSpecification : IncludeSpecification<WorkoutInterval>
    {
        public IntervalWithTypeAndLengthsAndWorkoutSpecification() : base(
            q => q
                .Include(i => i.Lengths)
                .Include(i => i.WorkoutIntervalType)
                .Include(i => i.Workout))
        {
        }
    }
}
