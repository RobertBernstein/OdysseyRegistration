// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Page07ViewData.cs" company="Tardis Technologies">
//   Copyright 2022 Tardis Technologies. All rights reserved.
// </copyright>
// <summary>
//   Defines the Page07ViewData type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Microsoft.AspNetCore.Mvc.Rendering;

namespace OdysseyCoreMvc.ViewData.TournamentRegistration
{
    /// <summary>
    /// The page 07 view data.
    /// </summary>
    public class Page07ViewData : BaseViewData
    {
        /// <summary>
        /// Gets or sets the division 123 and primary list of problems as html list.
        /// </summary>
        public string? Division123AndPrimaryListOfProblemsAsHtmlList { get; set; }

        /// <summary>
        /// Gets or sets the division 123 list of problems as html list.
        /// </summary>
        public string? Division123ListOfProblemsAsHtmlList { get; set; }

        /// <summary>
        /// Gets or sets the division 123 problem choice.
        /// </summary>
        public string? Division123ProblemChoice { get; set; }

        /// <summary>
        /// Gets or sets the division 123 problem drop down.
        /// </summary>
        public IEnumerable<SelectListItem>? Division123ProblemDropDown { get; set; }

        /// <summary>
        /// Gets or sets the division of team.
        /// </summary>
        public int DivisionOfTeam { get; set; }

        /// <summary>
        /// Gets or sets the division radio group.
        /// </summary>
        public string? DivisionRadioGroup { get; set; }

        /// <summary>
        /// Gets or sets the is doing spontaneous.
        /// </summary>
        public string? IsDoingSpontaneous { get; set; }

        /// <summary>
        /// Gets or sets the is doing spontaneous drop down.
        /// </summary>
        public IEnumerable<SelectListItem>? IsDoingSpontaneousDropDown { get; set; }

        /// <summary>
        /// Gets or sets the primary problem name.
        /// </summary>
        public string? PrimaryProblemName { get; set; }

        /// <summary>
        /// Gets or sets the selected problem.
        /// </summary>
        public string? SelectedProblem { get; set; }
    }
}
