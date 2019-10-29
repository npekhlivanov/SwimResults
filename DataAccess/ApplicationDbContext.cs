namespace DataAccess
{
    using System;
    using DataModels;
    using Microsoft.EntityFrameworkCore;

    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Workout> Workouts { get; set; }

        public DbSet<WorkoutInterval> WorkoutIntervals { get; set; }

        public DbSet<WorkoutIntervalLength> WorkoutIntervalLengths { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (modelBuilder == null)
            {
                throw new ArgumentNullException(nameof(modelBuilder));
            }

            modelBuilder.Entity<Workout>()
                .Property(p => p.Id)
                .ValueGeneratedNever();

            modelBuilder.Entity<Workout>()
                .Property(p => p.WorkoutDate)
                .HasColumnName("Date");

            modelBuilder.Entity<WorkoutInterval>()
                .Property(p => p.Duration)
                .HasComputedColumnSql("dbo.fnGetIntervalDuration(Id)");

            modelBuilder.Entity<WorkoutInterval>()
                .Property(p => p.Distance)
                .HasComputedColumnSql("dbo.fnGetIntervalDistance(Id)");

            modelBuilder.Entity<WorkoutInterval>()
                .Property(p => p.StrokeTypeId)
                .HasComputedColumnSql("dbo.fnGetIntervalStrokeType(Id)");

            modelBuilder.Entity<WorkoutInterval>()
                .Property(p => p.Pace)
                .HasComputedColumnSql("case when dbo.fnGetIntervalDistance(Id)=0 then null else " +
                    "dbo.fnGetIntervalDuration(Id) * 100 / dbo.fnGetIntervalDistance(Id) end");

            modelBuilder.Entity<WorkoutInterval>()
                .Property(p => p.Swolf)
                .HasComputedColumnSql("case when dbo.fnGetIntervalDistance(Id)=0 then null else " +
                    "dbo.fnGetIntervalDuration(Id) * 50 / dbo.fnGetIntervalDistance(Id) + StrokeCount * 2 end");

            modelBuilder.Entity<WorkoutIntervalType>().HasData(
                new WorkoutIntervalType { Id = 1, Name = "Warm up" },
                new WorkoutIntervalType { Id = 2, Name = "First quick freestyle" },
                new WorkoutIntervalType { Id = 3, Name = "Second quick freestyle" },
                new WorkoutIntervalType { Id = 4, Name = "Drill with fins" },
                new WorkoutIntervalType { Id = 5, Name = "Drill (other)" },
                new WorkoutIntervalType { Id = 6, Name = "Freestyle series" },
                new WorkoutIntervalType { Id = 7, Name = "Freestyle series with pull-buoy" },
                new WorkoutIntervalType { Id = 8, Name = "Freestyle series with paddles" },
                new WorkoutIntervalType { Id = 9, Name = "Final quick freestyle" },
                new WorkoutIntervalType { Id = 10, Name = "Backstroke" },
                new WorkoutIntervalType { Id = 11, Name = "Manually added" },
                new WorkoutIntervalType { Id = 12, Name = "Pre warm-up" },
                new WorkoutIntervalType { Id = 13, Name = "Intermediate quick freestyle" },
                new WorkoutIntervalType { Id = 14, Name = "Final quick freestyle 2" },
                new WorkoutIntervalType { Id = 15, Name = "Other freestyle" }
             );
        }
    }
}
