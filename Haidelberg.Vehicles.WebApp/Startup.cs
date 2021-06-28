using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Haidelberg.Vehicles.DataAccess.EF;
using Haidelberg.Vehicles.DataLayer;
using Haidelberg.Vehicles.BusinessLayer;
using Haidelberg.Vehicles.BusinessLayer.Abstractions;
using Microsoft.AspNetCore.Identity;
using System;
using NLog.Extensions.Logging;

namespace Haidelberg.Vehicles.WebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureLogging();
            // Register Services
            services.AddSingleton<IService, Service>();
            //services.AddTransient<IService, Service>();
            //services.AddScoped<IService, Service>();
            services.AddTransient<ICategoriesService, CategoriesService>();
            //services.AddTransient<ICategoriesService, SqlCategoriesService>();
            //services.AddTransient<ICategoriesService, MongoCategoriesService>();
            services.AddTransient<VehiclesService>();
            services.AddTransient<IRolesService, RolesService>();
            
            // Register Repositories
            services.AddTransient<CategoryRepository>();
            services.AddTransient<VehicleRepository>();

            // Register DbContext
            services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(Configuration["ConnectionString"]));
                services.AddIdentity<User,IdentityRole<string>>()
                .AddEntityFrameworkStores<DatabaseContext>()
                .AddDefaultTokenProviders();
            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 1;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 100;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = false;
            });

            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(60);

                options.LoginPath = "/identity/login";
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                options.SlidingExpiration = true;
            });

            services.AddControllersWithViews();
        }

        private void ConfigureLogging()
        {
            NLog.LogManager.Configuration = new NLogLoggingConfiguration(Configuration.GetSection("NLog"));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseRouting();            
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
