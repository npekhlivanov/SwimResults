namespace DataAccess
{
    using DataAccess.Repositories;
    using DataModels;
    using NP.DataTemplates.Interfaces;

    public static class RepositoryFactory
    {
        public static IRepository<Workout> CreateWorkoutRepository(ApplicationDbContext dbContext)
        {
            return new WorkoutRepository(dbContext);
        }

        public static IRepository<WorkoutInterval> CreateWorkoutIntervalRepository(ApplicationDbContext dbContext)
        {
            return new WorkoutIntervalRepository(dbContext);
        }

        public static IReadOnlyRepository<WorkoutIntervalLength> CreateWorkoutIntervalLengthRepository(ApplicationDbContext dbContext)
        {
            return new WorkoutIntervalLengthRepository(dbContext);
        }

        public static IRepository<WorkoutIntervalType> CreateWorkoutIntervalTypeRepository(ApplicationDbContext dbContext)
        {
            return new WorkoutIntervalTypeRepository(dbContext);
        }
    }
}
