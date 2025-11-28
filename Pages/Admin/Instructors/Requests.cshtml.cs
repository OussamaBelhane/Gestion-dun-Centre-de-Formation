using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GestionCentreDeFormation.Data;
using GestionCentreDeFormation.Models;

namespace GestionCentreDeFormation.Pages.Admin.Instructors
{
    [Authorize(Roles = "Admin")]
    public class RequestsModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public RequestsModel(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public List<ApplicationUser> PendingInstructors { get; set; } = new List<ApplicationUser>();

        public async Task OnGetAsync()
        {
            // Get all users in "Instructor" role who are NOT approved
            var instructors = await _userManager.GetUsersInRoleAsync("Instructor");
            PendingInstructors = instructors.Where(u => !u.IsApproved).ToList();
        }

        public async Task<IActionResult> OnPostApproveAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            // Approve User
            user.IsApproved = true;
            await _userManager.UpdateAsync(user);

            // Create Instructor Profile
            var instructor = new Instructor
            {
                Name = $"{user.FirstName} {user.LastName}",
                Title = "New Instructor", // Default title, can be updated later
                Bio = "Bio pending update.",
                ApplicationUserId = user.Id,
                ImageUrl = "https://images.unsplash.com/photo-1535713875002-d1d0cf377fde?w=800&q=80" // Default placeholder
            };

            _context.Instructors.Add(instructor);
            await _context.SaveChangesAsync();

            TempData["Message"] = $"Instructor {user.FirstName} {user.LastName} has been approved.";
            return RedirectToPage();
        }
    }
}
