namespace DataAccess.Specifications
{
    using DataModels;
    using NP.DataTemplates.Specifications;

    public class WorkoutsSortedByDateSpecification : BaseQuerySpecification<Workout>
    {
        public WorkoutsSortedByDateSpecification() : base(
            null,
            null,
            new SortOrderSpecification<Workout>(w => w.Start, true),
            null)
        {
        }
    }
}
