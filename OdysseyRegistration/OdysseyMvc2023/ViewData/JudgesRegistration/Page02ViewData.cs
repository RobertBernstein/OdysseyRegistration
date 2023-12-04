// Decompiled with JetBrains decompiler
// Type: OdysseyMvc4.ViewData.JudgesRegistration.Page02ViewData
// Assembly: OdysseyMvc4, Version=1.0.5460.36587, Culture=neutral, PublicKeyToken=null
// MVID: 7B658547-521F-44CB-80FA-52857CB94B72
// Assembly location: C:\Users\rob\OneDrive\Odyssey\OdysseyProd\registration\bin\OdysseyMvc4.dll

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace OdysseyMvc2023.ViewData.JudgesRegistration
{
    public class Page02ViewData : BaseViewData
    {
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required]
        public string LastName { get; set; }

        [Display(Name = "Street Address")]
        [Required]
        public string Address { get; set; }

        [Display(Name = "Street Address Line 2")]
        public string AddressLine2 { get; set; }

        [Display(Name = "City")]
        [Required]
        public string City { get; set; }

        [Display(Name = "State")]
        [StringLength(2, MinimumLength = 2, ErrorMessage = "The state must be exactly 2 characters.")]
        [Required]
        public string State { get; set; }

        [Required]
        [Range(0, 2147483647, ErrorMessage = "The zip code must only contain numeric digits.")]
        [StringLength(5, MinimumLength = 5, ErrorMessage = "The zip code must be exactly 5 characters.")]
        [Display(Name = "Zip Code")]
        public string ZipCode { get; set; }

        [Required]
        [RegularExpression("^\\(?([0-9]{3})\\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "This is not a valid phone number")]
        [Display(Name = "Evening Phone")]
        [DataType(DataType.PhoneNumber)]
        public string EveningPhone { get; set; }

        [Required]
        [Display(Name = "Daytime Phone")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression("^\\(?([0-9]{3})\\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "This is not a valid phone number")]
        public string DaytimePhone { get; set; }

        [RegularExpression("^\\(?([0-9]{3})\\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "This is not a valid phone number")]
        [Display(Name = "Mobile Phone")]
        [DataType(DataType.PhoneNumber)]
        public string MobilePhone { get; set; }

        [Display(Name = "E-mail Address")]
        [Required]
        public string EmailAddress { get; set; }

        [System.Web.Mvc.Compare("EmailAddress", ErrorMessage = "Your e-mail addresses do not match")]
        [Display(Name = "Confirm E-mail Address")]
        [Required]
        public string EmailConfirmation { get; set; }

        [Display(Name = "First Choice")]
        public string ProblemChoice1 { get; set; }

        [Display(Name = "Second Choice")]
        public string ProblemChoice2 { get; set; }

        [Display(Name = "Third Choice")]
        public string ProblemChoice3 { get; set; }

        [Display(Name = "Child 1")]
        public string ProblemConflict1 { get; set; }

        [Display(Name = "Child 2")]
        public string ProblemConflict2 { get; set; }

        [Display(Name = "Child 3")]
        public string ProblemConflict3 { get; set; }

        [Range(0, 100, ErrorMessage = "The Years of Long-Term Judging Experience field must only contain numeric digits and must be no more than 100 years.")]
        [Required]
        [Display(Name = "Years of Long-Term Judging Experience")]
        public string YearsOfLongTermJudgingExperience { get; set; }

        [Range(0, 100, ErrorMessage = "The Years of Spontaneous Judging Experience field must only contain numeric digits and must be no more than 100 years.")]
        [Required]
        [Display(Name = "Years of Spontaneous Judging Experience")]
        public string YearsOfSpontaneousJudgingExperience { get; set; }

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

        [Required]
        [Display(Name = "T-Shirt Size")]
        public string TshirtSize { get; set; }

        public IEnumerable<SelectListItem> TshirtSizes { get; set; }

        public IEnumerable<SelectListItem> ProblemChoices { get; set; }

        public IEnumerable<SelectListItem> ProblemConflictList1 => this.ProblemConflictListCommon;

        public IEnumerable<SelectListItem> ProblemConflictList2 => this.ProblemConflictListCommon;

        public IEnumerable<SelectListItem> ProblemConflictList3 => this.ProblemConflictListCommon;

        private IEnumerable<SelectListItem> ProblemConflictListCommon
        {
            get
            {
                List<SelectListItem> conflictListCommon = new List<SelectListItem>(this.ProblemChoices);
                SelectListItem selectListItem = conflictListCommon.Find((Predicate<SelectListItem>)(sli => sli.Text == "No Preference"));
                if (selectListItem != null)
                    selectListItem.Text = "I Don't Know";
                return (IEnumerable<SelectListItem>)conflictListCommon;
            }
        }

        [Required(ErrorMessage = "You must choose whether or not you want CEU credit.")]
        public string WantsCeuCredit { get; set; }

        [Required(ErrorMessage = "You must choose whether or not you are willing to be a scorechecker.")]
        public string WillingToBeScorechecker { get; set; }
    }
}
