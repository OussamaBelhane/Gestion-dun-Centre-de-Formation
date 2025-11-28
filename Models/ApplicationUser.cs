using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionCentreDeFormation.Models;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;

    [Column(TypeName = "decimal(18, 2)")]
    public decimal Balance { get; set; }

    public bool IsApproved { get; set; } = true; // Default to true for Students
    
    public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
}
