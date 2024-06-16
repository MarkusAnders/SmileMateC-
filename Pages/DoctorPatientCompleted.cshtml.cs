using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SmileMate.Common;
using SmileMate.Common.Entities;

namespace SmileMate.Pages
{
    public class DoctorPatientCompleted : PageModel
    {
        private readonly SmileMateContext _context;
        private readonly UserManager<User> _userManager;

        public DoctorPatientCompleted(SmileMateContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public List<Reception> Receptions { get; set; }

        public async Task<IActionResult> OnGet()
        {
            var user = await _userManager.GetUserAsync(User);

            var receptions = await _context.Set<Reception>()
                .Include(r => r.Doctor)
                .Include(r => r.Client)
                .Where(r => r.Doctor.Id == user.Id)
                .ToListAsync();

            Receptions = receptions;

            return Page();
        }
    }
}
