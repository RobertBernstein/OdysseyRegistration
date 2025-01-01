using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using JudgeRegistrationRazor.Data;
using JudgeRegistrationRazor.Models;
using JudgeRegistrationRazor.Pages.Shared;

namespace JudgeRegistrationRazor.Pages.JudgesRegistration
{
    /// <summary>
    /// The Judges Registration Page02 backing page data.
    /// 
    /// Razor Pages are derived from PageModel. By convention, the PageModel derived class is named PageNameModel. For
    /// example, the Index page is named IndexModel.
    /// https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.razorpages.pagemodel
    /// </summary>
    public class Page02Model : BasePageModel
    {
        public string Title { get; private set; }

        // The constructor uses dependency injection to add the OdysseyContext and logging to the page.
        // https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-6.0
        public Page02Model(OdysseyContext context, ILogger<Page01Model> logger)
            : base(context, logger)
        {
            CurrentRegistrationType = RegistrationType.Judges;
            Title = $"{DisplayableRegistrationName} Page 2 of 3";
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Judge Judge { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Context.Judges.Add(Judge);
            await Context.SaveChangesAsync();

            return RedirectToPage("Page03");
        }
    }
}
