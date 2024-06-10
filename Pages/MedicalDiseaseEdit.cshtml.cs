using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SmileMate.Common;
using SmileMate.Common.Entities;

namespace SmileMate.Pages
{
    public class MedicalDiseaseEditModel : PageModel
    {
        private readonly SmileMateContext _context;

        public MedicalDiseaseEditModel(SmileMateContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Reception Reception { get; set; }

        public async Task<IActionResult> OnGet(int? receptionid)
        {
            Reception = await _context.Set<Reception>()
                .Include(r => r.Client)
                .Include(r => r.MedicalHistory)
                .FirstOrDefaultAsync(r => r.Id == receptionid);

            return Page();
        }


    }
}
