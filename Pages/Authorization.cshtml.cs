using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmileMate.Common;
using SmileMate.Common.Entities;
using MimeKit;
using MailKit.Net.Smtp;

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
                var user = await _userManager.FindByNameAsync(login);
                if (user == null)
                {
                    return new JsonResult(new { success = false, message = "Пользователь не найден" });
                }

                var result = await _signInManager.PasswordSignInAsync(user.UserName, password, isPersistent: false, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    return new JsonResult(new { success = true, redirectUrl = roles.Contains("Admin") ? Url.Page("/Index") : Url.Page("/Index") });
                }
                else if (result.IsLockedOut)
                {
                    return new JsonResult(new { success = false, message = "Аккаунт заблокирован" });
                }
                else
                {
                    return new JsonResult(new { success = false, message = "Неверный логин или пароль" });
                }
            }

            return new JsonResult(new { success = false, message = "Неверные данные ввода" });
        }



        //public async Task<IActionResult> OnPostLoginAsync(string login, string password)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var user = await _userManager.FindByNameAsync(login);
        //        if (user != null)
        //        {
        //            var result = await _signInManager.PasswordSignInAsync(user.UserName, password, isPersistent: false, lockoutOnFailure: false);

        //            if (result.Succeeded)
        //            {
        //                var roles = await _userManager.GetRolesAsync(user);
        //                if (roles.Contains("Admin"))
        //                {
        //                    return RedirectToPage("/Index");
        //                }
        //                else
        //                {
        //                    return RedirectToPage("/Index");
        //                }
        //            }
        //            else if (result.IsLockedOut)
        //            {
        //                ModelState.AddModelError(string.Empty, "User account locked out.");
        //            }
        //            else
        //            {
        //                ModelState.AddModelError(string.Empty, "Неверный логин или пароль");
        //            }
        //        }
        //        else
        //        {
        //            ModelState.AddModelError(string.Empty, "Неверный логин или пароль");
        //        }
        //    }

        //    return Page();
        //}


        //public async Task<IActionResult> OnPostLoginAsync(string login, string password)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var result = await _signInManager.PasswordSignInAsync(login, password, isPersistent: false, lockoutOnFailure: false);

        //        if (result.Succeeded)
        //        {
        //            var user = await _userManager.FindByNameAsync(login);
        //            if (user != null)
        //            {
        //                var roles = await _userManager.GetRolesAsync(user);

        //                if (roles.Contains("Admin"))
        //                {
        //                    return RedirectToPage("/Index");
        //                }
        //                else
        //                {
        //                    return RedirectToPage("/Index");
        //                }
        //            }
        //        }
        //        else if (result.IsLockedOut)
        //        {
        //            ModelState.AddModelError(string.Empty, "User account locked out.");
        //        }
        //        else
        //        {
        //            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
        //        }
        //    }

        //    var errorList = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
        //    return NotFound($"Список ошибок: {string.Join(", ", errorList)}");
        //}


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

                    return RedirectToPage("/Doctor");
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

                    return RedirectToPage("/Patient");
                }
            }
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

                // Отправка электронного письма клиенту
                await SendEmailAsync(username, password);

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

        private readonly string _smtpServer = "smtp.gmail.com";
        private readonly int _smtpPort = 465;
        private readonly string _smtpUser = "serezha.tikhonov.2013@gmail.com";
        private readonly string _smtpPass = "onoewopmywkbkklk";
        private async Task SendEmailAsync(string username, string password)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("SmileMate", _smtpUser));
            message.To.Add(new MailboxAddress(username, username));
            message.Subject = $"Вы зарегистрированы в стоматологической клинике";

            var builder = new BodyBuilder
            {
                HtmlBody = GenerateEmailBody(username, password)
            };

            message.Body = builder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(_smtpServer, _smtpPort, true);
                await client.AuthenticateAsync(_smtpUser, _smtpPass);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
        }

        private string GenerateEmailBody(string username, string password)
        {
            var body = $"<div style=\"border-radius: 8px; background-color: #F2f2f2; padding: 20px 30px;\">" +
                       $"<p style=\"font-size: 32px; font-weight: 500;\">Ваш логин: {username}</p>" +
                       $"<p style=\"font-size: 32px; font-weight: 500;\">Ваш пароль: {password}</p>";

            return body;
        }
    }
}
