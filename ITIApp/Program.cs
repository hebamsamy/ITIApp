using Managers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models;

namespace ITIApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);



            #region Configration and DI
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<ProjectContext>(i =>
            {
                i.UseLazyLoadingProxies()
                .UseSqlServer(builder.Configuration.GetConnectionString("projectDb"));
            });

            builder.Services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<ProjectContext>();
            builder.Services.Configure<IdentityOptions>(i =>
            {
                
                i.User.RequireUniqueEmail = true;
                i.Password.RequireUppercase = false;
                i.Password.RequireLowercase = false;
                i.Password.RequireNonAlphanumeric = false;

                i.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);
                i.Lockout.MaxFailedAccessAttempts = 2;
            });
            builder.Services.AddScoped<AccountManager>();
            builder.Services.AddScoped<BookManager>();
            builder.Services.AddScoped<PuplisherManager>();
            builder.Services.AddScoped<SubjectManager>(); 
            builder.Services.AddScoped<RoleManager>();
            #endregion


            var app = builder.Build();



            #region Apply MiddelWare
            app.UseStaticFiles();//wwwroot

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=home}/{action=index}/{id?}"); 
            #endregion

            app.Run();

        }
    }
}