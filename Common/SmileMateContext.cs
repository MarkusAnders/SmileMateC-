using Microsoft.EntityFrameworkCore;

namespace SmileMate.Common
{
    public class SmileMateContext : DbContext
    {
        public SmileMateContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Database=CoursePlatform;Username=postgres;Password=admin");
        }
    }
}
