// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Page02ViewData.cs" company="Tardis Technologies">
//   Copyright 2021 Tardis Technologies. All rights reserved.
// </copyright>
// <summary>
//   Defines the Page02ViewData type.
// </summary>
// <created>
//   October 27th, 2013
// </created>
// --------------------------------------------------------------------------------------------------------------------

namespace OdysseyMvc4.ViewData.JudgesRegistration
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Diagnostics.CodeAnalysis;
    using System.Web.Mvc;
    using OdysseyMvc4.ViewData;

    /// <summary>
    /// The page 02 view data.
    /// </summary>
    public class Page02ViewData : BaseViewData
    {
        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        [Required]
        [Display(Name = "Street Address")]
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the address line 2.
        /// </summary>
        [Display(Name = "Street Address Line 2")]
        public string AddressLine2 { get; set; }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        [Required]
        [Display(Name = "City")]
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        [Display(Name = "State")]
        [Required]
        [StringLength(2, MinimumLength = 2, ErrorMessage = "The state must be exactly 2 characters.")]
        public string State { get; set; }

        /// <summary>
        /// Gets or sets the zip code.
        /// </summary>
        [Required]
        [Display(Name = "Zip Code")]
        [Range(0, int.MaxValue, ErrorMessage = "The zip code must only contain numeric digits.")]
        [StringLength(5, MinimumLength = 5, ErrorMessage = "The zip code must be exactly 5 characters.")]
        public string ZipCode { get; set; }

        /// <summary>
        /// Gets or sets the evening phone number.
        /// </summary>
        /// <remarks>
        /// http://stackoverflow.com/questions/9840337/mvc-validation-for-us-phone-number-000-000-0000-or-000-000-0000
        /// </remarks>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed. Suppression is OK here.")]
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        [Required]
        [Display(Name = "Evening Phone")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "This is not a valid phone number")]
        public string EveningPhone { get; set; }

        /// <summary>
        /// Gets or sets the daytime phone area code.
        /// </summary>
        /// <remarks>
        /// http://stackoverflow.com/questions/9840337/mvc-validation-for-us-phone-number-000-000-0000-or-000-000-0000
        /// </remarks>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed. Suppression is OK here.")]
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        [Required]
        [Display(Name = "Daytime Phone")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "This is not a valid phone number")]
        public string DaytimePhone { get; set; }

        /// <summary>
        /// Gets or sets the mobile phone area code.
        /// </summary>
        /// <remarks>
        /// http://stackoverflow.com/questions/9840337/mvc-validation-for-us-phone-number-000-000-0000-or-000-000-0000
        /// </remarks>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed. Suppression is OK here.")]
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        [Display(Name = "Mobile Phone")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "This is not a valid phone number")]
        public string MobilePhone { get; set; }

        /// <summary>
        /// Gets or sets the e-mail address.
        /// </summary>
        [Required]
        [Display(Name = "E-mail Address")]
        public string EmailAddress { get; set; }

        /// <summary>
        /// Gets or sets the confirmation e-mail address.
        /// </summary>
        [Required]
        [System.ComponentModel.DataAnnotations.Compare("EmailAddress", ErrorMessage = "Your e-mail addresses do not match")]
        [Display(Name = "Confirm E-mail Address")]
        public string EmailConfirmation { get; set; }

        /// <summary>
        /// Gets or sets the user's first problem choice.
        /// </summary>
        [Display(Name = "First Choice")]
        public string ProblemChoice1 { get; set; }

        /// <summary>
        /// Gets or sets the user's second problem choice.
        /// </summary>
        [Display(Name = "Second Choice")]
        public string ProblemChoice2 { get; set; }

        /// <summary>
        /// Gets or sets the user's third problem choice.
        /// </summary>
        [Display(Name = "Third Choice")]
        public string ProblemChoice3 { get; set; }

        /// <summary>
        /// Gets or sets the first conflict of interest problem choice.
        /// </summary>
        [Display(Name = "Child 1")]
        public string ProblemConflict1 { get; set; }

        /// <summary>
        /// Gets or sets the second conflict of interest problem choice.
        /// </summary>
        [Display(Name = "Child 2")]
        public string ProblemConflict2 { get; set; }

        /// <summary>
        /// Gets or sets the third conflict of interest problem choice.
        /// </summary>
        [Display(Name = "Child 3")]
        public string ProblemConflict3 { get; set; }

        /// <summary>
        /// Gets or sets the number of years of long-term judging experience this Odyssey judge has.
        /// </summary>
        [Required]
        [Display(Name = "Years of Long-Term Judging Experience")]
        [Range(0, 100, ErrorMessage = "The Years of Long-Term Judging Experience field must only contain numeric digits and must be no more than 100 years.")]
        public string YearsOfLongTermJudgingExperience { get; set; }

        /// <summary>
        /// Gets or sets the number of years of spontaneous judging experience this Odyssey judge has.
        /// </summary>
        [Required]
        [Display(Name = "Years of Spontaneous Judging Experience")]
        [Range(0, 100, ErrorMessage = "The Years of Spontaneous Judging Experience field must only contain numeric digits and must be no more than 100 years.")]
        public string YearsOfSpontaneousJudgingExperience { get; set; }

        /// <summary>
        /// Gets or sets the notes field's contents.
        /// </summary>
        [StringLength(500, ErrorMessage = "The notes field cannot contain more than 500 characters.")]
        public string Notes { get; set; }

        [Required(ErrorMessage = "You must choose whether or not you have children competing in our Regional Tournament.")]
        public string HasChildrenCompeting { get; set; }

        [Display(Name = "Head Judge")]
        public bool PreviouslyHeadJudge { get; set; }

        [Display(Name = "Problem Judge")]
        public bool PreviouslyProblemJudge { get; set; }

        [Display(Name = "Scorechecker")]
        public bool PreviouslyScorechecker { get; set; }

        [Display(Name = "Staging Judge")]
        public bool PreviouslyStagingJudge { get; set; }

        [Display(Name = "Style Judge")]
        public bool PreviouslyStyleJudge { get; set; }

        [Display(Name = "Timekeeper")]
        public bool PreviouslyTimekeeper { get; set; }

        [Display(Name = "Weigh-In Judge")]
        public bool PreviouslyWeighInJudge { get; set; }

        /// <summary>
        /// Gets or sets the t-shirt sizes.
        /// </summary>
        [Required]
        [Display(Name = "T-Shirt Size")]
        public string TshirtSize { get; set; }

        public IEnumerable<SelectListItem> TshirtSizes { get; set; }

        /// <summary>
        /// Gets or sets the problem choices.
        /// </summary>
        public IEnumerable<SelectListItem> ProblemChoices { get; set; }

        /// <summary>
        /// Gets the problem conflict list for the first child.
        /// </summary>
        public IEnumerable<SelectListItem> ProblemConflictList1
        {
            get
            {
                return this.ProblemConflictListCommon;
            }
        }

        /// <summary>
        /// Gets the problem conflict list for the second child.
        /// </summary>
        public IEnumerable<SelectListItem> ProblemConflictList2
        {
            get
            {
                return this.ProblemConflictListCommon;
            }
        }

        /// <summary>
        /// Gets the problem conflict list for the third child.
        /// </summary>
        public IEnumerable<SelectListItem> ProblemConflictList3
        {
            get
            {
                return this.ProblemConflictListCommon;
            }
        }

        /// <summary>
        /// Gets the problem conflicts.
        /// </summary>
        private IEnumerable<SelectListItem> ProblemConflictListCommon
        {
            get
            {
                List<SelectListItem> problemConflicts = new List<SelectListItem>(this.ProblemChoices);
                SelectListItem foundItem = problemConflicts.Find(sli => sli.Text == "No Preference");

                if (foundItem != null)
                {
                    foundItem.Text = "I Don't Know";
                }

                return problemConflicts;
            }
        }

        [Required(ErrorMessage = "You must choose whether or not you want CEU credit.")]
        public string WantsCeuCredit { get; set; }

        [Required(ErrorMessage = "You must choose whether or not you are willing to be a scorechecker.")]
        public string WillingToBeScorechecker { get; set; }
    }
}
