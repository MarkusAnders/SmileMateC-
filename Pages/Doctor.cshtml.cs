using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SmileMate.Common;
using SmileMate.Common.Entities;

namespace SmileMate.Pages
{
    public class DoctorModel : PageModel
    {
        private readonly SmileMateContext _context;

        public DoctorModel(SmileMateContext context)
        {
            _context = context;
        }

        public List<Doctor> Doctors { get; set; }

        public async Task<IActionResult> OnGetAsync(string searchTerm)
        {
            IQueryable<Doctor> query = _context.Set<Doctor>();

            if(!String.IsNullOrEmpty(searchTerm))
            {
                searchTerm = searchTerm.ToLower();

                query = query.Where(d => d.Name.ToLower().Contains(searchTerm)
                    || d.Surname.ToLower().Contains(searchTerm)
                    || d.Patronymic.ToLower().Contains(searchTerm)
                    || d.UserName.ToLower().Contains(searchTerm));
            }

            Doctors = await query.ToListAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? doctorid)
        {
            var doctor = await _context.Set<User>().FirstOrDefaultAsync(d => d.Id == doctorid);

            _context.Remove(doctor);
            await _context.SaveChangesAsync();

            return RedirectToPage();
        }
    }
}
