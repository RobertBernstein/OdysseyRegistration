// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Page01ViewData.cs" company="Tardis Technologies">
//   Copyright 2022 Tardis Technologies. All rights reserved.
// </copyright>
// <summary>
//   The page 01 view data.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using OdysseyCoreMvc.Models;

namespace OdysseyCoreMvc.ViewData.CoachesTrainingRegistration
{
    /// <summary>
    /// The page 01 view data.
    /// </summary>
    public class Page01ViewData : BaseViewData
    {
        /// <summary>
        /// Gets or sets the coaches training info.
        /// </summary>
        public Event? CoachesTrainingInfo { get; set; }

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
        public Models.CoachesTrainingRegistration? CoachesTrainingRegistration { get; set; }

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

                return this.Config["CoordinatorsDoNotPayCoachesTrainingRegistrationFee"].ToLower() == "true"
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
                return CoachesTrainingInfo.StartDate != null
                    ? CoachesTrainingInfo.StartDate.Value.ToLongDateString()
                    : "TBA";
            }
        }

        /// <summary>
        /// Gets the coaches training time.
        /// </summary>
        public string CoachesTrainingTime
        {
            get
            {
                return !string.IsNullOrWhiteSpace(CoachesTrainingInfo.Time)
                    ? CoachesTrainingInfo.Time
                    : "TBA";
            }
        }
    }
}
