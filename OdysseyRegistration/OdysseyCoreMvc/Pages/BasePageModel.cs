// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BasePageModel.cs" company="Tardis Technologies">
//   Copyright 2022 Tardis Technologies. All rights reserved.
// </copyright>
// <summary>
//   The base ViewData class for all other ViewData classes.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OdysseyCoreMvc.Models;

namespace OdysseyCoreMvc.Pages
{
    /// <summary>
    /// The base ViewData class for all other ViewData classes.
    /// </summary>
    public class BasePageModel : PageModel
    {
        /// <summary>
        /// The registration type.
        /// </summary>
        public enum RegistrationType
        {
            /// <summary>
            /// The default, i.e. no registration type.  This should never be used.
            /// </summary>
            None,

            /// <summary>
            /// Identifies Tournament Registration.
            /// </summary>
            Tournament,

            /// <summary>
            /// Identifies Judges Registration.
            /// </summary>
            Judges,

            /// <summary>
            /// Identifies Coaches Training Registration.
            /// </summary>
            CoachesTraining,

            /// <summary>
            /// Identifies Volunteer Registration.
            /// </summary>
            Volunteer
        }

        protected readonly Data.OdysseyContext _context;

        /// <summary>
        /// Gets or sets the current registration type.
        /// </summary>
        public RegistrationType CurrentRegistrationType { get; set; }

        // The constructor uses dependency injection to add the OdysseyContext to the page.
        // https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-6.0
        public BasePageModel(Data.OdysseyContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets or sets the general configuration data for all registration types.
        /// </summary>
        public Dictionary<string, string>? Config { get; set; }

        /// <summary>
        /// Gets or sets the displayable registration name, e.g. "Tournament
        /// Registration".
        /// </summary>
        public string? FriendlyRegistrationName { get; set; }

        /// <summary>
        /// Gets or sets the path to the web site's CSS file based on which server this is hosted on, NoVA North or
        /// NoVA South.
        /// </summary>
        public string? PathToSiteCssFile { get; set; }

        /// <summary>
        /// Gets or sets the Odyssey of the Mind region name within Virginia (e.g. "NoVA
        /// North").
        /// </summary>
        public string? RegionName { get; set; }

        /// <summary>
        /// Gets or sets the Odyssey of the Mind region number within the state of
        /// Virginia (e.g. 9).
        /// </summary>
        public string? RegionNumber { get; set; }

        public string? SiteName { get; set; }

        public string TournamentDate
        {
            get
            {
                if (TournamentInfo != null)
                {
                    return TournamentInfo.StartDate.HasValue ? TournamentInfo.StartDate.Value.ToLongDateString() : "TBA";
                }
                else
                {
                    // TODO: Add logging.
                    // TODO: Test that this actually works.
                    return "TournamentInfo was null";
                }
            }
        }

        public Events? TournamentInfo { get; set; }

        public string TournamentLocation
        {
            get
            {
                if (TournamentInfo != null)
                {
                    return !string.IsNullOrWhiteSpace(TournamentInfo.Location) ? TournamentInfo.Location : "TBA";
                }
                else
                {
                    // TODO: Add logging.
                    // TODO: Test that this actually works.
                    return "TournamentInfo was null";
                }
            }
        }

        public string TournamentTime
        {
            get
            {
                if (TournamentInfo != null)
                {
                    return !string.IsNullOrWhiteSpace(TournamentInfo.Time) ? TournamentInfo.Time : "TBA";
                }
                else
                {
                    // TODO: Add logging.
                    // TODO: Test that this actually works.
                    return "TournamentInfo was null";
                }
            }
        }

        /// <summary>
        /// Gets the registration type name that is displayed to the user when they browse to the web page, e.g.
        /// Judges, Tournament.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/> displayed to the user for the current registration type when they browse to the
        /// web page.
        /// </returns>
        /// <remarks>
        /// TODO: Write tests for this.
        /// </remarks>
        public string GetDisplayableRegistrationName()
        {
            // Make sure that CurrentRegistrationType has been set before calling this method.
            // TODO: we should probably assert here if CurrentRegistrationType has not been set.
            // TODO: we should definitely log an error here if CurrentRegistrationType has not been set.
            if (CurrentRegistrationType == RegistrationType.None)
            {
                return string.Empty;
            }

            switch (CurrentRegistrationType)
            {
                case RegistrationType.None:
                case RegistrationType.Volunteer:
                    return "Invalid Registration"; ;
                case RegistrationType.CoachesTraining:
                    return "Coaches Training Registration";
                case RegistrationType.Tournament:
                case RegistrationType.Judges:
                default:
                    return CurrentRegistrationType + " Registration";
            }
        }

        public async Task OnGetAsync()
        {
            // If config is null, run the LINQ query, assign the result to Config as a Dictionary, and return the result.
            Config ??= await (from c in _context.Config
                              select c).ToDictionaryAsync(d => d.Name, d => d.Value);
        }
    }
}
