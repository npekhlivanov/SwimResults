namespace DataAccess.Repositories
{
    using DataModels;
    using DataTemplates.Interfaces;
    using DataTemplates.Repositories;

    internal class WorkoutRepository : Repository<Workout>, IRepository<Workout>
    {
        public WorkoutRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
