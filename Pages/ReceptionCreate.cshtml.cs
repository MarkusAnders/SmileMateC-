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

            DateTime currentDate = DateTime.Now;
            int daysToAdd = 0;

            while (ThisWeek.Count < 22)
            {
                // Проверяем, что день не суббота и не воскресенье
                if (currentDate.AddDays(daysToAdd).DayOfWeek != DayOfWeek.Saturday &&
                    currentDate.AddDays(daysToAdd).DayOfWeek != DayOfWeek.Sunday)
                {
                    ThisWeek.Add(currentDate.AddDays(daysToAdd));
                }
                daysToAdd++;
            }

            return Page();
        }

        //public async Task<IActionResult> OnGet(int? patientid)
        //{
        //    Doctors = await _context.Set<Doctor>().ToListAsync();

        //    Patient = await _context.Set<Patient>().FirstOrDefaultAsync(p => p.Id == patientid);

        //    for(int i = 0; i <= 21; i++)
        //        ThisWeek.Add(DateTime.Now.AddDays(i));

        //    return Page();
        //}
    }
}
