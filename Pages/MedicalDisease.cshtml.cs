using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SmileMate.Common;
using SmileMate.Common.Entities;

namespace SmileMate.Pages
{
    public class MedicalDiseaseModel : PageModel
    {
        private readonly SmileMateContext _context;

        public MedicalDiseaseModel(SmileMateContext context)
        {
            _context = context;
        }

        [BindProperty]
        public MedicalHistory MedicalHistory { get; set; }

        public async Task<IActionResult> OnGet(int? mhid)
        {
            MedicalHistory = await _context.Set<MedicalHistory>()
                .Include(mh => mh.Reception).ThenInclude(r => r.Client)
                .Include(mh => mh.Reception).ThenInclude(r => r.Doctor)
                .FirstOrDefaultAsync(mh => mh.Id == mhid);

            return Page();
        }

        public async Task<IActionResult> OnPost(int? mhid, int? patientid)
        {
            var mhToDelete = await _context.Set<MedicalHistory>().FirstOrDefaultAsync(mh => mh.Id == mhid);

            _context.Remove(mhToDelete);

            await _context.SaveChangesAsync();

            return RedirectToPage("/MedicalHistory", new { patientid = patientid });
        }
    }
}
