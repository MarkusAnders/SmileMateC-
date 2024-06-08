using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SmileMate.Pages
{
    public class MedicalDiseaseCreateModel : PageModel
    {
        private readonly ILogger<MedicalDiseaseCreateModel> _logger;

        public MedicalDiseaseCreateModel(ILogger<MedicalDiseaseCreateModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}
