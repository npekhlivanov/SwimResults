namespace DataAccess.Repositories
{
    using DataModels;
    using NP.DataTemplates.Interfaces;
    using NP.DataTemplates.Repositories;

    internal class WorkoutIntervalRepository : Repository<WorkoutInterval>, IRepository<WorkoutInterval>
    {
        public WorkoutIntervalRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
