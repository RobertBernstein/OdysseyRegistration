// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Page03ViewData.cs" company="Tardis Technologies">
//   Copyright 2014 Tardis Technologies. All rights reserved.
// </copyright>
// <summary>
//   Defines the Page03ViewData type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OdysseyMvc4.ViewData.TournamentRegistration
{
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    using OdysseyMvc4.Models;
    using OdysseyMvc4.ViewData;

    /// <summary>
    /// The page 03 view data.
    /// </summary>
    public class Page03ViewData : BaseViewData
    {
        /// <summary>
        /// Gets or sets a value indicating whether judge already taken.
        /// </summary>
        public bool JudgeAlreadyTaken { get; set; }

        /// <summary>
        /// Gets or sets the judge first name.
        /// </summary>
        [Display(Name = "Judge's First Name")]
        [StringLength(0x19, ErrorMessage = "The Judge's first name must not be more than 25 characters.")]
        [Required]
        public string JudgeFirstName { get; set; }

        /// <summary>
        /// Gets or sets the judge id.
        /// </summary>
        [StringLength(4, ErrorMessage = "The Judge's ID must not be more than 3 digits.")]
        [Required]
        [Display(Name = "Judge's ID Number")]
        [Range(0, 0x7fffffff, ErrorMessage = "The Judge's ID must only contain numeric digits.")]
        public string JudgeId { get; set; }

        /// <summary>
        /// Gets or sets the judge last name.
        /// </summary>
        [Required]
        [Display(Name = "Judge's Last Name")]
        [StringLength(0x19, ErrorMessage = "The Judge's last name must not be more than 25 characters.")]
        public string JudgeLastName { get; set; }

        /// <summary>
        /// Gets or sets the list of judges found.
        /// </summary>
        public IQueryable<Judge> ListOfJudgesFound { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether no judges found.
        /// </summary>
        public bool NoJudgesFound { get; set; }
    }
}
