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
        public async Task<IActionResult> OnGet(int? patientid, string searchTerm)
        {
            IQueryable<MedicalHistory> query = _context.Set<MedicalHistory>()
                .Include(mh => mh.Reception).ThenInclude(r => r.Client)
                .Include(mh => mh.Reception).ThenInclude(r => r.Doctor)
                .Where(mh => mh.Reception.Client.Id == patientid);

            if (!String.IsNullOrEmpty(searchTerm))
            {
                searchTerm = searchTerm.ToLower();

                DateOnly.TryParse(searchTerm, out DateOnly result);

                query = query.Where(mh => mh.Reception.Doctor.Surname.ToLower().Contains(searchTerm)
                    || mh.Reception.Doctor.Name.ToLower().Contains(searchTerm)
                    || mh.Reception.Doctor.Patronymic.ToLower().Contains(searchTerm)
                    || mh.Reception.Date == result);
            }

            query = query.OrderByDescending(mh => mh.Reception.Date);

            Patient = await _context.Set<Patient>().FirstOrDefaultAsync(p => p.Id == patientid);

            MedicalHystory = await query.ToListAsync();


            return Page();
        }
    }
}
