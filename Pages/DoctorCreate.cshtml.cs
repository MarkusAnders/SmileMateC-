using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SmileMate.Pages
{
    public class DoctorCreate : PageModel
    {
        private readonly ILogger<DoctorCreate> _logger;

        public DoctorCreate(ILogger<DoctorCreate> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}
