﻿namespace OdysseyMvc4.ViewData.VolunteerRegistration
{
    using OdysseyMvc4.ViewData;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.CompilerServices;
    using System.Web.Mvc;

    public class Page02ViewData : BaseViewData
    {
        [Display(Name="Daytime Phone"), DataType(DataType.PhoneNumber), Required, RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage="This is not a valid phone number")]
        public string DaytimePhone { get; set; }

        [Required, Display(Name="E-mail Address")]
        public string EmailAddress { get; set; }

        [System.Web.Mvc.Compare("EmailAddress", ErrorMessage="Your e-mail addresses do not match"), Display(Name="Confirm E-mail Address"), Required]
        public string EmailConfirmation { get; set; }

        [Required, DataType(DataType.PhoneNumber), Display(Name="Evening Phone"), RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage="This is not a valid phone number")]
        public string EveningPhone { get; set; }

        [Required, Display(Name="First Name")]
        public string FirstName { get; set; }

        [Required, Display(Name="Last Name")]
        public string LastName { get; set; }

        [DataType(DataType.PhoneNumber), Display(Name="Mobile Phone"), Required, RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage="This is not a valid phone number")]
        public string MobilePhone { get; set; }

        [StringLength(500, ErrorMessage="The notes field cannot contain more than 500 characters.")]
        public string Notes { get; set; }

        public string VolunteerWantsToSee { get; set; }
    }
}

