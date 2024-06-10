using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SmileMate.Common;
using SmileMate.Common.Entities;


namespace SmileMate.Pages
{
    public class PatientDetails: PageModel
    {
        private readonly SmileMateContext _context;

        public PatientDetails(SmileMateContext context)
        {
            _context = context;
        }

        public Patient Patient { get; set; }

        public async Task<IActionResult> OnGetAsync(long id)
        {
            Patient = await _context.Patients.FindAsync(id);

            if (Patient == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
