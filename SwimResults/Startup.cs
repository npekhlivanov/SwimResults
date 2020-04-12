namespace SwimResults
{
    using System.Reflection;
    using AutoMapper;
    using DataAccess;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Newtonsoft.Json.Serialization;
    using SmartBreadcrumbs.Extensions;

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
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
                assembly => assembly.MigrationsAssembly(nameof(DataAccess)) //typeof(ApplicationDbContext).Assembly.FullName
            ));

            // Add AutoMapper
            //var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            //var dataAccessAssembly = Assembly.Load("DataAccess");// Assembly.GetAssembly(typeof(System.Int32));
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            // Add repositories
            services.AddRepositories();

            // Set specific name for the TempData cookie
            services.Configure<CookieTempDataProviderOptions>(options =>
            {
                options.Cookie.Name = "MyTempDataCookie";
            });

            // The IDistributedCache implementation is used as a backing store for session
            //services.AddDistributedMemoryCache();

            // Add Session
            //services.AddSession(options =>
            //{
            //    options.Cookie.Name = "MySession"; // Cookie name
            //    options.IdleTimeout = TimeSpan.FromHours(1); // 1 hours expiry
            //options.Cookie.HttpOnly = true;
            //options.Cookie.IsEssential = true;
            //});

#if (DEBUG)
            services.AddRazorPages()// configuration delegate gets called by Startup.Configure()
                .AddRazorRuntimeCompilation(); // Since Core 3.0: the package Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation must nave been added
#else
            services.AddRazorPages();

#endif

            // see https://docs.microsoft.com/en-us/aspnet/core/migration/22-to-30?view=aspnetcore-2.2&tabs=visual-studio#opt-in-to-runtime-compilation
            //services.AddMvc() // 2.x
            //    .AddMvcOptions(options => options.EnableEndpointRouting = false); // for core 3.0 compatibility
            //.SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            //.AddSessionStateTempDataProvider(); // see https://www.learnrazorpages.com/razor-pages/tempdata

            services.AddControllers() // 3.0; does not affect routing
                .AddNewtonsoftJson(options => 
                { 
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver(); 
                });


            //services.AddSingleton<ITempDataProvider, CookieTempDataProvider>(); // used by default, workos without this line
            //services.AddSession();
            services.AddBreadcrumbs(GetType().Assembly, options =>
            {
                //options.DontLookForDefaultNode = true;
                //TagName = "nav",
                //TagClasses = "",
                //OlClasses = "breadcrumb",
                //LiClasses = "breadcrumb-item",
                //ActiveLiClasses = "breadcrumb-item active",
                //SeparatorElement = "<li class=\"separator\">/</li>"
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                //app.UseDatabaseExceptionsPage(); ??
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            // https://docs.microsoft.com/en-us/aspnet/core/fundamentals/localization?view=aspnetcore-2.2#make-the-apps-content-localizable
            //app.UseRequestLocalization(options => options.DefaultRequestCulture = new RequestCulture("bg-BG"));
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            // Enforce cookie policy and display GDPR-friendly messaging - https://wakeupandcode.com/cookies-and-consent-in-asp-net-core-3-1/
            app.UseCookiePolicy();

            app.UseRouting(); // 3.0

            // Call UseSession after UseRouting and before UseEndpoints
            //app.UseSession(); or
            //app.UseHttpContextItemsMiddleware();

            //app.UseMvc(); // 2.x
            app.UseEndpoints(endpoints => // 3.0
            {
                endpoints.MapControllers(); // must be present for api controller action routing; adds support for attribute-routed controllers 
                endpoints.MapRazorPages();
                //endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}"); // adds a conventional route for controllers
            });
        }
    }
}
