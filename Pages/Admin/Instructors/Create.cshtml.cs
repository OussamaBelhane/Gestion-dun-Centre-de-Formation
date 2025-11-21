using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using GestionCentreDeFormation.Data;
using GestionCentreDeFormation.Models;
using Microsoft.AspNetCore.Authorization;

namespace GestionCentreDeFormation.Pages.Admin.Instructors
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
        private readonly GestionCentreDeFormation.Data.ApplicationDbContext _context;

        public CreateModel(GestionCentreDeFormation.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Instructor Instructor { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Instructors.Add(Instructor);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
