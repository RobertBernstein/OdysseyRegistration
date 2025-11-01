// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Page06ViewData.cs" company="Tardis Technologies">
//   Copyright 2014 Tardis Technologies. All rights reserved.
// </copyright>
// <summary>
//   Defines the Page06ViewData type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

// Decompiled with JetBrains decompiler
// Type: OdysseyMvc4.ViewData.TournamentRegistration.Page06ViewData
// Assembly: OdysseyMvc4, Version=1.0.5460.36587, Culture=neutral, PublicKeyToken=null
// MVID: 7B658547-521F-44CB-80FA-52857CB94B72
// Assembly location: C:\Users\rob\OneDrive\Odyssey\OdysseyProd\registration\bin\OdysseyMvc4.dll

using Microsoft.AspNetCore.Mvc.Rendering;
using OdysseyMvc2024.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OdysseyMvc2024.ViewData.TournamentRegistration
{
    /// <summary>
    /// Backing data for Page 06 of the Tournament Registration wizard.
    /// </summary>
    public class Page06ViewData : BaseViewData
    {
        /// <summary>
        /// Gets or sets the grade choices.
        /// </summary>
        public required IEnumerable<SelectListItem> GradeChoices { get; set; }

        /// <summary>
        /// Gets or sets the member first name 1.
        /// </summary>
        [Required(ErrorMessage = "You must include at least Team Member 1 to proceed.  Make sure the first name field is filled in.")]
        [Display(Name = "First Name")]
        [StringLength(25, ErrorMessage = "The team member's first name must not be more than 25 characters.")]
        public string? MemberFirstName1 { get; set; }

        /// <summary>
        /// Gets or sets the member first name 2.
        /// </summary>
        [Display(Name = "First Name")]
        [StringLength(25, ErrorMessage = "The team member's first name must not be more than 25 characters.")]
        public string? MemberFirstName2 { get; set; }

        [Display(Name = "First Name")]
        [StringLength(25, ErrorMessage = "The team member's first name must not be more than 25 characters.")]
        public string? MemberFirstName3 { get; set; }

        [Display(Name = "First Name")]
        [StringLength(25, ErrorMessage = "The team member's first name must not be more than 25 characters.")]
        public string? MemberFirstName4 { get; set; }

        [Display(Name = "First Name")]
        [StringLength(25, ErrorMessage = "The team member's first name must not be more than 25 characters.")]
        public string? MemberFirstName5 { get; set; }

        [Display(Name = "First Name")]
        [StringLength(25, ErrorMessage = "The team member's first name must not be more than 25 characters.")]
        public string? MemberFirstName6 { get; set; }

        [Display(Name = "First Name")]
        [StringLength(25, ErrorMessage = "The team member's first name must not be more than 25 characters.")]
        public string? MemberFirstName7 { get; set; }

        [Display(Name = "Grade")]
        [Required(ErrorMessage = "Please select the team member's grade.")]
        public string? MemberGrade1 { get; set; }

        [Display(Name = "Grade")]
        public string? MemberGrade2 { get; set; }

        [Display(Name = "Grade")]
        public string? MemberGrade3 { get; set; }

        [Display(Name = "Grade")]
        public string? MemberGrade4 { get; set; }

        [Display(Name = "Grade")]
        public string? MemberGrade5 { get; set; }

        [Display(Name = "Grade")]
        public string? MemberGrade6 { get; set; }

        [Display(Name = "Grade")]
        public string? MemberGrade7 { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [StringLength(25, ErrorMessage = "The team member's last name must not be more than 25 characters.")]
        public string? MemberLastName1 { get; set; }

        [Display(Name = "Last Name")]
        [StringLength(25, ErrorMessage = "The team member's last name must not be more than 25 characters.")]
        public string? MemberLastName2 { get; set; }

        [Display(Name = "Last Name")]
        [StringLength(25, ErrorMessage = "The team member's last name must not be more than 25 characters.")]
        public string? MemberLastName3 { get; set; }

        [Display(Name = "Last Name")]
        [StringLength(25, ErrorMessage = "The team member's last name must not be more than 25 characters.")]
        public string? MemberLastName4 { get; set; }

        [Display(Name = "Last Name")]
        [StringLength(25, ErrorMessage = "The team member's last name must not be more than 25 characters.")]
        public string? MemberLastName5 { get; set; }

        [Display(Name = "Last Name")]
        [StringLength(25, ErrorMessage = "The team member's last name must not be more than 25 characters.")]
        public string? MemberLastName6 { get; set; }

        [Display(Name = "Last Name")]
        [StringLength(25, ErrorMessage = "The team member's last name must not be more than 25 characters.")]
        public string? MemberLastName7 { get; set; }
    }
}
