namespace DataAccess.Data
{
    using DataAccess.Models;
    using DataTemplates.Interfaces;
    using DataTemplates.Repositories;

    public class WorkoutIntervalTypeRepository : Repository<WorkoutIntervalType>, IRepository<WorkoutIntervalType>
    {
        public WorkoutIntervalTypeRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
