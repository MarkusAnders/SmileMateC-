using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SmileMate.Pages
{
    public class DoctorModel : PageModel
    {
        private readonly ILogger<DoctorModel> _logger;

        public DoctorModel(ILogger<DoctorModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}
