using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;

namespace GestionCentreDeFormation.Pages.Admin
{
    [Authorize(Roles = "Admin")]
    public class StudentsModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; } = string.Empty;

        public List<StudentViewModel> Students { get; set; } = new List<StudentViewModel>();

        public void OnGet()
        {
            // Generate Mock Data
            var mockStudents = new List<StudentViewModel>
            {
                new StudentViewModel { Id = 1, Name = "Alice Johnson", Email = "alice.j@example.com", EnrolledCoursesCount = 3, Status = "Active" },
                new StudentViewModel { Id = 2, Name = "Bob Smith", Email = "bob.smith@test.com", EnrolledCoursesCount = 1, Status = "Pending" },
                new StudentViewModel { Id = 3, Name = "Charlie Brown", Email = "charlie.b@domain.org", EnrolledCoursesCount = 5, Status = "Active" },
                new StudentViewModel { Id = 4, Name = "Diana Prince", Email = "diana.p@themyscira.net", EnrolledCoursesCount = 2, Status = "Active" },
                new StudentViewModel { Id = 5, Name = "Evan Wright", Email = "evan.w@example.com", EnrolledCoursesCount = 0, Status = "Inactive" },
                new StudentViewModel { Id = 6, Name = "Fiona Gallagher", Email = "fiona.g@chicago.com", EnrolledCoursesCount = 4, Status = "Active" },
                new StudentViewModel { Id = 7, Name = "George Martin", Email = "george.m@westeros.org", EnrolledCoursesCount = 1, Status = "Pending" },
                new StudentViewModel { Id = 8, Name = "Hannah Baker", Email = "hannah.b@liberty.edu", EnrolledCoursesCount = 2, Status = "Active" },
                new StudentViewModel { Id = 9, Name = "Ian Malcolm", Email = "ian.m@chaos.math", EnrolledCoursesCount = 6, Status = "Active" },
                new StudentViewModel { Id = 10, Name = "Jane Doe", Email = "jane.doe@unknown.com", EnrolledCoursesCount = 0, Status = "Inactive" }
            };

            if (!string.IsNullOrEmpty(SearchTerm))
            {
                Students = mockStudents
                    .Where(s => s.Name.Contains(SearchTerm, System.StringComparison.OrdinalIgnoreCase) || 
                                s.Email.Contains(SearchTerm, System.StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }
            else
            {
                Students = mockStudents;
            }
        }

        public class StudentViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
            public int EnrolledCoursesCount { get; set; }
            public string Status { get; set; } = "Active";
        }
    }
}
