// Decompiled with JetBrains decompiler
// Type: OdysseyMvc4.ViewData.TournamentRegistration.Page06ViewData
// Assembly: OdysseyMvc4, Version=1.0.5460.36587, Culture=neutral, PublicKeyToken=null
// MVID: 7B658547-521F-44CB-80FA-52857CB94B72
// Assembly location: C:\Users\rob\OneDrive\Odyssey\OdysseyProd\registration\bin\OdysseyMvc4.dll

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace OdysseyMvc2023.ViewData.TournamentRegistration
{
    public class Page06ViewData : BaseViewData
    {
        public IEnumerable<SelectListItem> GradeChoices { get; set; }

        [Required(ErrorMessage = "You must include at least Team Member 1 to proceed.  Make sure the first name field is filled in.")]
        [Display(Name = "First Name")]
        [StringLength(25, ErrorMessage = "The team member's first name must not be more than 25 characters.")]
        public string MemberFirstName1 { get; set; }

        [StringLength(25, ErrorMessage = "The team member's first name must not be more than 25 characters.")]
        [Display(Name = "First Name")]
        public string MemberFirstName2 { get; set; }

        [Display(Name = "First Name")]
        [StringLength(25, ErrorMessage = "The team member's first name must not be more than 25 characters.")]
        public string MemberFirstName3 { get; set; }

        [Display(Name = "First Name")]
        [StringLength(25, ErrorMessage = "The team member's first name must not be more than 25 characters.")]
        public string MemberFirstName4 { get; set; }

        [Display(Name = "First Name")]
        [StringLength(25, ErrorMessage = "The team member's first name must not be more than 25 characters.")]
        public string MemberFirstName5 { get; set; }

        [Display(Name = "First Name")]
        [StringLength(25, ErrorMessage = "The team member's first name must not be more than 25 characters.")]
        public string MemberFirstName6 { get; set; }

        [Display(Name = "First Name")]
        [StringLength(25, ErrorMessage = "The team member's first name must not be more than 25 characters.")]
        public string MemberFirstName7 { get; set; }

        [Required(ErrorMessage = "Please select the team member's grade.")]
        [Display(Name = "Grade")]
        public string MemberGrade1 { get; set; }

        [Display(Name = "Grade")]
        public string MemberGrade2 { get; set; }

        [Display(Name = "Grade")]
        public string MemberGrade3 { get; set; }

        [Display(Name = "Grade")]
        public string MemberGrade4 { get; set; }

        [Display(Name = "Grade")]
        public string MemberGrade5 { get; set; }

        [Display(Name = "Grade")]
        public string MemberGrade6 { get; set; }

        [Display(Name = "Grade")]
        public string MemberGrade7 { get; set; }

        [Required]
        [StringLength(25, ErrorMessage = "The team member's last name must not be more than 25 characters.")]
        [Display(Name = "Last Name")]
        public string MemberLastName1 { get; set; }

        [StringLength(25, ErrorMessage = "The team member's last name must not be more than 25 characters.")]
        [Display(Name = "Last Name")]
        public string MemberLastName2 { get; set; }

        [StringLength(25, ErrorMessage = "The team member's last name must not be more than 25 characters.")]
        [Display(Name = "Last Name")]
        public string MemberLastName3 { get; set; }

        [Display(Name = "Last Name")]
        [StringLength(25, ErrorMessage = "The team member's last name must not be more than 25 characters.")]
        public string MemberLastName4 { get; set; }

        [StringLength(25, ErrorMessage = "The team member's last name must not be more than 25 characters.")]
        [Display(Name = "Last Name")]
        public string MemberLastName5 { get; set; }

        [StringLength(25, ErrorMessage = "The team member's last name must not be more than 25 characters.")]
        [Display(Name = "Last Name")]
        public string MemberLastName6 { get; set; }

        [Display(Name = "Last Name")]
        [StringLength(25, ErrorMessage = "The team member's last name must not be more than 25 characters.")]
        public string MemberLastName7 { get; set; }
    }
}
