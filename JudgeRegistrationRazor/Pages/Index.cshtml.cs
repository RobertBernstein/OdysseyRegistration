using JudgeRegistrationRazor.Data;
using JudgeRegistrationRazor.Pages.Shared;
using Microsoft.AspNetCore.Mvc;

namespace JudgeRegistrationRazor.Pages
{
    public class IndexModel : BasePageModel
    {
        public IndexModel(OdysseyContext context, ILogger<IndexModel> logger)
            : base(context, logger)
        {
        }

        public void OnGet()
        {
        }

        // TODO: Change this back to OnPostAsync.
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // TODO: Fix this possible null reference.
            return Redirect(Config?["HomePage"]);
        }
    }
}
