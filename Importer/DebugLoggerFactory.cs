namespace Importer
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    public class DebugLoggerFactory
    {
        public static ILoggerFactory GetLoggerFactory()
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddLogging(builder =>
                   builder.AddDebug()
                          .AddFilter(DbLoggerCategory.Database.Command.Name, LogLevel.Information)
                          );
            return serviceCollection.BuildServiceProvider()
                    .GetService<ILoggerFactory>();
        }

        //public static readonly LoggerFactory Instance =
        //    new LoggerFactory(new[] {
        //        new DebugLoggerProvider((category, level) => category == DbLoggerCategory.Database.Command.Name && level == LogLevel.Information) }
        //        //},
        //        //GetOptions()
        //        );

        //private static LoggerFilterOptions GetOptions()
        //{
        //    var options = new LoggerFilterOptions();
        //    var rule = new LoggerFilterRule("Microsoft.EntityFrameworkCore.SqlServer", string.Empty, LogLevel.Information, (provider, category, level) => 
        //        category == DbLoggerCategory.Database.Command.Name && level == LogLevel.Information && provider == "Microsoft.EntityFrameworkCore.Database");
        //    options.Rules.Add(rule);
        //    return options;
        //}
    }
}
