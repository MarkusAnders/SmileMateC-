using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SmileMate.Pages
{
    public class ReceptionCreate : PageModel
    {
        private readonly ILogger<ReceptionCreate> _logger;

        public ReceptionCreate(ILogger<ReceptionCreate> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}
