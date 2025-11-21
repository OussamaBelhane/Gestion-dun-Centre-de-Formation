using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionCentreDeFormation.Models;

public class Course
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Title { get; set; } = string.Empty;

    [Required]
    public string Description { get; set; } = string.Empty;

    [Range(1, 1000)]
    public int Capacity { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal Price { get; set; }

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public string? ImageUrl { get; set; }

    public string? WhatYoullLearn { get; set; }

    public int? InstructorId { get; set; }
    public Instructor? Instructor { get; set; }

    public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
}
