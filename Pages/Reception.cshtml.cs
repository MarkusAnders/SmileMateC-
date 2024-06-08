using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SmileMate.Pages
{
    public class ReceptionModel : PageModel
    {
        private readonly ILogger<ReceptionModel> _logger;

        public ReceptionModel(ILogger<ReceptionModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}
