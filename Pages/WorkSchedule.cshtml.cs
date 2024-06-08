using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SmileMate.Pages
{
    public class WorkSchedule : PageModel
    {
        private readonly ILogger<WorkSchedule> _logger;

        public WorkSchedule(ILogger<WorkSchedule> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}
