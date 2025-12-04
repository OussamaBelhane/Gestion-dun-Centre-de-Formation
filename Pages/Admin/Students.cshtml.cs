using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using GestionCentreDeFormation.Data;
using GestionCentreDeFormation.Models;

namespace GestionCentreDeFormation.Pages.Admin
{
    [Authorize(Roles = "Admin")]
    public class StudentsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public StudentsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; } = string.Empty;

        public List<StudentViewModel> Students { get; set; } = new List<StudentViewModel>();

        public async Task OnGetAsync()
        {
            // Fetch users who have at least one enrollment
            // Note: In a real app, you might want to fetch all users with "Student" role, 
            // but here we'll fetch users with enrollments as requested.
            var query = _context.Users
                .Include(u => u.Enrollments)
                .Where(u => u.Enrollments.Any()); // Only enrolled students

            if (!string.IsNullOrEmpty(SearchTerm))
            {
                query = query.Where(u => u.Email.Contains(SearchTerm) || 
                                         (u.FirstName != null && u.FirstName.Contains(SearchTerm)) || 
                                         (u.LastName != null && u.LastName.Contains(SearchTerm)));
            }

            var users = await query.ToListAsync();

            Students = users.Select(u => new StudentViewModel
            {
                Id = u.Id,
                // Logic: Use Name if available, otherwise Email prefix
                Name = !string.IsNullOrEmpty(u.FirstName) || !string.IsNullOrEmpty(u.LastName) 
                    ? $"{u.FirstName} {u.LastName}".Trim() 
                    : u.Email.Split('@')[0],
                Email = u.Email,
                EnrolledCoursesCount = u.Enrollments.Count,
                Status = "Active" // Default status for now
            }).ToList();
        }

        public class StudentViewModel
        {
            public string Id { get; set; } // Changed to string to match Identity User ID
            public string Name { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
            public int EnrolledCoursesCount { get; set; }
            public string Status { get; set; } = "Active";
        }
    }
}
