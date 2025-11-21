using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using GestionCentreDeFormation.Models;

namespace GestionCentreDeFormation.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Course> Courses { get; set; }
    public DbSet<Enrollment> Enrollments { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Configure Many-to-Many relationship explicitly if needed, 
        // but EF Core convention handles it well. 
        // Here we just ensure the foreign keys are set up correctly.

        builder.Entity<Enrollment>()
            .HasOne(e => e.Student)
            .WithMany(u => u.Enrollments)
            .HasForeignKey(e => e.StudentId);

        builder.Entity<Enrollment>()
            .HasOne(e => e.Course)
            .WithMany(c => c.Enrollments)
            .HasForeignKey(e => e.CourseId);
    }
}
