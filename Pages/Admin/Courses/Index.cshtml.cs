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

namespace GestionCentreDeFormation.Pages.Admin.Courses
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly GestionCentreDeFormation.Data.ApplicationDbContext _context;

        public IndexModel(GestionCentreDeFormation.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Course> Course { get;set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string? SearchTerm { get; set; }

        public async Task OnGetAsync()
        {
            var query = _context.Courses.AsQueryable();

            if (!string.IsNullOrEmpty(SearchTerm))
            {
                query = query.Where(c => c.Title.Contains(SearchTerm));
            }

            Course = await query.ToListAsync();
        }
    }
}
