using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GestionCentreDeFormation.Data;
using GestionCentreDeFormation.Models;
using Microsoft.AspNetCore.Authorization;

namespace GestionCentreDeFormation.Pages.Admin.Instructors
{
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        private readonly GestionCentreDeFormation.Data.ApplicationDbContext _context;

        public EditModel(GestionCentreDeFormation.Data.ApplicationDbContext context)
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

            var instructor =  await _context.Instructors.FirstOrDefaultAsync(m => m.Id == id);
            if (instructor == null)
            {
                return NotFound();
            }
            Instructor = instructor;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Instructor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InstructorExists(Instructor.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool InstructorExists(int id)
        {
            return _context.Instructors.Any(e => e.Id == id);
        }
    }
}
