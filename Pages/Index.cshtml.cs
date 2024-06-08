using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SmileMate.Common;
using SmileMate.Common.Entities;

namespace SmileMate.Pages
{
    public class IndexModel : PageModel
    {
        private readonly SmileMateContext _context;

        public IndexModel(SmileMateContext context)
        {
            _context = context;
        }

        public List<User> Users { get; set; }

        public async Task<IActionResult> OnGet()
        {
            Users = await _context.Set<User>().ToListAsync();

            return Page();
        }
    }
}
