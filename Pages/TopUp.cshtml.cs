using System;
using System.Threading.Tasks;
using GestionCentreDeFormation.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GestionCentreDeFormation.Pages
{
    [Authorize]
    [IgnoreAntiforgeryToken] // For simplicity in this demo, though ideally we should use it
    public class TopUpModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public TopUpModel(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> OnPostAsync([FromForm] decimal amount)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (amount <= 0)
            {
                return BadRequest("Amount must be greater than zero.");
            }

            user.Balance += amount;
            await _userManager.UpdateAsync(user);

            return new JsonResult(new { success = true, newBalance = user.Balance.ToString("N0") });
        }
    }
}
