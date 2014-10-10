namespace OdysseyMvc4.ViewData.TournamentRegistration
{
    using OdysseyMvc4.ViewData;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class Page03ViewData : BaseViewData
    {
        public bool JudgeAlreadyTaken { get; set; }

        [Display(Name="Judge's First Name"), StringLength(0x19, ErrorMessage="The Judge's first name must not be more than 25 characters."), Required]
        public string JudgeFirstName { get; set; }

        [StringLength(4, ErrorMessage="The Judge's ID must not be more than 3 digits."), Required, Display(Name="Judge's ID Number"), Range(0, 0x7fffffff, ErrorMessage="The Judge's ID must only contain numeric digits.")]
        public string JudgeId { get; set; }

        [Required, Display(Name="Judge's Last Name"), StringLength(0x19, ErrorMessage="The Judge's last name must not be more than 25 characters.")]
        public string JudgeLastName { get; set; }

        public IQueryable<Judge> ListOfJudgesFound { get; set; }

        public bool NoJudgesFound { get; set; }
    }
}

