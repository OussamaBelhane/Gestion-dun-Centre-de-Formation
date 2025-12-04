using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GestionCentreDeFormation.Data;
using GestionCentreDeFormation.Models;
using System.Linq;

namespace GestionCentreDeFormation.Pages.Admin
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public decimal TotalRevenue { get; set; }
        public int ActiveStudents { get; set; }
        public int TotalEnrollments { get; set; }
        public List<Enrollment> RecentEnrollments { get; set; } = new List<Enrollment>();
        
        // Chart Data
        public string ChartLabels { get; set; }
        public string ChartData { get; set; }

        public void OnGet()
        {
            // 1. Total Revenue: Sum of Price of all enrolled courses
            // Note: In a real app, you might want to store the price at the time of enrollment in the Enrollment table
            // to handle price changes. For now, we use the current course price.
            TotalRevenue = _context.Enrollments
                .Include(e => e.Course)
                .Sum(e => e.Course.Price);

            // 2. Active Students: Count of unique students with at least one enrollment
            ActiveStudents = _context.Enrollments
                .Select(e => e.StudentId)
                .Distinct()
                .Count();

            // 3. Recent Activity (Last 5 enrollments)
            RecentEnrollments = _context.Enrollments
                .Include(e => e.Course)
                .Include(e => e.Student)
                .OrderByDescending(e => e.EnrollmentDate)
                .Take(5)
                .ToList();

            // 4. Chart Data (Enrollments per month for the last 6 months)
            var sixMonthsAgo = DateTime.Now.AddMonths(-5);
            var enrollmentData = _context.Enrollments
                .Where(e => e.EnrollmentDate >= sixMonthsAgo)
                .AsEnumerable() // Switch to client-side evaluation for grouping by month if SQLite has issues
                .GroupBy(e => new { e.EnrollmentDate.Year, e.EnrollmentDate.Month })
                .OrderBy(g => g.Key.Year).ThenBy(g => g.Key.Month)
                .Select(g => new 
                { 
                    Month = new DateTime(g.Key.Year, g.Key.Month, 1).ToString("MMM"), 
                    Count = g.Count() 
                })
                .ToList();

            // Fill in missing months if needed, or just send what we have
            ChartLabels = string.Join(",", enrollmentData.Select(d => $"'{d.Month}'"));
            ChartData = string.Join(",", enrollmentData.Select(d => d.Count));
        }
    }
}
