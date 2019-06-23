namespace DataAccess
{
    using DataAccess.Models;
    using Microsoft.EntityFrameworkCore;
using System;

    public class DataContext : DbContext
    {
        public DbSet<Workout> Workouts { get; set; }

        public DbSet<WorkoutInterval> WorkoutIntervals { get; set; }

        public DbSet<WorkoutIntervalLength> WorkoutIntervalLengths { get; set; }
    }
}
