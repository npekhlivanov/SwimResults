namespace DataAccess
{
    using DataAccess.Repositories;
    using DataModels;
    using Microsoft.Extensions.DependencyInjection;
    using NP.DataTemplates.Interfaces;

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

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services
                .AddTransient<IRepository<Workout>, WorkoutRepository>()
                .AddTransient<IRepository<WorkoutInterval>, WorkoutIntervalRepository>()
                .AddTransient<IReadOnlyRepository<WorkoutIntervalLength>, WorkoutIntervalLengthRepository>()
                .AddTransient<IRepository<WorkoutIntervalType>, WorkoutIntervalTypeRepository>();

            return services;
        }
    }
}

