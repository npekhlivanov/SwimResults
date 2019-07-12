namespace DataAccess.Data
{
    using DataAccess.Models;
    using DataTemplates.Interfaces;
    using DataTemplates.Repositories;

    public class WorkoutRepository : Repository<Workout>, IRepository<Workout>
    {
        public WorkoutRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
