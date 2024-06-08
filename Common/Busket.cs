using Microsoft.AspNetCore.Identity;
using SmileMate.Common.Entities;

namespace SmileMate.Common
{
    public static class Busket
    {
        public static async Task FillDataBase(UserManager<User> _userManager, RoleManager<Role> _roleManager)
        {
            var admin = await _userManager.FindByNameAsync("FirstAdmin");

            if (admin == null)
            {
                admin = new User { UserName = "Administrator" };
                var resultOne = await _userManager.CreateAsync(admin, "Administrator1!");
                if (resultOne.Succeeded)
                {
                    if (!await _roleManager.RoleExistsAsync("Admin"))
                        await _roleManager.CreateAsync(new Role { Name = "Admin" });

                    await _userManager.AddToRoleAsync(admin, "Admin");
                }
            }
        }
    }
}
