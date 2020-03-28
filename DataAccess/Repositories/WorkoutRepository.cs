namespace DataAccess.Repositories
{
    using DataModels;
    using NP.DataTemplates.Interfaces;
    using NP.DataTemplates.Repositories;

    internal class WorkoutRepository : Repository<Workout>, IRepository<Workout>
    {
        public WorkoutRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
