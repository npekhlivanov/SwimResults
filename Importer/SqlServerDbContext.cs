namespace Importer
{
    using System;
    using DataAccess;
    using Microsoft.EntityFrameworkCore;

    public class SqlServerDbContext : ApplicationDbContext
    {
        public SqlServerDbContext(string connectionString) : base(GetOptions(connectionString))
        {
        }

        private static DbContextOptions<ApplicationDbContext> GetOptions(string connectionString)
        {
            return SqlServerDbContextOptionsExtensions.UseSqlServer(new DbContextOptionsBuilder<ApplicationDbContext>(), connectionString).Options;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuider)
        {
            if (optionsBuider == null)
            {
                throw new ArgumentNullException(nameof(optionsBuider));
            }

            optionsBuider.UseLoggerFactory(DebugLoggerFactory.GetLoggerFactory());
        }
    }
}
