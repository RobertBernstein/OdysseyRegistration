// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Page03ViewData.cs" company="Tardis Technologies">
//   Copyright 2014 Tardis Technologies. All rights reserved.
// </copyright>
// <summary>
//   Defines the Page03ViewData type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

// Decompiled with JetBrains decompiler
// Type: OdysseyMvc4.ViewData.TournamentRegistration.Page03ViewData
// Assembly: OdysseyMvc4, Version=1.0.5460.36587, Culture=neutral, PublicKeyToken=null
// MVID: 7B658547-521F-44CB-80FA-52857CB94B72
// Assembly location: C:\Users\rob\OneDrive\Odyssey\OdysseyProd\registration\bin\OdysseyMvc4.dll

using System.ComponentModel.DataAnnotations;
using System.Linq;
using OdysseyMvc2024.Models;

namespace OdysseyMvc2024.ViewData.TournamentRegistration
{
    /// <summary>
    /// The backing data for Page 03 of the Tournament Registration wizard.
    /// </summary>
    public class Page03ViewData : BaseViewData
    {
        /// <summary>
        /// Gets or sets a value indicating whether the judge has already been assigned to a team.
        /// </summary>
        public bool JudgeAlreadyTaken { get; set; }

        /// <summary>
        /// Gets or sets the judge's first name.
        /// </summary>
        [Required]
        [Display(Name = "Judge's First Name")]
        [StringLength(25, ErrorMessage = "The Judge's first name must not be more than 25 characters.")]
        public string JudgeFirstName { get; set; }

        /// <summary>
        /// Gets or sets the judge id.
        /// </summary>
        [Required]
        [Display(Name = "Judge's ID Number")]
        [Range(0, int.MaxValue, ErrorMessage = "The Judge's ID must only contain numeric digits.")]
        [StringLength(4, ErrorMessage = "The Judge's ID must not be more than 3 digits.")]
        public string JudgeId { get; set; }

        /// <summary>
        /// Gets or sets the judge last name.
        /// </summary>
        [Required]
        [Display(Name = "Judge's Last Name")]
        [StringLength(25, ErrorMessage = "The Judge's last name must not be more than 25 characters.")]
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
