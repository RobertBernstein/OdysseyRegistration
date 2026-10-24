// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Page01.cshtml.cs" company="Tardis Technologies">
//   Copyright 2022 Tardis Technologies. All rights reserved.
// </copyright>
// <summary>
//   Defines the Page01Model type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using OdysseyCoreMvc.Data;
using OdysseyCoreMvc.Models;

namespace OdysseyCoreMvc.Pages.CoachesTrainingRegistration
{
    /// <summary>
    /// <para>The Coaches Training Registration Page01 page model.</para>
    /// <para>
    /// Razor Pages are derived from PageModel. By convention, the PageModel derived class is named PageNameModel. For
    /// example, the Index page is named IndexModel.
    /// https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.razorpages.pagemodel
    /// </para>
    /// </summary>

    // The constructor uses dependency injection to add the OdysseyContext and logging to the page.
    // https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-6.0
    internal sealed class Page01Model : BasePageModel
    {
        internal Page01Model(OdysseyContext context, ILogger<Page01Model> logger) : base(context ?? throw new System.ArgumentNullException(nameof(context)), logger ?? throw new System.ArgumentNullException(nameof(logger)))
        {
        }

        /// <summary>
        /// Gets or sets the coaches training info.
        /// </summary>
        public Events? CoachesTrainingInfo { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        [Required]
        [Display(Name = "First Name")]
        public string? FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        [Required]
        [Display(Name = "Last Name")]
        public string? LastName { get; set; }

        /// <summary>
        /// Gets or sets the school name.
        /// </summary>
        [Required]
        [Display(Name = "School Name")]
        public string? SchoolName { get; set; }

        /// <summary>
        /// Gets or sets the role list.
        /// </summary>
        public IEnumerable<SelectListItem>? RoleList { get; set; }

        /// <summary>
        /// Gets or sets the selected role.
        /// </summary>
        [Required]
        [Display(Name = "Role")]

        public string? SelectedRole { get; set; }

        /// <summary>
        /// Gets or sets the division list.
        /// </summary>
        public IEnumerable<SelectListItem>? DivisionList { get; set; }

        /// <summary>
        /// Gets or sets the selected division.
        /// </summary>
        [Required]
        [Display(Name = "Division")]

        public string? SelectedDivision { get; set; }

        /// <summary>
        /// Gets or sets the problem list.
        /// </summary>
        public IEnumerable<SelectListItem>? ProblemList { get; set; }

        /// <summary>
        /// Gets or sets the selected problem.
        /// </summary>
        [Required]
        [Display(Name = "Problem")]
        public string? SelectedProblem { get; set; }

        /// <summary>
        /// Gets or sets the e-mail address.
        /// </summary>
        [Required]
        [Display(Name = "E-mail Address")]
        public string? EmailAddress { get; set; }

        /// <summary>
        /// Gets or sets the email confirmation.
        /// </summary>
        [Required]
        [Display(Name = "Confirm E-mail Address")]
        [Compare("EmailAddress", ErrorMessage = "Your e-mail addresses do not match")]
        public string? EmailConfirmation { get; set; }

        /// <summary>
        /// Gets or sets the years involved.
        /// </summary>
        [Display(Name = "Years as a Coach")]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter your number of years as a coach using only numeric digits")]
        public string? YearsInvolved { get; set; }

        /// <summary>
        /// Gets or sets the region list.
        /// </summary>
        public IEnumerable<SelectListItem>? RegionList { get; set; }

        /// <summary>
        /// Gets or sets the selected region.
        /// </summary>
        [Display(Name = "Region #")]

        public string? SelectedRegion { get; set; }

        /// <summary>
        /// Gets or sets the coaches training registration.
        /// </summary>
        public Models.CoachesTrainingRegistrations? CoachesTrainingRegistration { get; set; }

        /// <summary>
        /// Gets the coordinators do not pay coaches training registration fee message.
        /// </summary>
        public string CoordinatorsDoNotPayCoachesTrainingRegistrationFeeMessage
        {
            get
            {
                if (!Config.ContainsKey("CoordinatorsDoNotPayCoachesTrainingRegistrationFee"))
                {
                    return string.Empty;
                }

                return bool.TryParse(
                        Config["CoordinatorsDoNotPayCoachesTrainingRegistrationFee"],
                        out bool parsed) && parsed
                    ? " &nbsp;We invite School Coordinators to attend at no charge."
                    : string.Empty;
            }
        }

        /// <summary>
        /// Gets the coaches training date.
        /// </summary>
        public string CoachesTrainingDate
        {
            get
            {
                return CoachesTrainingInfo?.StartDate is DateTime startDate
                    ? startDate.ToLongDateString()
                    : "TBA";
            }
        }

        /// <summary>
        /// Gets the coaches training time.
        /// </summary>
        public string CoachesTrainingTime => !string.IsNullOrWhiteSpace(CoachesTrainingInfo?.Time)
            ? CoachesTrainingInfo.Time
            : "TBA";
    }
}
