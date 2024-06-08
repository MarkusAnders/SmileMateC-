using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SmileMate.Pages
{
    public class Profile : PageModel
    {
        private readonly ILogger<Profile> _logger;

        public Profile(ILogger<Profile> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}
