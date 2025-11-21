using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GestionCentreDeFormation.Data;
using GestionCentreDeFormation.Models;
using Microsoft.AspNetCore.Authorization;

namespace GestionCentreDeFormation.Pages.Admin.Instructors
{
    [Authorize(Roles = "Admin")]
    public class DeleteModel : PageModel
    {
        private readonly GestionCentreDeFormation.Data.ApplicationDbContext _context;

        public DeleteModel(GestionCentreDeFormation.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Instructor Instructor { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instructor = await _context.Instructors.FirstOrDefaultAsync(m => m.Id == id);

            if (instructor == null)
            {
                return NotFound();
            }
            else
            {
                Instructor = instructor;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instructor = await _context.Instructors.FindAsync(id);
            if (instructor != null)
            {
                Instructor = instructor;
                _context.Instructors.Remove(Instructor);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
