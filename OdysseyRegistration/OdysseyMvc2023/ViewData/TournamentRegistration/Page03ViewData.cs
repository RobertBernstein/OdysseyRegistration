// Decompiled with JetBrains decompiler
// Type: OdysseyMvc4.ViewData.TournamentRegistration.Page03ViewData
// Assembly: OdysseyMvc4, Version=1.0.5460.36587, Culture=neutral, PublicKeyToken=null
// MVID: 7B658547-521F-44CB-80FA-52857CB94B72
// Assembly location: C:\Users\rob\OneDrive\Odyssey\OdysseyProd\registration\bin\OdysseyMvc4.dll

using System.ComponentModel.DataAnnotations;
using System.Linq;
using OdysseyMvc2023.Models;

namespace OdysseyMvc2023.ViewData.TournamentRegistration
{
    public class Page03ViewData : BaseViewData
    {
        public bool JudgeAlreadyTaken { get; set; }

        [Display(Name = "Judge's First Name")]
        [StringLength(25, ErrorMessage = "The Judge's first name must not be more than 25 characters.")]
        [Required]
        public string JudgeFirstName { get; set; }

        [Display(Name = "Judge's ID Number")]
        [StringLength(4, ErrorMessage = "The Judge's ID must not be more than 3 digits.")]
        [Range(0, 2147483647, ErrorMessage = "The Judge's ID must only contain numeric digits.")]
        [Required]
        public string JudgeId { get; set; }

        [Required]
        [Display(Name = "Judge's Last Name")]
        [StringLength(25, ErrorMessage = "The Judge's last name must not be more than 25 characters.")]
        public string JudgeLastName { get; set; }

        public IQueryable<Judge> ListOfJudgesFound { get; set; }

        public bool NoJudgesFound { get; set; }
    }
}
