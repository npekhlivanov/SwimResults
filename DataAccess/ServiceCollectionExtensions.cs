namespace DataAccess
{
    using DataAccess.Repositories;
    using DataModels;
    using DataTemplates.Interfaces;
    using Microsoft.Extensions.DependencyInjection;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddWorkoutRepository(this IServiceCollection services)
        {
            return services.AddTransient<IRepository<Workout>, WorkoutRepository>();
        }

        public static IServiceCollection AddWorkoutIntervalRepository(this IServiceCollection services)
        {
            return services.AddTransient<IRepository<WorkoutInterval>, WorkoutIntervalRepository>();
        }

        public static IServiceCollection AddWorkoutIntervalLengthRepository(this IServiceCollection services)
        {
            return services.AddTransient<IReadOnlyRepository<WorkoutIntervalLength>, WorkoutIntervalLengthRepository>();
        }

        public static IServiceCollection AddWorkoutIntervalTypeRepository(this IServiceCollection services)
        {
            return services.AddTransient<IRepository<WorkoutIntervalType>, WorkoutIntervalTypeRepository>();
        }
    }
}

