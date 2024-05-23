using Microsoft.EntityFrameworkCore;
using SmileMate.Common;

namespace SmileMate
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

            builder.Services.AddDbContext<SmileMateContext>();

            Busket.FillDb();
            
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

            app.UseAuthorization();

            app.MapRazorPages();

            using (var scope = app.Services.CreateScope())
            {
                var provider = scope.ServiceProvider;
                using (var context = new SmileMateContext())
                {
                    if (context.Database.GetPendingMigrations().Any())
                        context.Database.Migrate();
                }
            }

            app.Run();
        }
    }
}
