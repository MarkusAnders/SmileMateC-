using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SmileMate.Common;
using SmileMate.Common.Entities;

namespace SmileMate.Pages
{
    public class ReceptionCreate : PageModel
    {
        private readonly SmileMateContext _context;

        public ReceptionCreate(SmileMateContext context)
        {
            _context = context;
        }

        [BindProperty]
        public IEnumerable<Doctor> Doctors { get; set; }
        [BindProperty]
        public List<DateTime> ThisWeek { get; set; } = new();
        public Patient? Patient { get; set; }

        public async Task<IActionResult> OnGet(int? patientid)
        {
            Doctors = await _context.Set<Doctor>().ToListAsync();

            Patient = await _context.Set<Patient>().FirstOrDefaultAsync(p => p.Id == patientid);

            for(int i = 0; i <= 7; i++)
                ThisWeek.Add(DateTime.Now.AddDays(i));
            
            return Page();
        }
    }
}
