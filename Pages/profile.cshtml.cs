using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmileMate.Common.Entities;
using SmileMate.Common;

namespace SmileMate.Pages
{
    public class Profile : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly SmileMateContext _context;

        public Profile(UserManager<User> userManager, SmileMateContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public User CurrentUser { get; set; }

        public async Task<IActionResult> OnGet()
        {
            if (User.Identity.IsAuthenticated)
                CurrentUser = await _userManager.GetUserAsync(User);
            else
                return NotFound("Авторизируйтесь, перед тем как смотреть профиль");
            
            return Page();
        }


        public async Task<IActionResult> OnPost(string newpassword)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user != null)
                {
                    var result = await _userManager.UpdateAsync(user);

                    if (result.Succeeded)
                    {
                        if (!string.IsNullOrEmpty(newpassword))
                        {
                            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                            var passwordChangeResult = await _userManager.ResetPasswordAsync(user, token, newpassword);

                            if (!passwordChangeResult.Succeeded)
                            {
                                foreach (var error in passwordChangeResult.Errors)
                                {
                                    ModelState.AddModelError(string.Empty, error.Description);
                                }

                                return NotFound($"Список ошибок: {string.Join(", ", (List<string>?)ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList())}");
                            }
                        }

                        return RedirectToPage("/Profile");
                    }

                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "User not found.");
                }
            }

            var errorList = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
            return NotFound($"Список ошибок: {string.Join(", ", errorList)}");
        }
    }
}
