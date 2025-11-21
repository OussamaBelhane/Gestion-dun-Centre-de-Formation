using System.ComponentModel.DataAnnotations;

namespace GestionCentreDeFormation.Models;

public class Instructor
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string Title { get; set; } = string.Empty;

    [Required]
    public string Bio { get; set; } = string.Empty;

    public string? ImageUrl { get; set; }

    public ICollection<Course> Courses { get; set; } = new List<Course>();
}
