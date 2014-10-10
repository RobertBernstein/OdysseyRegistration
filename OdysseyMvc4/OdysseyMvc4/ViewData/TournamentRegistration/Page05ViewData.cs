namespace OdysseyMvc4.ViewData.TournamentRegistration
{
    using OdysseyMvc4.ViewData;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.CompilerServices;
    using System.Web.Mvc;

    public class Page05ViewData : BaseViewData
    {
        [Display(Name="Alternate Coach's Daytime Phone"), Required, DataType(DataType.PhoneNumber), RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage="This is not a valid phone number")]
        public string AltCoachDaytimePhone { get; set; }

        [Required, Display(Name="Alternate Coach's E-mail Address")]
        public string AltCoachEmailAddress { get; set; }

        [System.Web.Mvc.Compare("AltCoachEmailAddress", ErrorMessage="Your e-mail addresses do not match"), Display(Name="Confirm Alternate Coach's E-mail Address"), Required]
        public string AltCoachEmailConfirmation { get; set; }

        [Display(Name="Alternate Coach's Evening Phone"), DataType(DataType.PhoneNumber), RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage="This is not a valid phone number"), Required]
        public string AltCoachEveningPhone { get; set; }

        [StringLength(0x19, ErrorMessage="The Alternate Coach's first name must not be more than 25 characters."), Display(Name="Alternate Coach's First Name"), Required]
        public string AltCoachFirstName { get; set; }

        [StringLength(0x19, ErrorMessage="The Alternate Coach's last name must not be more than 25 characters."), Display(Name="Alternate Coach's Last Name"), Required]
        public string AltCoachLastName { get; set; }

        [Display(Name="Alternate Coach's Mobile Phone"), DataType(DataType.PhoneNumber), RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage="This is not a valid phone number"), Required]
        public string AltCoachMobilePhone { get; set; }

        [Required, Display(Name="Street Address"), StringLength(0x23, ErrorMessage="The Coach's street address must not be more than 35 characters.")]
        public string CoachAddress { get; set; }

        [StringLength(0x23, ErrorMessage="The Coach's city must not be more than 35 characters."), Required, Display(Name="City")]
        public string CoachCity { get; set; }

        [Required, DataType(DataType.PhoneNumber), RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage="This is not a valid phone number"), Display(Name="Daytime Phone")]
        public string CoachDaytimePhone { get; set; }

        [Required, Display(Name="E-mail Address")]
        public string CoachEmailAddress { get; set; }

        [System.Web.Mvc.Compare("CoachEmailAddress", ErrorMessage="Your e-mail addresses do not match"), Display(Name="Confirm E-mail Address"), Required]
        public string CoachEmailConfirmation { get; set; }

        [Display(Name="Evening Phone"), RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage="This is not a valid phone number"), Required, DataType(DataType.PhoneNumber)]
        public string CoachEveningPhone { get; set; }

        [StringLength(0x19, ErrorMessage="The Coach's first name must not be more than 25 characters."), Display(Name="Coach First Name"), Required]
        public string CoachFirstName { get; set; }

        [StringLength(0x19, ErrorMessage="The Coach's last name must not be more than 25 characters."), Required, Display(Name="Coach Last Name")]
        public string CoachLastName { get; set; }

        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage="This is not a valid phone number"), Required, Display(Name="Mobile Phone"), DataType(DataType.PhoneNumber)]
        public string CoachMobilePhone { get; set; }

        [StringLength(2, MinimumLength=2, ErrorMessage="The state must be exactly 2 characters."), Required, Display(Name="State")]
        public string CoachState { get; set; }

        [StringLength(5, MinimumLength=5, ErrorMessage="The zip code must be exactly 5 characters."), Display(Name="Zip Code"), Range(0, 0x7fffffff, ErrorMessage="The zip code must only contain numeric digits."), Required]
        public string CoachZipCode { get; set; }
    }
}

