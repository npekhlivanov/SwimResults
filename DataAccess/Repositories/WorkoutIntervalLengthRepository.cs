namespace DataAccess.Repositories
{
    using DataModels;
    using NP.DataTemplates.Interfaces;
    using NP.DataTemplates.Repositories;

    internal class WorkoutIntervalLengthRepository : ReadOnlyRepository<WorkoutIntervalLength>, IReadOnlyRepository<WorkoutIntervalLength>
    {
        public WorkoutIntervalLengthRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
