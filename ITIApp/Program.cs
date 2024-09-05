using Managers;
using Microsoft.EntityFrameworkCore;
using Models;

namespace ITIApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<ProjectContext>(i =>
            {
                i.UseLazyLoadingProxies()
                .UseSqlServer(builder.Configuration.GetConnectionString("projectDb"));
            }
          );

            builder.Services.AddScoped<BookManager>();
            builder.Services.AddScoped<PuplisherManager>();
            builder.Services.AddScoped<SubjectManager>();




            var app = builder.Build();

            app.UseStaticFiles();//wwwroot

            app.UseRouting();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=home}/{action=index}/{id?}");

            app.Run();

        }
    }
}