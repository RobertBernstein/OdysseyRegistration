// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Page04.cshtml.cs" company="Tardis Technologies">
//   Copyright 2022 Tardis Technologies. All rights reserved.
// </copyright>
// <summary>
//   Defines the Page04Model type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using OdysseyCoreMvc.Data;
using OdysseyCoreMvc.Models;
using System.ComponentModel.DataAnnotations;

namespace OdysseyCoreMvc.Pages.TournamentRegistration
{
    /// <summary>
    /// The Tournament Registration Page04 page model.
    /// 
    /// Razor Pages are derived from PageModel. By convention, the PageModel derived class is named PageNameModel. For
    /// example, the Index page is named IndexModel.
    /// https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.razorpages.pagemodel
    /// </summary>
    public class Page04Model : BasePageModel
    {
        // The constructor uses dependency injection to add the OdysseyContext and logging to the page.
        // https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-6.0
        public Page04Model(OdysseyContext context, ILogger<BasePageModel> logger) : base(context, logger)
        {
            CurrentRegistrationType = RegistrationType.Tournament;
        }

        public bool NoVolunteersFound { get; set; }

        public bool VolunteerAlreadyTaken { get; set; }

        [StringLength(0x19, ErrorMessage = "The Volunteer's first name must not be more than 25 characters."), Required, Display(Name = "Volunteer's First Name")]
        public string? VolunteerFirstName { get; set; }

        public Volunteers? VolunteerFound { get; set; }

        [Range(0, 0x7fffffff, ErrorMessage = "The Volunteer's ID must only contain numeric digits."), StringLength(4, ErrorMessage = "The Volunteer's ID must not be more than 3 digits."), Required, Display(Name = "Volunteer's ID Number")]
        public string? VolunteerId { get; set; }

        [Required, StringLength(0x19, ErrorMessage = "The Volunteer's last name must not be more than 25 characters."), Display(Name = "Volunteer's Last Name")]
        public string? VolunteerLastName { get; set; }
    }
}

