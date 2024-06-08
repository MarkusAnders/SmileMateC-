using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SmileMate.Pages
{
    public class PatientModel : PageModel
    {
        private readonly ILogger<PatientModel> _logger;

        public PatientModel(ILogger<PatientModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}
