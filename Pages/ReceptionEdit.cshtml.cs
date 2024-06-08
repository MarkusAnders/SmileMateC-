using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SmileMate.Pages
{
    public class ReceptionEdit : PageModel
    {
        private readonly ILogger<ReceptionEdit> _logger;

        public ReceptionEdit(ILogger<ReceptionEdit> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}
