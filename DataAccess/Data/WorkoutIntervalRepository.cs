namespace DataAccess.Data
{
    using DataAccess.Models;
    using DataTemplates.Interfaces;
    using DataTemplates.Repositories;

    public class WorkoutIntervalRepository : Repository<WorkoutInterval>, IRepository<WorkoutInterval>
    {
        public WorkoutIntervalRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
