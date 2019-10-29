namespace Importer
{
    using DataAccess;
    using Microsoft.EntityFrameworkCore;

    public class SqlServerDbContext : ApplicationDbContext
    {
        public SqlServerDbContext(string connectionString) : base(GetOptions(connectionString))
        {
        }

        private static DbContextOptions GetOptions(string connectionString)
        {
            return SqlServerDbContextOptionsExtensions.UseSqlServer(new DbContextOptionsBuilder(), connectionString).Options;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuider)
        {
            optionsBuider.UseLoggerFactory(DebugLoggerFactory.GetLoggerFactory());
        }
    }
}
