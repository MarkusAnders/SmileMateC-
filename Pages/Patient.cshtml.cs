using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SmileMate.Common;
using SmileMate.Common.Entities;

namespace SmileMate.Pages
{
    public class PatientModel : PageModel
    {
        private readonly SmileMateContext _context;

        public PatientModel(SmileMateContext context)
        {
            _context = context; 
        }

        public List<Patient> Patients { get; set; }

        public async Task<IActionResult> OnGetAsync(string searchTerm)
        {
            IQueryable<Patient> query = _context.Set<Patient>();

            if (!String.IsNullOrEmpty(searchTerm))
            {
                searchTerm = searchTerm.ToLower();

                query = query.Where(p => p.Name.ToLower().Contains(searchTerm)
                    || p.Surname.ToLower().Contains(searchTerm)
                    || p.Patronymic.ToLower().Contains(searchTerm)
                    || p.UserName.ToLower().Contains(searchTerm)
                    || p.PhoneNumber.Contains(searchTerm));
            }

            query = query.OrderBy(p => p.Surname);

            Patients = await query.ToListAsync();

            return Page();
        }
    }
}
