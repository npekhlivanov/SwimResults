namespace DataAccess
{
    using DataAccess.Models;
    using Microsoft.EntityFrameworkCore;
    using System.ComponentModel.DataAnnotations.Schema;

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Workout> Workouts { get; set; }

        public DbSet<WorkoutInterval> WorkoutIntervals { get; set; }

        public DbSet<WorkoutIntervalLength> WorkoutIntervalLengths { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WorkoutIntervalType>().HasData(
                new WorkoutIntervalType { Id = 1, Name = "Warm up"},
                new WorkoutIntervalType { Id = 2, Name = "First quick freestyle" },
                new WorkoutIntervalType { Id = 3, Name = "Second quick freestyle" },
                new WorkoutIntervalType { Id = 4, Name = "Drill with fins" },
                new WorkoutIntervalType { Id = 5, Name = "Drill (other)" },
                new WorkoutIntervalType { Id = 6, Name = "Freestyle series" },
                new WorkoutIntervalType { Id = 7, Name = "Freestyle series with pull-buoy" },
                new WorkoutIntervalType { Id = 8, Name = "Freestyle series with paddles" },
                new WorkoutIntervalType { Id = 9, Name = "Final quick freestyle" },
                new WorkoutIntervalType { Id = 10, Name = "Backstroke" },
                new WorkoutIntervalType { Id = 11, Name = "Manually added" }
               );

//            modelBuilder.Entity<Workout>().Property(w => w.Id).ValueGeneratedNever(); // HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
        }
    }
}
