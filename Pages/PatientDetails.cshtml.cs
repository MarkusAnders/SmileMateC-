using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using SmileMate.Common;
using SmileMate.Common.Entities;


namespace SmileMate.Pages
{
    public class PatientDetails : PageModel
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

        public async Task<IActionResult> OnPostAsync(int? patientid)
        {
            var patientToDelete = await _context.Set<Patient>().FirstOrDefaultAsync(p => p.Id == patientid);

            var receptionsToDelete = await _context.Set<Reception>()
                .Include(r => r.Client)
                .Include(r => r.Doctor)
                .Include(r => r.MedicalHistory)
                .Where(r => r.Client.Id == patientToDelete.Id)
                .ToListAsync();

            foreach (var reception in receptionsToDelete)
                if (reception.MedicalHistory.Count() != 0)
                    _context.RemoveRange(reception.MedicalHistory);

            _context.RemoveRange(receptionsToDelete);

            _context.Remove(patientToDelete);

            await _context.SaveChangesAsync();

            return RedirectToPage("/Patient");
        }
    }
}