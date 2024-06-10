using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SmileMate.Common;
using SmileMate.Common.Entities;
using SmileMate.Models;

namespace SmileMate.Pages
{
    public class ReceptionCreateTime : PageModel
    {
        private readonly SmileMateContext _context;

        public ReceptionCreateTime(SmileMateContext context)
        {
            _context = context;
        }

        [BindProperty]
        public List<TimeOnly> Times { get; set; } = new();
        [BindProperty]
        public TempDoctorDate DoctorDate { get; set; }
        public async Task<IActionResult> OnGet(int? selecteddoctor, string? selecteddate, int? patientid)
        {
            DoctorDate = new TempDoctorDate();

            DoctorDate.SelectedDoctor = selecteddoctor;
            DoctorDate.SelectedDate = selecteddate;
            DoctorDate.SelectedPatient = patientid;

            var times = new List<TimeOnly>() 
            { 
                new TimeOnly(8, 30),
                new TimeOnly(9, 0),
                new TimeOnly(9, 30),
                new TimeOnly(10, 0),
                new TimeOnly(10, 30),
                new TimeOnly(11, 0),
                new TimeOnly(11, 30),
                new TimeOnly(12, 0),
                new TimeOnly(12, 30),
                new TimeOnly(13, 0),
                new TimeOnly(13, 30),
                new TimeOnly(14, 0),
                new TimeOnly(14, 30),
                new TimeOnly(15, 0),
                new TimeOnly(15, 30),
                new TimeOnly(16, 0) 
            };

            var receptions = await _context.Set<Reception>()
                .Include(r => r.Doctor)
                .Where(r => r.Doctor.Id == selecteddoctor)
                .ToListAsync();

            var receptionsToTime = receptions.Where(r => r.Date == DateOnly.Parse(selecteddate)).Select(r => r.Time).ToList();

            Times = times.Except(receptionsToTime).ToList();

            return Page();
        }

        public async Task<IActionResult> OnPost(string? time, int? patientid)
        {
            var doctor = await _context.Set<Doctor>().FirstOrDefaultAsync(d => d.Id == DoctorDate.SelectedDoctor);
            var patient = await _context.Set<Patient>().FirstOrDefaultAsync(p => p.Id == patientid);

            var reception = new Reception() { Client = patient, Doctor = doctor, Date = DateOnly.Parse(DoctorDate.SelectedDate), Time = TimeOnly.Parse(time) };

            await _context.AddAsync(reception);

             await _context.SaveChangesAsync();

            return RedirectToPage("/Reception");
        }
    }
}
