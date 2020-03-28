namespace DataAccess.Repositories
{
    using DataModels;
    using NP.DataTemplates.Interfaces;
    using NP.DataTemplates.Repositories;

    internal class WorkoutIntervalTypeRepository : Repository<WorkoutIntervalType>, IRepository<WorkoutIntervalType>
    {
        public WorkoutIntervalTypeRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
