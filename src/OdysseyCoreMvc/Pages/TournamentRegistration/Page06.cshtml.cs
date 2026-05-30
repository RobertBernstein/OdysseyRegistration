// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Page06.cshtml.cs" company="Tardis Technologies">
//   Copyright 2022 Tardis Technologies. All rights reserved.
// </copyright>
// <summary>
//   Defines the Page06Model type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using OdysseyCoreMvc.Data;

namespace OdysseyCoreMvc.Pages.TournamentRegistration
{
    /// <summary>
    /// The Tournament Registration Page06 page model.
    /// 
    /// Razor Pages are derived from PageModel. By convention, the PageModel derived class is named PageNameModel. For
    /// example, the Index page is named IndexModel.
    /// https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.razorpages.pagemodel
    /// </summary>
    public class Page06Model : BasePageModel
    {
        // The constructor uses dependency injection to add the OdysseyContext and logging to the page.
        // https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-6.0
        public Page06Model(OdysseyContext context, ILogger<BasePageModel> logger) : base(context, logger)
        {
            CurrentRegistrationType = RegistrationType.Tournament;
        }

        /// <summary>
        /// Gets or sets the grade choices.
        /// </summary>
        public IEnumerable<SelectListItem>? GradeChoices { get; set; }

        /// <summary>
        /// Gets or sets the member first name 1.
        /// </summary>
        [Display(Name = "First Name")]
        [StringLength(0x19, ErrorMessage = "The team member's first name must not be more than 25 characters.")]
        [Required(
            ErrorMessage =
                "You must include at least Team Member 1 to proceed.  Make sure the first name field is filled in.")]
        public string? MemberFirstName1 { get; set; }

        /// <summary>
        /// Gets or sets the member first name 2.
        /// </summary>
        [StringLength(0x19, ErrorMessage = "The team member's first name must not be more than 25 characters.")]
        [Display(Name = "First Name")]
        public string? MemberFirstName2 { get; set; }

        [Display(Name="First Name"), StringLength(0x19, ErrorMessage="The team member's first name must not be more than 25 characters.")]
        public string? MemberFirstName3 { get; set; }

        [Display(Name="First Name"), StringLength(0x19, ErrorMessage="The team member's first name must not be more than 25 characters.")]
        public string? MemberFirstName4 { get; set; }

        [Display(Name="First Name"), StringLength(0x19, ErrorMessage="The team member's first name must not be more than 25 characters.")]
        public string? MemberFirstName5 { get; set; }

        [Display(Name="First Name"), StringLength(0x19, ErrorMessage="The team member's first name must not be more than 25 characters.")]
        public string? MemberFirstName6 { get; set; }

        [StringLength(0x19, ErrorMessage="The team member's first name must not be more than 25 characters."), Display(Name="First Name")]
        public string? MemberFirstName7 { get; set; }

        [Required(ErrorMessage="Please select the team member's grade."), Display(Name="Grade")]
        public string? MemberGrade1 { get; set; }

        [Display(Name="Grade")]
        public string? MemberGrade2 { get; set; }

        [Display(Name="Grade")]
        public string? MemberGrade3 { get; set; }

        [Display(Name="Grade")]
        public string? MemberGrade4 { get; set; }

        [Display(Name="Grade")]
        public string? MemberGrade5 { get; set; }

        [Display(Name="Grade")]
        public string? MemberGrade6 { get; set; }

        [Display(Name="Grade")]
        public string? MemberGrade7 { get; set; }

        [StringLength(0x19, ErrorMessage="The team member's last name must not be more than 25 characters."), Required, Display(Name="Last Name")]
        public string? MemberLastName1 { get; set; }

        [StringLength(0x19, ErrorMessage="The team member's last name must not be more than 25 characters."), Display(Name="Last Name")]
        public string? MemberLastName2 { get; set; }

        [Display(Name="Last Name"), StringLength(0x19, ErrorMessage="The team member's last name must not be more than 25 characters.")]
        public string? MemberLastName3 { get; set; }

        [Display(Name="Last Name"), StringLength(0x19, ErrorMessage="The team member's last name must not be more than 25 characters.")]
        public string? MemberLastName4 { get; set; }

        [Display(Name="Last Name"), StringLength(0x19, ErrorMessage="The team member's last name must not be more than 25 characters.")]
        public string? MemberLastName5 { get; set; }

        [StringLength(0x19, ErrorMessage="The team member's last name must not be more than 25 characters."), Display(Name="Last Name")]
        public string? MemberLastName6 { get; set; }

        [Display(Name="Last Name"), StringLength(0x19, ErrorMessage="The team member's last name must not be more than 25 characters.")]
        public string? MemberLastName7 { get; set; }
    }
}

