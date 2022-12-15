using Microsoft.AspNetCore.Mvc;
using OdysseyCoreMvc.Data;

namespace OdysseyCoreMvc.Pages
{
    public class IndexModel : BasePageModel
    {
        private readonly ILogger<BasePageModel> _logger;

        public IndexModel(OdysseyContext context, ILogger<BasePageModel> logger) : base(context, logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            ViewData["Message"] =
                $"Welcome to the {Config["RegionName"]} Odyssey of the Mind Region {Config["RegionNumber"]} Registration web pages.";
        }

        // TODO: Change this back to OnPostAsync.
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            return Redirect(Config["HomePage"]);
        }
    }
}