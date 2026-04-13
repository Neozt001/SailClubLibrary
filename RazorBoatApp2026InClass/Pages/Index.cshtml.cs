using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorBoatApp2026InClass.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public string PhoneNumber { get; set; }
        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public IActionResult OnGet()
        {

            PhoneNumber = HttpContext.Session.GetString("PhoneNumber");
            if (PhoneNumber == null)
            {
                return RedirectToPage("Members/Login");
            }
            else return Page();
        }
    }
}
