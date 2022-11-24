using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Production_Planning_System.Areas.Identity.Data;
using Production_Planning_System.Data;
namespace Production_Planning_System
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
                        var connectionString = builder.Configuration.GetConnectionString("Production_Planning_SystemContextConnection") ?? throw new InvalidOperationException("Connection string 'Production_Planning_SystemContextConnection' not found.");

                                    builder.Services.AddDbContext<Production_Planning_SystemContext>(options =>
                options.UseSqlServer(connectionString));

                                                builder.Services.AddDefaultIdentity<Production_Planning_SystemUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<Production_Planning_SystemContext>();

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
                        app.UseAuthentication();;

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();
            app.Run();
        }
    }
}