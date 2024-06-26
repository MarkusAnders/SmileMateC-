using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SmileMate.Common;
using SmileMate.Common.Entities;

namespace SmileMate
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            builder.Services.AddDbContext<SmileMateContext>();

            builder.Services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<SmileMateContext>()
                .AddDefaultUI()
                .AddDefaultTokenProviders();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapRazorPages();

            using (var scope = app.Services.CreateScope())
            {
                var provider = scope.ServiceProvider;
                var userManager = provider.GetRequiredService<UserManager<User>>();
                var roleManager = provider.GetRequiredService<RoleManager<Role>>();

                using (var context = new SmileMateContext())
                {
                    context.Database.EnsureCreated();

                    if (context.Database.GetPendingMigrations().Any())
                        context.Database.Migrate();

                    // Ensure the database is populated
                    Task.Run(() => Busket.FillDataBase(userManager, roleManager)).GetAwaiter().GetResult();
                }
            }

            app.Run();
        }
    }
}
