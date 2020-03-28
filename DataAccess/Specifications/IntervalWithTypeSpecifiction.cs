namespace DataAccess.Specifications
{
    using DataModels;
    using Microsoft.EntityFrameworkCore;
    using NP.DataTemplates.Specifications;

    public class IntervalWithTypeSpecifiction : IncludeSpecification<WorkoutInterval>
    {
        public IntervalWithTypeSpecifiction() : base(q => q.Include(wi => wi.WorkoutIntervalType))
        {
        }
    }
}
