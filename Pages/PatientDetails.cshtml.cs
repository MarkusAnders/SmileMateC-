using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SmileMate.Pages
{
    public class PatientDetails: PageModel
    {
        private readonly ILogger<PatientDetails> _logger;

        public PatientDetails(ILogger<PatientDetails> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}
