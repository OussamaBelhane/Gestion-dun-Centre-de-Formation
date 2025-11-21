using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GestionCentreDeFormation.Pages.Dashboard
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
