// Decompiled with JetBrains decompiler
// Type: OdysseyMvc4.ViewData.TournamentRegistration.Page05ViewData
// Assembly: OdysseyMvc4, Version=1.0.5460.36587, Culture=neutral, PublicKeyToken=null
// MVID: 7B658547-521F-44CB-80FA-52857CB94B72
// Assembly location: C:\Users\rob\OneDrive\Odyssey\OdysseyProd\registration\bin\OdysseyMvc4.dll

using System.ComponentModel.DataAnnotations;

namespace OdysseyMvc2023.ViewData.TournamentRegistration
{
    public class Page05ViewData : BaseViewData
    {
        [Required]
        [RegularExpression("^\\(?([0-9]{3})\\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "This is not a valid phone number")]
        [Display(Name = "Alternate Coach's Daytime Phone")]
        [DataType(DataType.PhoneNumber)]
        public string AltCoachDaytimePhone { get; set; }

        [Required]
        [Display(Name = "Alternate Coach's E-mail Address")]
        public string AltCoachEmailAddress { get; set; }

        [System.Web.Mvc.Compare("AltCoachEmailAddress", ErrorMessage = "Your e-mail addresses do not match")]
        [Display(Name = "Confirm Alternate Coach's E-mail Address")]
        [Required]
        public string AltCoachEmailConfirmation { get; set; }

        [Display(Name = "Alternate Coach's Evening Phone")]
        [RegularExpression("^\\(?([0-9]{3})\\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "This is not a valid phone number")]
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string AltCoachEveningPhone { get; set; }

        [StringLength(25, ErrorMessage = "The Alternate Coach's first name must not be more than 25 characters.")]
        [Required]
        [Display(Name = "Alternate Coach's First Name")]
        public string AltCoachFirstName { get; set; }

        [StringLength(25, ErrorMessage = "The Alternate Coach's last name must not be more than 25 characters.")]
        [Display(Name = "Alternate Coach's Last Name")]
        [Required]
        public string AltCoachLastName { get; set; }

        [Required]
        [Display(Name = "Alternate Coach's Mobile Phone")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression("^\\(?([0-9]{3})\\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "This is not a valid phone number")]
        public string AltCoachMobilePhone { get; set; }

        [Display(Name = "Street Address")]
        [StringLength(35, ErrorMessage = "The Coach's street address must not be more than 35 characters.")]
        [Required]
        public string CoachAddress { get; set; }

        [Display(Name = "City")]
        [StringLength(35, ErrorMessage = "The Coach's city must not be more than 35 characters.")]
        [Required]
        public string CoachCity { get; set; }

        [DataType(DataType.PhoneNumber)]
        [RegularExpression("^\\(?([0-9]{3})\\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "This is not a valid phone number")]
        [Display(Name = "Daytime Phone")]
        [Required]
        public string CoachDaytimePhone { get; set; }

        [Display(Name = "E-mail Address")]
        [Required]
        public string CoachEmailAddress { get; set; }

        [Required]
        [Display(Name = "Confirm E-mail Address")]
        [System.Web.Mvc.Compare("CoachEmailAddress", ErrorMessage = "Your e-mail addresses do not match")]
        public string CoachEmailConfirmation { get; set; }

        [Display(Name = "Evening Phone")]
        [RegularExpression("^\\(?([0-9]{3})\\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "This is not a valid phone number")]
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string CoachEveningPhone { get; set; }

        [Display(Name = "Coach First Name")]
        [StringLength(25, ErrorMessage = "The Coach's first name must not be more than 25 characters.")]
        [Required]
        public string CoachFirstName { get; set; }

        [StringLength(25, ErrorMessage = "The Coach's last name must not be more than 25 characters.")]
        [Required]
        [Display(Name = "Coach Last Name")]
        public string CoachLastName { get; set; }

        [DataType(DataType.PhoneNumber)]
        [RegularExpression("^\\(?([0-9]{3})\\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "This is not a valid phone number")]
        [Required]
        [Display(Name = "Mobile Phone")]
        public string CoachMobilePhone { get; set; }

        [Required]
        [Display(Name = "State")]
        [StringLength(2, MinimumLength = 2, ErrorMessage = "The state must be exactly 2 characters.")]
        public string CoachState { get; set; }

        [StringLength(5, MinimumLength = 5, ErrorMessage = "The zip code must be exactly 5 characters.")]
        [Display(Name = "Zip Code")]
        [Range(0, 2147483647, ErrorMessage = "The zip code must only contain numeric digits.")]
        [Required]
        public string CoachZipCode { get; set; }
    }
}
