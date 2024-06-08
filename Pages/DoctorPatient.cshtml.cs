using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SmileMate.Pages
{
    public class DoctorPatient : PageModel
    {
        private readonly ILogger<DoctorPatient> _logger;

        public DoctorPatient(ILogger<DoctorPatient> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}
