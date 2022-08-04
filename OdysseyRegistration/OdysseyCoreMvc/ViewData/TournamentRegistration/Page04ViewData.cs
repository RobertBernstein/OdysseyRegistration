// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Page04ViewData.cs" company="Tardis Technologies">
//   Copyright 2022 Tardis Technologies. All rights reserved.
// </copyright>
// <summary>
//   Defines the Page04ViewData type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using OdysseyCoreMvc.Models;
using System.ComponentModel.DataAnnotations;

namespace OdysseyCoreMvc.ViewData.TournamentRegistration
{
    public class Page04ViewData : BaseViewData
    {
        public bool NoVolunteersFound { get; set; }

        public bool VolunteerAlreadyTaken { get; set; }

        [StringLength(0x19, ErrorMessage="The Volunteer's first name must not be more than 25 characters."), Required, Display(Name="Volunteer's First Name")]
        public string? VolunteerFirstName { get; set; }

        public Volunteer? VolunteerFound { get; set; }

        [Range(0, 0x7fffffff, ErrorMessage="The Volunteer's ID must only contain numeric digits."), StringLength(4, ErrorMessage="The Volunteer's ID must not be more than 3 digits."), Required, Display(Name="Volunteer's ID Number")]
        public string? VolunteerId { get; set; }

        [Required, StringLength(0x19, ErrorMessage="The Volunteer's last name must not be more than 25 characters."), Display(Name="Volunteer's Last Name")]
        public string? VolunteerLastName { get; set; }
    }
}

