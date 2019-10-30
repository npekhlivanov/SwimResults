namespace SwimResults
{
    //using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Hosting;

    public static class Program
    {
        public static void Main(string[] args)
        {
            //CreateWebHostBuilder(args).Build().Run(); // 2.x
            CreateHostBuilder(args).Build().Run(); // 3.0
        }

        //public static IWebHostBuilder CreateWebHostBuilder(string[] args) => // 2.x
        //WebHost.CreateDefaultBuilder(args)
        //    .UseStartup<Startup>();

        public static IHostBuilder CreateHostBuilder(string[] args) => // 3.0
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
