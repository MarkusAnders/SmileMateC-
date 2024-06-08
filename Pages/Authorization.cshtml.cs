using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmileMate.Common;
using SmileMate.Common.Entities;

namespace SmileMate.Pages
{
    public class Authorization : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly SmileMateContext _context;

        public Authorization(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<Role> roleManager, SmileMateContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _context = context;
        }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostLoginAsync(string login, string password)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(login, password, isPersistent: false, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    var user = await _userManager.FindByNameAsync(login);
                    if (user != null)
                    {
                        var roles = await _userManager.GetRolesAsync(user);

                        if (roles.Contains("Admin"))
                        {
                            return RedirectToPage("/Index");
                        }
                        /*                        else if (roles.Contains("Teacher"))
                                                {
                                                    return RedirectToPage("/MyCourses");
                                                }
                                                else if (roles.Contains("Student"))
                                                {
                                                    return RedirectToPage("/Index", new { FilterType = "InProgress" });
                                                }*/
                        else
                        {
                            return RedirectToPage("/Index");
                        }
                    }
                }
                else if (result.IsLockedOut)
                {
                    ModelState.AddModelError(string.Empty, "User account locked out.");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                }
            }

            var errorList = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
            return NotFound($"Список ошибок: {string.Join(", ", errorList)}");
        }

        public async Task<IActionResult> OnPostRegistrationAsync(string login, string password, string rb, string surname, string name, string patronymic)
        {

            if (rb == "doctor")
            {
                var doctor = new Doctor { UserName = login, Surname = surname, Name = name, Patronymic = patronymic };

                var result = await _userManager.CreateAsync(doctor, password);
                if (result.Succeeded)
                {
                    if (!await _roleManager.RoleExistsAsync("Doctor"))
                    {
                        await _roleManager.CreateAsync(new Role { Name = "Doctor" });
                    }

                    await _userManager.AddToRoleAsync(doctor, "Doctor");

                    return RedirectToPage("/Index");
                }
            }

            if (rb == "medicalworker")
            {
                var medicalworker = new User { UserName = login };

                var result = await _userManager.CreateAsync(medicalworker, password);
                if (result.Succeeded)
                {
                    if (!await _roleManager.RoleExistsAsync("MedicalWorker"))
                    {
                        await _roleManager.CreateAsync(new Role { Name = "MedicalWorker" });
                    }

                    await _userManager.AddToRoleAsync(medicalworker, "MedicalWorker");

                    return RedirectToPage("/Index");
                }
            }
            /*            else
                        {
                            if (result.Succeeded)
                            {
                                if (!await _roleManager.RoleExistsAsync("Teacher"))
                                {
                                    await _roleManager.CreateAsync(new Role { Name = "Teacher" });
                                }

                                await _userManager.AddToRoleAsync(user, "Teacher");
                                await _signInManager.SignInAsync(user, isPersistent: false);
                                return RedirectToPage("/ProfilePage");
                            }
                        }*/


            var errorList = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
            return NotFound($"Список ошибок: {string.Join(", ", errorList)}");
        }

        public async Task<IActionResult> OnPostCreatePatientAsync(string username, string password, string surname, string name, string patronymic, string phonenumber, string date, string adress)
        {
            var patient = new Patient { UserName = username, Surname = surname, Name = name, Patronymic = patronymic, PhoneNumber = phonenumber, BirthdayDate = DateTime.Parse(date), Address = adress };

            var result = await _userManager.CreateAsync(patient, password);
            
            if (result.Succeeded)
            {
                if (!await _roleManager.RoleExistsAsync("Patient"))
                {
                    await _roleManager.CreateAsync(new Role { Name = "Patient" });
                }

                await _userManager.AddToRoleAsync(patient, "Patient");

                return RedirectToPage("/Patient");
            }

            var errorList = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
            return NotFound($"Список ошибок: {string.Join(", ", errorList)}");
        }

        public async Task<IActionResult> OnPostLogoutAsync()
        {
            await _signInManager.SignOutAsync();

            return RedirectToPage("/Index");
        }
    }
}
