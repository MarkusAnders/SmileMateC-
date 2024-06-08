using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SmileMate.Pages
{
    public class DoctorEdit : PageModel
    {
        private readonly ILogger<DoctorEdit> _logger;

        public DoctorEdit(ILogger<DoctorEdit> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}
