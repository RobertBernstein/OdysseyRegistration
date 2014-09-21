// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseViewData.cs" company="Tardis Technologies">
//   Copyright 2013 Tardis Technologies. All rights reserved.
// </copyright>
// <summary>
//   The base ViewData class for all other ViewData classes.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OdysseyMvc4.ViewData
{
    using System.Collections.Generic;

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
    }
}
