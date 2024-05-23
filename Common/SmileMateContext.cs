using Microsoft.EntityFrameworkCore;
using SmileMate.Common.Entities;
using static System.Net.Mime.MediaTypeNames;

namespace SmileMate.Common
{
    public class SmileMateContext : DbContext
    {
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Reception> Receptions { get; set; }
        public DbSet<MedicalHistory> MedicalHistories { get; set; }

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
