//// --------------------------------------------------------------------------------------------------------------------
//// <copyright file="Page01.cshtml.cs" company="Tardis Technologies">
////   Copyright 2022 Tardis Technologies. All rights reserved.
//// </copyright>
//// <summary>
////   Defines the Page01Model type.
//// </summary>
//// --------------------------------------------------------------------------------------------------------------------

using OdysseyCoreMvc.Data;

namespace OdysseyCoreMvc.Pages.VolunteerRegistration
{
    /// <summary>
    /// The Volunteer Registration Page01 page model.
    /// 
    /// Razor Pages are derived from PageModel. By convention, the PageModel derived class is named PageNameModel. For
    /// example, the Index page is named IndexModel.
    /// https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.razorpages.pagemodel
    /// </summary>
    public class Page01Model : BasePageModel
    {
        // The constructor uses dependency injection to add the OdysseyContext and logging to the page.
        // https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-6.0
        public Page01Model(OdysseyContext context, ILogger<BasePageModel> logger) : base(context, logger)
        {
            this.CurrentRegistrationType = RegistrationType.Volunteer;
            this.FriendlyRegistrationName = this.GetDisplayableRegistrationName();
        }

        public string? MailRegionalDirectorHyperLink { get; set; }

        public string? MailRegionalDirectorHyperLinkText { get; set; }
    }
}
