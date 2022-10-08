// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Page02.cshtml.cs" company="Tardis Technologies">
//   Copyright 2022 Tardis Technologies. All rights reserved.
// </copyright>
// <summary>
//   Defines the Page02Model type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace OdysseyCoreMvc.Pages.TournamentRegistration
{
    /// <summary>
    /// The Tournament Registration Page02 page model.
    /// 
    /// Razor Pages are derived from PageModel. By convention, the PageModel derived class is named PageNameModel. For
    /// example, the Index page is named IndexModel.
    /// https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.razorpages.pagemodel
    /// </summary>
    public class Page02Model : BasePageModel
    {
        // The constructor uses dependency injection to add the OdysseyContext to the page.
        // https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-6.0
        public Page02Model(Data.OdysseyContext context)
            : base(context)
        {
            this.CurrentRegistrationType = RegistrationType.Tournament;
            this.FriendlyRegistrationName = this.GetDisplayableRegistrationName();
        }

        /// <summary>
        /// Gets or sets the school list.
        /// </summary>
        public IEnumerable<SelectListItem>? SchoolList { get; set; }

        /// <summary>
        /// Gets or sets the selected school.
        /// </summary>
        [Required(
            ErrorMessage =
                "Please select one of the schools or organizations listed in the \"School/Organization Name\" field.")]
        [Display(Name = "School or Sponsoring Organization Name")]
        public int SelectedSchool { get; set; }
    }
}
