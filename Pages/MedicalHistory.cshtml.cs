using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SmileMate.Pages
{
    public class MedicalHistoryModel : PageModel
    {
        private readonly ILogger<MedicalHistoryModel> _logger;

        public MedicalHistoryModel(ILogger<MedicalHistoryModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}
