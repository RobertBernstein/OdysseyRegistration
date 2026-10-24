using JudgeRegistrationRazor.Data;
using JudgeRegistrationRazor.Pages.Shared;
using Microsoft.AspNetCore.Mvc;

namespace JudgeRegistrationRazor.Pages
{
    internal class IndexModel : BasePageModel
    {
        public IndexModel(OdysseyContext context, ILogger<IndexModel> logger)
            : base(context, logger)
        {
        }

        public void OnGet()
        {
            // This page intentionally has no server-side work; it exists to route the home page.
            _ = HttpContext;
        }

        // TODO: Change this back to OnPostAsync.
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (Config == null || !Config.TryGetValue("HomePage", out var homePage) || string.IsNullOrWhiteSpace(homePage))
            {
                return Page();
            }

            return Redirect(homePage);
        }
    }
}
