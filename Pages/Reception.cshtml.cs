using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SmileMate.Common;
using SmileMate.Common.Entities;

namespace SmileMate.Pages
{
    public class ReceptionModel : PageModel
    {
        private readonly SmileMateContext _context;

        public ReceptionModel(SmileMateContext context)
        {
            _context = context;
        }

        [BindProperty]
        public List<Reception> Receptions { get; set; }

        public async Task<IActionResult> OnGet(string searchTerm)
        {
            IQueryable<Reception> query = _context.Set<Reception>()
                .Include(r => r.Client).Include(r => r.Doctor);

            if (!String.IsNullOrEmpty(searchTerm))
            {
                searchTerm = searchTerm.ToLower();

                query = query.Where(r => r.Doctor.Surname.ToLower().Contains(searchTerm)
                    || r.Doctor.Name.ToLower().Contains(searchTerm)
                    || r.Doctor.Patronymic.ToLower().Contains(searchTerm)
                    || r.Client.Name.ToLower().Contains(searchTerm)
                    || r.Client.Name.ToLower().Contains(searchTerm)
                    || r.Client.Name.ToLower().Contains(searchTerm));
            }

            Receptions = await query.ToListAsync();

            return Page();
        }

        public async Task<IActionResult> OnPost(int? receptionid)
        {
            var receptionToDelete = await _context.Set<Reception>()
                .Include(r => r.MedicalHistory)
                .FirstOrDefaultAsync(r => r.Id == receptionid);

            if (receptionToDelete.MedicalHistory.Count() != 0)
                _context.RemoveRange(receptionToDelete.MedicalHistory);

            _context.Remove(receptionToDelete);

            await _context.SaveChangesAsync();

            return Page();
        }
    }
}
