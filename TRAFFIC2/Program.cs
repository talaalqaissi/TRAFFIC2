using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TRAFFIC2.Models;

namespace TRAFFIC2
{

    public class Program
    {
        
        public static void Main(string[] args)
        {
            
            var builder = WebApplication.CreateBuilder(args);
            
            builder.Services.AddDbContext<ModelContext>(options =>
            options.UseOracle(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddSession(options => { options.IdleTimeout = TimeSpan.FromMinutes(60); });

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();
			app.UseSession();

			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllerRoute(
            //        name: "monthlyReport",
            //        pattern: "report/monthly/{year}/{month}",
            //        defaults: new { controller = "Report", action = "MonthlyReport" }
            //    );

            //    endpoints.MapControllerRoute(
            //        name: "annualReport",
            //        pattern: "report/annual/{year}",
            //        defaults: new { controller = "Report", action = "AnnualReport" }
            //    );

            //    // Other routes...
            //});
          
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
          
            app.Run();
        }
       
       

    }
}