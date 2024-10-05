// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseViewData.cs" company="Tardis Technologies">
//   Copyright 2014 Tardis Technologies. All rights reserved.
// </copyright>
// <summary>
//   The base ViewData class for all other ViewData classes.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

// Decompiled with JetBrains decompiler
// Type: OdysseyMvc4.ViewData.BaseViewData
// Assembly: OdysseyMvc4, Version=1.0.5460.36587, Culture=neutral, PublicKeyToken=null
// MVID: 7B658547-521F-44CB-80FA-52857CB94B72
// Assembly location: C:\Users\rob\OneDrive\Odyssey\OdysseyProd\registration\bin\OdysseyMvc4.dll

using System.Collections.Generic;
using OdysseyMvc2024.Models;

/// <summary>
/// The base ViewData class for all other ViewData classes.
/// </summary>
namespace OdysseyMvc2024.ViewData
{
    public class BaseViewData(IOdysseyRepository repository)
    {

        /// <summary>
        /// Gets or sets the general configuration data for all registration types.
        /// </summary>
        public required Dictionary<string, string> Config { get; set; } = repository.Config;

        /// <summary>
        /// Gets or sets the displayable registration name, e.g., "Tournament Registration" or "Judges Registration".
        /// </summary>
        public string? FriendlyRegistrationName { get; set; }

        /// <summary>
        /// Gets or sets the path to the web site's CSS file based on which server this is hosted on, NoVA North or
        /// NoVA South.
        /// </summary>
        public string? PathToSiteCssFile { get; set; }

        /// <summary>
        /// Gets or sets the Odyssey of the Mind region name within Virginia, e.g., "NoVA North".
        /// </summary>
        public string? RegionName { get; set; }

        /// <summary>
        /// Gets or sets the Odyssey of the Mind region number within the state of Virginia, e.g., 9.
        /// </summary>
        public string? RegionNumber { get; set; }

        public string? SiteName { get; set; }

        public string? TournamentDate => this.TournamentInfo.StartDate.HasValue
            ? this.TournamentInfo.StartDate.Value.ToLongDateString()
            : "TBA";

        public required Event TournamentInfo { get; set; } = repository.TournamentInfo;

        public string? TournamentLocation => !string.IsNullOrWhiteSpace(this.TournamentInfo.Location)
            ? this.TournamentInfo.Location
            : "TBA";

        public string? TournamentTime => !string.IsNullOrWhiteSpace(this.TournamentInfo.Time)
            ? this.TournamentInfo.Time
            : "TBA";
    }
}
