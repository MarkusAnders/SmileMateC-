using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SmileMate.Pages
{
    public class MedicalDiseaseModel : PageModel
    {
        private readonly ILogger<MedicalDiseaseModel> _logger;

        public MedicalDiseaseModel(ILogger<MedicalDiseaseModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}
