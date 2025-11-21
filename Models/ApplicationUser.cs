using Microsoft.AspNetCore.Identity;

namespace GestionCentreDeFormation.Models;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    
    public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
}
