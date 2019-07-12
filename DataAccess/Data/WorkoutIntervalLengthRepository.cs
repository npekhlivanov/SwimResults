namespace DataAccess.Data
{
    using DataAccess.Models;
    using DataTemplates.Repositories;

    public class WorkoutIntervalLengthRepository : ReadOnlyRepository<WorkoutIntervalLength>
    {
        public WorkoutIntervalLengthRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
