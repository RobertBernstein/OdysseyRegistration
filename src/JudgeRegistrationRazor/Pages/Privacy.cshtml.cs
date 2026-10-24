using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JudgeRegistrationRazor.Pages
{
    internal class PrivacyModel : PageModel
    {
        public void OnGet()
        {
            // This page intentionally has no server-side work; it exists to render the privacy policy.
            _ = HttpContext;
        }
    }

}
