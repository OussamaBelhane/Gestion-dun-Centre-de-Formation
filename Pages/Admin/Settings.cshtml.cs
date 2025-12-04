using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace GestionCentreDeFormation.Pages.Admin
{
    [Authorize(Roles = "Admin")]
    public class SettingsModel : PageModel
    {
        [BindProperty]
        [Required(ErrorMessage = "Platform Name is required.")]
        public string PlatformName { get; set; } = "Gestion Centre de Formation";

        [BindProperty]
        [Required(ErrorMessage = "Support Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public string SupportEmail { get; set; } = "support@gcf.com";

        [BindProperty]
        public bool EnrollmentsOpen { get; set; } = true;

        public string Message { get; set; } = string.Empty;

        public void OnGet()
        {
        }

        public void OnPost()
        {
            if (!ModelState.IsValid)
            {
                return;
            }

            // Simulate saving settings
            Message = "Settings saved successfully!";
        }
    }
}
