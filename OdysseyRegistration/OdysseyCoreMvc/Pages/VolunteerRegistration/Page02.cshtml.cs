// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Page01.cshtml.cs" company="Tardis Technologies">
//   Copyright 2022 Tardis Technologies. All rights reserved.
// </copyright>
// <summary>
//   Defines the Page02Model type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using OdysseyCoreMvc.Data;
using System.ComponentModel.DataAnnotations;

namespace OdysseyCoreMvc.Pages.VolunteerRegistration
{
    /// <summary>
    /// The Volunteer Registration Page02 page model.
    /// 
    /// Razor Pages are derived from PageModel. By convention, the PageModel derived class is named PageNameModel. For
    /// example, the Index page is named IndexModel.
    /// https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.razorpages.pagemodel
    /// </summary>
    public class Page02Model : BasePageModel
    {
        // The constructor uses dependency injection to add the OdysseyContext and logging to the page.
        // https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-6.0
        public Page02Model(OdysseyContext context, ILogger<BasePageModel> logger) : base(context, logger)
        {
            this.CurrentRegistrationType = RegistrationType.Volunteer;
            this.FriendlyRegistrationName = this.GetDisplayableRegistrationName();
        }

        [Display(Name="Daytime Phone"), DataType(DataType.PhoneNumber), Required, RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage="This is not a valid phone number")]
        public string? DaytimePhone { get; set; }

        [Required, Display(Name="E-mail Address")]
        public string? EmailAddress { get; set; }

        [Compare("EmailAddress", ErrorMessage="Your e-mail addresses do not match"), Display(Name="Confirm E-mail Address"), Required]
        public string? EmailConfirmation { get; set; }

        [Required, DataType(DataType.PhoneNumber), Display(Name="Evening Phone"), RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage="This is not a valid phone number")]
        public string? EveningPhone { get; set; }

        [Required, Display(Name="First Name")]
        public string? FirstName { get; set; }

        [Required, Display(Name="Last Name")]
        public string? LastName { get; set; }

        [DataType(DataType.PhoneNumber), Display(Name="Mobile Phone"), Required, RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage="This is not a valid phone number")]
        public string? MobilePhone { get; set; }

        [StringLength(500, ErrorMessage="The notes field cannot contain more than 500 characters.")]
        public string? Notes { get; set; }

        public string? VolunteerWantsToSee { get; set; }
    }
}

