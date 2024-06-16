using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SmileMate.Common;
using SmileMate.Common.Entities;

namespace SmileMate.Pages
{
    public class ReceptionCompleted : PageModel
    {
        private readonly SmileMateContext _context;

        public ReceptionCompleted(SmileMateContext context)
        {
            _context = context;
        }

        [BindProperty]
        public List<Reception> Receptions { get; set; }

        public async Task<IActionResult> OnGet(string searchTerm)
        {
            DateTime currentDateTime = DateTime.Now;
            DateTime oneHourAgo = currentDateTime.AddHours(-1);

            IQueryable<Reception> query = _context.Set<Reception>()
                .Include(r => r.Client).Include(r => r.Doctor)
                .Where(r => new DateTime(r.Date.Year, r.Date.Month, r.Date.Day, r.Time.Hour, r.Time.Minute, r.Time.Second) < oneHourAgo);

            if (!String.IsNullOrEmpty(searchTerm))
            {
                searchTerm = searchTerm.ToLower();

                query = query.Where(r => r.Doctor.Surname.ToLower().Contains(searchTerm)
                    || r.Doctor.Name.ToLower().Contains(searchTerm)
                    || r.Doctor.Patronymic.ToLower().Contains(searchTerm)
                    || r.Client.Name.ToLower().Contains(searchTerm)
                    || r.Client.Surname.ToLower().Contains(searchTerm)
                    || r.Client.Patronymic.ToLower().Contains(searchTerm));
            }

            // Добавляем сортировку по дате и времени в порядке убывания
            Receptions = await query
                .OrderByDescending(r => r.Date)
                .ThenByDescending(r => r.Time)
                .ToListAsync();

            return Page();
        }

        public async Task<IActionResult> OnPost(int? receptionid)
        {
            if (receptionid == null)
            {
                return NotFound();
            }

            var receptionToDelete = await _context.Set<Reception>()
                .Include(r => r.MedicalHistory)
                .FirstOrDefaultAsync(r => r.Id == receptionid);

            if (receptionToDelete == null)
            {
                return NotFound();
            }

            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    if (receptionToDelete.MedicalHistory.Any())
                    {
                        _context.RemoveRange(receptionToDelete.MedicalHistory);
                    }

                    _context.Remove(receptionToDelete);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    // Логируем исключение или обрабатываем его иначе
                    return StatusCode(500, "Internal server error: " + ex.Message);
                }
            }

            return RedirectToPage("/ReceptionCompleted"); // Перенаправляем пользователя на страницу со списком
        }
    }
}
