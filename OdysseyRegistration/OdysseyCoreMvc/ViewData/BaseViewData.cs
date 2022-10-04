// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseViewData.cs" company="Tardis Technologies">
//   Copyright 2022 Tardis Technologies. All rights reserved.
// </copyright>
// <summary>
//   The base ViewData class for all other ViewData classes.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using OdysseyCoreMvc.Models;

namespace OdysseyCoreMvc.ViewData
{
    /// <summary>
    /// The base ViewData class for all other ViewData classes.
    /// </summary>
    public class BaseViewData
    {
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
                    return this.TournamentInfo.StartDate.HasValue ? this.TournamentInfo.StartDate.Value.ToLongDateString() : "TBA";
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
    }
}
