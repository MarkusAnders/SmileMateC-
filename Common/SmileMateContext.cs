using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SmileMate.Common.Entities;

namespace SmileMate.Common
{
    public class SmileMateContext : IdentityDbContext<User, Role, long>
    {
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Reception> Receptions { get; set; }
        public DbSet<MedicalHistory> MedicalHistories { get; set; }

        public SmileMateContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Database=CoursePlatform;Username=postgres;Password=admin");
        }
    }
}
