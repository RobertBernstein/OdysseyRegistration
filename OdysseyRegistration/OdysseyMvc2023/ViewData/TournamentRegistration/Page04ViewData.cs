// Decompiled with JetBrains decompiler
// Type: OdysseyMvc4.ViewData.TournamentRegistration.Page04ViewData
// Assembly: OdysseyMvc4, Version=1.0.5460.36587, Culture=neutral, PublicKeyToken=null
// MVID: 7B658547-521F-44CB-80FA-52857CB94B72
// Assembly location: C:\Users\rob\OneDrive\Odyssey\OdysseyProd\registration\bin\OdysseyMvc4.dll

using System.ComponentModel.DataAnnotations;

namespace OdysseyMvc2023.ViewData.TournamentRegistration
{
    public class Page04ViewData : BaseViewData
    {
        public bool NoVolunteersFound { get; set; }

        public bool VolunteerAlreadyTaken { get; set; }

        [StringLength(25, ErrorMessage = "The Volunteer's first name must not be more than 25 characters.")]
        [Required]
        [Display(Name = "Volunteer's First Name")]
        public string VolunteerFirstName { get; set; }

        //public Volunteer VolunteerFound { get; set; }

        [Display(Name = "Volunteer's ID Number")]
        [Range(0, 2147483647, ErrorMessage = "The Volunteer's ID must only contain numeric digits.")]
        [StringLength(4, ErrorMessage = "The Volunteer's ID must not be more than 3 digits.")]
        [Required]
        public string VolunteerId { get; set; }

        [Required]
        [StringLength(25, ErrorMessage = "The Volunteer's last name must not be more than 25 characters.")]
        [Display(Name = "Volunteer's Last Name")]
        public string VolunteerLastName { get; set; }
    }
}
