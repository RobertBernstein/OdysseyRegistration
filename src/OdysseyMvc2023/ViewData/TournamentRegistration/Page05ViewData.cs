// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Page05ViewData.cs" company="Tardis Technologies">
//   Copyright 2014 Tardis Technologies. All rights reserved.
// </copyright>
// <summary>
//   Defines the Page05ViewData type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

// Decompiled with JetBrains decompiler
// Type: OdysseyMvc4.ViewData.TournamentRegistration.Page05ViewData
// Assembly: OdysseyMvc4, Version=1.0.5460.36587, Culture=neutral, PublicKeyToken=null
// MVID: 7B658547-521F-44CB-80FA-52857CB94B72
// Assembly location: C:\Users\rob\OneDrive\Odyssey\OdysseyProd\registration\bin\OdysseyMvc4.dll

using System.ComponentModel.DataAnnotations;

namespace OdysseyMvc2023.ViewData.TournamentRegistration
{
    /// <summary>
    /// The backing data for Page 05 of the Tournament Registration wizard.
    /// </summary>
    public class Page05ViewData : BaseViewData
    {
        [Required]
        [Display(Name = "Alternate Coach's Daytime Phone")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "This is not a valid phone number")]
        public string AltCoachDaytimePhone { get; set; }

        [Required]
        [Display(Name = "Alternate Coach's E-mail Address")]
        public string AltCoachEmailAddress { get; set; }

        [Required]
        [Display(Name = "Confirm Alternate Coach's E-mail Address")]
        [Compare("AltCoachEmailAddress", ErrorMessage = "Your e-mail addresses do not match")]
        public string AltCoachEmailConfirmation { get; set; }

        [Required]
        [Display(Name = "Alternate Coach's Evening Phone")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "This is not a valid phone number")]
        public string AltCoachEveningPhone { get; set; }

        [Required]
        [Display(Name = "Alternate Coach's First Name")]
        [StringLength(25, ErrorMessage = "The Alternate Coach's first name must not be more than 25 characters.")]
        public string AltCoachFirstName { get; set; }

        [Required]
        [Display(Name = "Alternate Coach's Last Name")]
        [StringLength(25, ErrorMessage = "The Alternate Coach's last name must not be more than 25 characters.")]
        public string AltCoachLastName { get; set; }

        [Required]
        [Display(Name = "Alternate Coach's Mobile Phone")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "This is not a valid phone number")]
        public string AltCoachMobilePhone { get; set; }

        [Required]
        [Display(Name = "Street Address")]
        [StringLength(35, ErrorMessage = "The Coach's street address must not be more than 35 characters.")]
        public string CoachAddress { get; set; }

        [Required]
        [Display(Name = "City")]
        [StringLength(35, ErrorMessage = "The Coach's city must not be more than 35 characters.")]
        public string CoachCity { get; set; }

        [Required]
        [Display(Name = "Daytime Phone")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "This is not a valid phone number")]
        public string CoachDaytimePhone { get; set; }

        [Required]
        [Display(Name = "E-mail Address")]
        public string CoachEmailAddress { get; set; }

        [Required]
        [Display(Name = "Confirm E-mail Address")]
        [Compare("CoachEmailAddress", ErrorMessage = "Your e-mail addresses do not match")]
        public string CoachEmailConfirmation { get; set; }

        [Required]
        [Display(Name = "Evening Phone")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "This is not a valid phone number")]
        public string CoachEveningPhone { get; set; }

        [Required]
        [Display(Name = "Coach First Name")]
        [StringLength(25, ErrorMessage = "The Coach's first name must not be more than 25 characters.")]
        public string CoachFirstName { get; set; }

        [Required]
        [Display(Name = "Coach Last Name")]
        [StringLength(25, ErrorMessage = "The Coach's last name must not be more than 25 characters.")]
        public string CoachLastName { get; set; }

        [Required]
        [Display(Name = "Mobile Phone")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "This is not a valid phone number")]
        public string CoachMobilePhone { get; set; }

        [Required]
        [Display(Name = "State")]
        [StringLength(2, MinimumLength = 2, ErrorMessage = "The state must be exactly 2 characters.")]
        public string CoachState { get; set; }

        [Required]
        [Display(Name = "Zip Code")]
        [StringLength(5, MinimumLength = 5, ErrorMessage = "The zip code must be exactly 5 characters.")]
        [Range(0, int.MaxValue, ErrorMessage = "The zip code must only contain numeric digits.")]
        public string CoachZipCode { get; set; }
    }
}
