using Microsoft.EntityFrameworkCore;
using Csharp.Data;

namespace _10_ASP_NET_Core
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<MujDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("MujConnectionString")));
            builder.Services.AddSession(options => {
                options.Cookie.Name = ".MojeAplikace";
                options.Cookie.IsEssential = true;
                options.IdleTimeout = TimeSpan.FromMinutes(30);
            });
            var app = builder.Build();

            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.UseSession();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}