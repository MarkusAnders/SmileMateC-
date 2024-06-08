using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SmileMate.Pages
{
    public class PatientEdit : PageModel
    {
        private readonly ILogger<PatientEdit> _logger;

        public PatientEdit(ILogger<PatientEdit> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}
