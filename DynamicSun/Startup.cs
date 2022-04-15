using DynamicSun.Data;
using DynamicSun.Data.Domain;
using DynamicSun.Data.Models.EntityFramework;
using DynamicSun.Data.Models.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DynamicSun
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            Configuration.Bind("Project", new Config());
            services.AddTransient<IWeatherEntityRepository, EFWeatherEntityRepository>();
            services.AddDbContext<AppDbContext>(x => x.UseSqlServer(Config.ConnectionString));
            services.AddRazorPages();
            services.AddMvc();
            services.AddTransient<DataManager>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePages();
            }

            app.UseStaticFiles();
            app.UseRouting();

            app.UseHttpsRedirection();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
