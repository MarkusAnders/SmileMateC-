using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmileMate.Common.Entities;
using SmileMate.Common;
using Microsoft.EntityFrameworkCore;

namespace SmileMate.Pages
{
    public class DoctorEdit : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly SmileMateContext _context;

        public DoctorEdit(UserManager<User> userManager, SmileMateContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public User CurrentDoctor { get; set; }

        public async Task<IActionResult> OnGet(int? doctorid)
        {
            if (doctorid is null)
                return NotFound("Id not provided");

            var user = await _context.Set<User>().FirstOrDefaultAsync(d => d.Id == doctorid);

            if(user is null)
                return NotFound($"Доктор с ID {doctorid} не найден");

            CurrentDoctor = user;

            return Page();
        }

        public async Task<IActionResult> OnPost(int? doctorid, string surname, string name, string patronymic)
        {
            var doctor = await _context.Set<User>().FirstOrDefaultAsync(d => d.Id == doctorid);

            if (doctor is null)
                return NotFound($"Доктор с ID {doctorid} не найден");

            if(!String.IsNullOrEmpty(surname))
                doctor.Surname = surname;

            if (!String.IsNullOrEmpty(name))
                doctor.Name = name;

            if (!String.IsNullOrEmpty(patronymic))
                doctor.Patronymic = patronymic;

            var result = await _userManager.UpdateAsync(doctor);

            return RedirectToPage("/DoctorEdit", new { doctorid = doctorid });
        }
    }
}
