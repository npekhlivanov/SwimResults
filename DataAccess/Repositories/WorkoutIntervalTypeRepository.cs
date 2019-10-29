namespace DataAccess.Repositories
{
    using DataModels;
    using DataTemplates.Interfaces;
    using DataTemplates.Repositories;

    internal class WorkoutIntervalTypeRepository : Repository<WorkoutIntervalType>, IRepository<WorkoutIntervalType>
    {
        public WorkoutIntervalTypeRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
