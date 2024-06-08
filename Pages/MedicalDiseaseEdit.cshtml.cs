using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SmileMate.Pages
{
    public class MedicalDiseaseEditModel : PageModel
    {
        private readonly ILogger<MedicalDiseaseEditModel> _logger;

        public MedicalDiseaseEditModel(ILogger<MedicalDiseaseEditModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}
