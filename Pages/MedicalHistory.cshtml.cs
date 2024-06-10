using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SmileMate.Common;
using SmileMate.Common.Entities;

namespace SmileMate.Pages
{
    public class MedicalHistoryModel : PageModel
    {
        private readonly SmileMateContext _context;

        public MedicalHistoryModel(SmileMateContext context)
        {
            _context = context;
        }

        [BindProperty]
        public List<MedicalHistory> MedicalHystory { get; set; } = new();
        [BindProperty]
        public Patient Patient { get; set; }

        public async Task<IActionResult> OnGet(int? patientid)
        {
            Patient = await _context.Set<Patient>().FirstOrDefaultAsync(p => p.Id == patientid);

            MedicalHystory = await _context.Set<MedicalHistory>()
                .Include(mh => mh.Reception).ThenInclude(r => r.Client)
                .Include(mh => mh.Reception).ThenInclude(r => r.Doctor)
                .Where(mh => mh.Reception.Client.Id == patientid).ToListAsync();


            return Page();
        }
    }
}
