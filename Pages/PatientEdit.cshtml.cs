using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SmileMate.Common;
using SmileMate.Common.Entities;

namespace SmileMate.Pages
{
    public class PatientEdit : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly SmileMateContext _context;

        public PatientEdit(UserManager<User> userManager, SmileMateContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public Patient CurrentPatient { get; set; }

        public async Task<IActionResult> OnGet(int? patientid)
        {
            if (patientid is null)
                return NotFound("Id not provided");

            var user = await _context.Set<Patient>().FirstOrDefaultAsync(d => d.Id == patientid);

            if (user is null)
                return NotFound($"Пацинет с ID {patientid} не найден");

            CurrentPatient = user;

            return Page();
        }

        public async Task<IActionResult> OnPost(int? patientid, string surname, string name, string patronymic, string phoneNumber , DateTime? birthdayDate, string address)
        {
            var patient = await _context.Set<Patient>().FirstOrDefaultAsync(d => d.Id == patientid);

            if (patient is null)
                return NotFound($"Пацинет с ID {patientid} не найден");

            if (!String.IsNullOrEmpty(surname))
                patient.Surname = surname;

            if (!String.IsNullOrEmpty(name))
                patient.Name = name;

            if (!String.IsNullOrEmpty(patronymic))
                patient.Patronymic = patronymic;

            if (!String.IsNullOrEmpty(phoneNumber))
                patient.PhoneNumber = phoneNumber;

            if (birthdayDate is not null)
                patient.BirthdayDate = (DateTime)birthdayDate;

            if (!String.IsNullOrEmpty(address))
                patient.Address = address;

            var result = await _userManager.UpdateAsync(patient);

            return RedirectToPage("/PatientDetails", new { id = patientid });
        }
    }
}
