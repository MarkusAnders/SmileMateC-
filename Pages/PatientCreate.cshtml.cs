using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SmileMate.Pages
{
    public class PatientCreate: PageModel
    {
        private readonly ILogger<PatientCreate> _logger;

        public PatientCreate(ILogger<PatientCreate> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }

    }
}
