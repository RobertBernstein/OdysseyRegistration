// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseViewData.cs" company="Tardis Technologies">
//   Copyright 2014 Tardis Technologies. All rights reserved.
// </copyright>
// <summary>
//   The base ViewData class for all other ViewData classes.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OdysseyMvc4.ViewData
{
    using System.Collections.Generic;

    using OdysseyMvc4.Models;

    /// <summary>
    /// The base ViewData class for all other ViewData classes.
    /// </summary>
    public class BaseViewData
    {
        /// <summary>
        /// Gets or sets the general configuration data for all registration types.
        /// </summary>
        public Dictionary<string, string> Config { get; set; }

        /// <summary>
        /// Gets or sets the displayable registration name, e.g. "Tournament
        /// Registration".
        /// </summary>
        public string FriendlyRegistrationName { get; set; }

        /// <summary>
        /// Gets or sets the Odyssey of the Mind region number within the state of
        /// Virginia (e.g. 9).
        /// </summary>
        public string RegionNumber { get; set; }

        /// <summary>
        /// Gets or sets the Odyssey of the Mind region name within Virginia (e.g. "NoVA
        /// North").
        /// </summary>
        public string RegionName { get; set; }

        public string SiteName { get; set; }

        public string TournamentDate
        {
            get
            {
                return this.TournamentInfo.StartDate.HasValue ? this.TournamentInfo.StartDate.Value.ToLongDateString() : "TBA";
            }
        }

        public Event TournamentInfo { get; set; }

        public string TournamentLocation
        {
            get
            {
                return !string.IsNullOrWhiteSpace(this.TournamentInfo.Location) ? this.TournamentInfo.Location : "TBA";
            }
        }

        public string TournamentTime
        {
            get
            {
                return !string.IsNullOrWhiteSpace(this.TournamentInfo.Time) ? this.TournamentInfo.Time : "TBA";
            }
        }
    }
}
