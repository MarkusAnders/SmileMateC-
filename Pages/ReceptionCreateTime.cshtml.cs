using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SmileMate.Pages
{
    public class ReceptionCreateTime : PageModel
    {
        private readonly ILogger<ReceptionCreateTime> _logger;

        public ReceptionCreateTime(ILogger<ReceptionCreateTime> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}
