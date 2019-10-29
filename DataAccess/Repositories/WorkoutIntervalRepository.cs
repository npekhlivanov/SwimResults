namespace DataAccess.Repositories
{
    using DataModels;
    using DataTemplates.Interfaces;
    using DataTemplates.Repositories;

    internal class WorkoutIntervalRepository : Repository<WorkoutInterval>, IRepository<WorkoutInterval>
    {
        public WorkoutIntervalRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
