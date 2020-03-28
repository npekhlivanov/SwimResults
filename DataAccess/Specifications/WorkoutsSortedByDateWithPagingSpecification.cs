namespace DataAccess.Specifications
{
    using DataModels;
    using NP.DataTemplates.Specifications;

    public class WorkoutsSortedByDateWithPagingSpecification : BaseQuerySpecification<Workout>
    {
        public WorkoutsSortedByDateWithPagingSpecification(int pageNo, int pageSize) : base(
            null,
            null,
            new SortOrderSpecification<Workout>(w => w.Start, true),
            new PagingSpecification(pageNo, pageSize))
        {
        }
    }
}
