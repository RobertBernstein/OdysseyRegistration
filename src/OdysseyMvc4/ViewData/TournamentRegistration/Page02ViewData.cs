// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Page02ViewData.cs" company="Tardis Technologies">
//   Copyright 2021 Tardis Technologies. All rights reserved.
// </copyright>
// <summary>
//   Defines the Page02ViewData type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OdysseyMvc4.ViewData.TournamentRegistration
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using OdysseyMvc4.ViewData;

    /// <summary>
    /// The page 02 view data.
    /// </summary>
    public class Page02ViewData : BaseViewData
    {
        /// <summary>
        /// Gets or sets the school list.
        /// </summary>
        public IEnumerable<SelectListItem> SchoolList { get; set; }

        /// <summary>
        /// Gets or sets the selected school.
        /// </summary>
        [Required(
            ErrorMessage =
                "Please select one of the schools or organizations listed in the \"School/Organization Name\" field.")]
        [Display(Name = "School or Sponsoring Organization Name")]
        public int SelectedSchool { get; set; }
    }
}
