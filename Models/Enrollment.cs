using System.ComponentModel.DataAnnotations;

namespace GestionCentreDeFormation.Models;

public enum EnrollmentStatus
{
    Active,
    Completed,
    Waitlist
}

public class Enrollment
{
    public int Id { get; set; }

    public string StudentId { get; set; } = string.Empty;
    public ApplicationUser? Student { get; set; }

    public int CourseId { get; set; }
    public Course? Course { get; set; }

    public EnrollmentStatus Status { get; set; }
    public DateTime EnrollmentDate { get; set; } = DateTime.UtcNow;
}
