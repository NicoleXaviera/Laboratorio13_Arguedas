using Microsoft.EntityFrameworkCore;

namespace Laboratorio13_Arguedas.Models
{
    public class SchoolContext : DbContext
    {
        public SchoolContext(DbContextOptions<SchoolContext> options)
            : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Student>()
                .Property(s => s.Activo)
                .HasDefaultValue(true);

            modelBuilder.Entity<Grade>()
                .Property(s => s.Activo)
                .HasDefaultValue(true);

            modelBuilder.Entity<Course>()
                .Property(s => s.Activo)
                .HasDefaultValue(true);
        }
    }
}