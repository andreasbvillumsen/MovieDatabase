using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MovieDatabase.Data;
using Microsoft.EntityFrameworkCore;

namespace MovieDatabase
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            CurrentEnvironment = env;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment CurrentEnvironment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            String DBString;

            if (CurrentEnvironment.IsProduction())
            {
                DBString = "Data Source=database,1434;Initial Catalog=MovieDatabase;User ID=sa;Password=HelloW0rld";
            }
            else
            {
                DBString = "Data Source=database,1433;Initial Catalog=MovieDatabase;User ID=sa;Password=HelloW0rld";
            }

            services.AddControllersWithViews();

            services.AddDbContext<MovieContext>(options =>
                options.UseSqlServer("Data Source=database,1433;Initial Catalog=MovieDatabase;User ID=sa;Password=HelloW0rld")
            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

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
