using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GestionCentreDeFormation.Data;
using GestionCentreDeFormation.Models;

namespace GestionCentreDeFormation.Pages.Courses;

public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public IndexModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public IList<Course> Course { get;set; } = default!;

    public async Task OnGetAsync()
    {
        if (_context.Courses != null)
        {
            Course = await _context.Courses.ToListAsync();
        }
        
        // Seed data for demonstration if empty
        if (!Course.Any())
        {
            Course = new List<Course>
            {
                new Course 
                { 
                    Title = "FULL STACK .NET", 
                    Description = "Master C#, EF Core, and Azure architecture. Build scalable enterprise applications from scratch.", 
                    Price = 1200, 
                    Capacity = 20, 
                    StartDate = DateTime.Now.AddDays(10), 
                    EndDate = DateTime.Now.AddDays(40) 
                },
                new Course 
                { 
                    Title = "REACT & NEXT.JS", 
                    Description = "Build modern, high-performance web applications with the latest React features and Next.js framework.", 
                    Price = 950, 
                    Capacity = 15, 
                    StartDate = DateTime.Now.AddDays(20), 
                    EndDate = DateTime.Now.AddDays(50) 
                },
                new Course 
                { 
                    Title = "UX/UI MASTERCLASS", 
                    Description = "Learn Figma, user research, and prototyping. Design interfaces that users love.", 
                    Price = 1500, 
                    Capacity = 12, 
                    StartDate = DateTime.Now.AddDays(30), 
                    EndDate = DateTime.Now.AddDays(60) 
                },
                new Course 
                { 
                    Title = "DEVOPS ENGINEERING", 
                    Description = "Master Docker, Kubernetes, and CI/CD pipelines. Automate deployment like a pro.", 
                    Price = 1800, 
                    Capacity = 10, 
                    StartDate = DateTime.Now.AddDays(15), 
                    EndDate = DateTime.Now.AddDays(45) 
                },
                new Course 
                { 
                    Title = "DATA SCIENCE PRO", 
                    Description = "Python, Machine Learning, and Big Data analytics. Turn data into actionable insights.", 
                    Price = 1600, 
                    Capacity = 18, 
                    StartDate = DateTime.Now.AddDays(25), 
                    EndDate = DateTime.Now.AddDays(55) 
                },
                new Course 
                { 
                    Title = "CYBERSECURITY OPS", 
                    Description = "Learn ethical hacking, network security, and threat analysis. Protect systems from attacks.", 
                    Price = 2000, 
                    Capacity = 8, 
                    StartDate = DateTime.Now.AddDays(35), 
                    EndDate = DateTime.Now.AddDays(65) 
                },
                new Course 
                { 
                    Title = "CLOUD ARCHITECT", 
                    Description = "Design and deploy scalable, highly available, and fault-tolerant systems on AWS and Azure.", 
                    Price = 2200, 
                    Capacity = 10, 
                    StartDate = DateTime.Now.AddDays(40), 
                    EndDate = DateTime.Now.AddDays(70) 
                },
                new Course 
                { 
                    Title = "MOBILE DEV FLUTTER", 
                    Description = "Build beautiful, natively compiled applications for mobile, web, and desktop from a single codebase.", 
                    Price = 1100, 
                    Capacity = 20, 
                    StartDate = DateTime.Now.AddDays(12), 
                    EndDate = DateTime.Now.AddDays(42) 
                }
            };
        }
    }
}
