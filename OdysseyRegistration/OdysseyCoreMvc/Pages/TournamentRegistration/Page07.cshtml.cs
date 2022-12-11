// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Page07.cshtml.cs" company="Tardis Technologies">
//   Copyright 2022 Tardis Technologies. All rights reserved.
// </copyright>
// <summary>
//   Defines the Page07Model type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Microsoft.AspNetCore.Mvc.Rendering;
using OdysseyCoreMvc.Data;
using OdysseyCoreMvc.Pages;

namespace OdysseyCoreMvc.Pages.TournamentRegistration
{
    /// <summary>
    /// The Tournament Registration Page07 page model.
    /// 
    /// Razor Pages are derived from PageModel. By convention, the PageModel derived class is named PageNameModel. For
    /// example, the Index page is named IndexModel.
    /// https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.razorpages.pagemodel
    /// </summary>
    public class Page07Model : BasePageModel
    {
        // The constructor uses dependency injection to add the OdysseyContext and logging to the page.
        // https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-6.0
        public Page07Model(OdysseyContext context, ILogger<BasePageModel> logger) : base(context, logger)
        {
            this.CurrentRegistrationType = RegistrationType.Tournament;
            this.FriendlyRegistrationName = this.GetDisplayableRegistrationName();
        }

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
