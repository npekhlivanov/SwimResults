namespace DataAccess.Repositories
{
    using DataModels;
    using DataTemplates.Interfaces;
    using DataTemplates.Repositories;

    internal class WorkoutIntervalLengthRepository : ReadOnlyRepository<WorkoutIntervalLength>, IReadOnlyRepository<WorkoutIntervalLength>
    {
        public WorkoutIntervalLengthRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
