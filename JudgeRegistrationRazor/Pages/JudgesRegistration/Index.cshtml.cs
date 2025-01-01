using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JudgeRegistrationRazor.Pages.JudgesRegistration
{
    public class IndexModel : PageModel
    {
        public IndexModel()
        {
        }

        public IActionResult OnGet()
        {
            return RedirectToPage("Page01");
        }
    }
}
