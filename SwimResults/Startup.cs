namespace SwimResults
{
    using AutoMapper;
    using DataAccess;
    using DataAccess.Data;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Reflection;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")) 
            );

            // Add AutoMapper
            //var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            //var dataAccessAssembly = Assembly.Load("DataAccess");// Assembly.GetAssembly(typeof(System.Int32));
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            // Add repositories
            services.AddTransient(typeof(WorkoutRepository));
            services.AddTransient(typeof(WorkoutIntervalRepository));
            services.AddTransient(typeof(WorkoutIntervalLengthRepository));
            services.AddTransient(typeof(WorkoutIntervalTypeRepository));

            // Set specific name for the TempData cookie
            services.Configure<CookieTempDataProviderOptions>(options =>
            {
                options.Cookie.Name = "MyTempDataCookie";
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
                //.AddSessionStateTempDataProvider(); // see https://www.learnrazorpages.com/razor-pages/tempdata

            //services.AddSingleton<ITempDataProvider, CookieTempDataProvider>(); // used by default, workos without this line
            //services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            //app.UseSession();

            app.UseMvc();
        }
    }
}
