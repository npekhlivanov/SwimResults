using DataModels;
using Microsoft.EntityFrameworkCore;
using NP.DataTemplates.Specifications;

namespace DataAccess.Specifications
{
    public class WorkoutWithIntervalsAndLengthsSpecification : IncludeSpecification<Workout>
    {
        public WorkoutWithIntervalsAndLengthsSpecification() : base(
            q => q
                .Include(w => w.Intervals)
                .ThenInclude(wi => wi.Lengths)
                .Include(w => w.Intervals)
                .ThenInclude(wi => wi.WorkoutIntervalType))
        {
        }

        //private static IQueryable<Workout> IncludeMethod(IQueryable<Workout> input)
        //{
        //    var queriable = input
        //        .Include(w => w.Intervals)
        //        .ThenInclude(wi => wi.Lengths)
        //        .Include(w => w.Intervals)
        //        .ThenInclude(wi => wi.WorkoutIntervalType);
        //    return queriable;
        //}
    }
}
