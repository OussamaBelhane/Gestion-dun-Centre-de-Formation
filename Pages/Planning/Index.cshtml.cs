using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GestionCentreDeFormation.Data;
using GestionCentreDeFormation.Models;

namespace GestionCentreDeFormation.Pages.Planning
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public IndexModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public List<Course> ScheduledCourses { get; set; } = new List<Course>();
        public bool IsInstructor { get; set; }

        public async Task OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return;

            IsInstructor = await _userManager.IsInRoleAsync(user, "Instructor");

            if (IsInstructor)
            {
                // Get courses taught by this instructor that haven't ended
                ScheduledCourses = await _context.Courses
                    .Include(c => c.Instructor)
                    .Where(c => c.Instructor.ApplicationUserId == user.Id && c.EndDate >= DateTime.Now)
                    .OrderBy(c => c.StartDate)
                    .ToListAsync();
            }
            else
            {
                // Get courses the student is enrolled in that haven't ended
                ScheduledCourses = await _context.Enrollments
                    .Include(e => e.Course)
                    .ThenInclude(c => c.Instructor)
                    .Where(e => e.StudentId == user.Id && e.Course.EndDate >= DateTime.Now)
                    .Select(e => e.Course)
                    .OrderBy(c => c.StartDate)
                    .ToListAsync();
            }
        }
    }
}
