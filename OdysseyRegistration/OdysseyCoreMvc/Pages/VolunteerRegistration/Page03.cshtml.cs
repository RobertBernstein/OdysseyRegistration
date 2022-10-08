// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Page03.cshtml.cs" company="Tardis Technologies">
//   Copyright 2022 Tardis Technologies. All rights reserved.
// </copyright>
// <summary>
//   Defines the Page03Model type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using OdysseyCoreMvc.Models;

namespace OdysseyCoreMvc.Pages.VolunteerRegistration
{
    /// <summary>
    /// The Volunteer Registration Page03 page model.
    /// 
    /// Razor Pages are derived from PageModel. By convention, the PageModel derived class is named PageNameModel. For
    /// example, the Index page is named IndexModel.
    /// https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.razorpages.pagemodel
    /// </summary>
    public class Page03Model : BasePageModel
    {
        // The constructor uses dependency injection to add the RazorPagesMovieContext to the page.
        // https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-6.0
        public Page03Model(Data.OdysseyContext context)
            : base(context)
        {
            this.CurrentRegistrationType = RegistrationType.Volunteer;
            this.FriendlyRegistrationName = this.GetDisplayableRegistrationName();
        }

        public bool EmailAddressWasSpecified { get; set; }

        public string? ErrorMessage { get; set; }

        public string? MailBody { get; set; }

        public string? MailErrorMessage { get; set; }

        public Volunteers? Volunteer { get; set; }

        public Events? VolunteerInfo { get; set; }
    }
}
