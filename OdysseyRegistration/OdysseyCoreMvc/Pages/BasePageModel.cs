// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BasePageModel.cs" company="Tardis Technologies">
//   Copyright 2022 Tardis Technologies. All rights reserved.
// </copyright>
// <summary>
//   The base ViewData class for all other ViewData classes.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OdysseyCoreMvc.Models;
using System.Globalization;

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

        private readonly ILogger<BasePageModel> _logger;

        // The constructor uses dependency injection to add the OdysseyContext and logging to the page.
        // https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-6.0
        public BasePageModel(Data.OdysseyContext context, ILogger<BasePageModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        private string _siteCssFile = string.Empty;

        public string SiteCssFile
        {
            get
            {
                if (string.IsNullOrEmpty(_siteCssFile))
                {
                    // TODO: Test that this works correctly after removing Request.Uri and Request.Uri.AbsoluteUri.
                    if ((Request.GetEncodedPathAndQuery() != null) && Request.GetEncodedPathAndQuery().ToLowerInvariant().Contains("novasouth"))
                    {
                        _siteCssFile = Url.Content("NovaSouth.css");
                    }
                    else
                    {
                        _siteCssFile = Url.Content("NovaNorth.css");
                    }
                }

                return _siteCssFile;
            }
        }

        /// <summary>
        /// Backing member variable for general configuration data for all registration types.
        /// </summary>
        public Dictionary<string, string>? _config;

        /// <summary>
        /// Gets or sets the general configuration data for all registration types.
        /// </summary>
        public Dictionary<string, string>? Config
        {
            get
            {
                // If config is null, run the LINQ query, assign the result to Config as a Dictionary, and return the result.
                if (_config == null)
                {
                    _config = (from c in _context.Config
                              select c).ToDictionary(d => d.Name, d => d.Value);

                    _config.Add("EndYear", (int.Parse(_config["Year"]) + 1).ToString(CultureInfo.InvariantCulture));
                }

                return _config;
            }
        }

        /// <summary>
        /// Backing member variable for the displayable website name, e.g. "novanorth.org".
        /// </summary>
        public string? _siteName = string.Empty;

        /// <summary>
        /// Gets the displayable website name, e.g. "novanorth.org".
        /// </summary>
        public string? SiteName
        {
            get
            {
                // If _regionName is null, run the LINQ query, assign the result to RegionName, and return the result
                if (string.IsNullOrEmpty(_siteName))
                {
                    if (Config != null)
                    {
                        _regionName = Config["RegionName"];

                        // TODO: Test this.
                        _siteName = (Request.GetEncodedPathAndQuery() != null)
                            ? Request.Host.Host.ToLowerInvariant()
                            : "novanorth.org";

                        // Drop the "www." if present.
                        if (_siteName.StartsWith("www."))
                        {
                            // TODO: Test this replacement of .Substring(4).
                            _siteName = _siteName[4..];
                        }
                    }
                    else
                    {
                        _logger.LogWarning("Config was null and should not have been.");
                    }
                }

                return _siteName;
            }
        }

        /// <summary>
        /// Gets or sets the path to the web site's CSS file based on which server this is hosted on, NoVA North or
        /// NoVA South.
        /// </summary>
        public string? PathToSiteCssFile { get; set; }

        /// <summary>
        /// Backing member variable for the Odyssey of the Mind region name within Virginia (e.g., "NoVA North").
        /// </summary>
        public string? _regionName = string.Empty;

        /// <summary>
        /// Gets or sets the Odyssey of the Mind region name within Virginia (e.g., "NoVA North").
        /// </summary>
        public string? RegionName
        {
            get
            {
                // If _regionName is null, run the LINQ query, assign the result to RegionName, and return the result
                if (string.IsNullOrEmpty(_regionName))
                {
                    if (Config != null)
                    {
                        _regionName = Config["RegionName"];
                    }
                    else
                    {
                        _logger.LogWarning("Config was null and should not have been.");
                    }
                }

                return _regionName;
            }
        }

        /// <summary>
        /// Backing member variable for the Odyssey of the Mind region number within the state of Virginia (e.g. 9).
        /// </summary>
        private string _regionNumber = string.Empty;

        /// <summary>
        /// Gets the Odyssey of the Mind region number within the state of Virginia (e.g. 9).
        /// </summary>
        public string RegionNumber
        {
            get
            {
                if (string.IsNullOrEmpty(_regionNumber))
                {
                    if (Config != null)
                    {
                        _regionNumber ??= Config["RegionNumber"];
                    }
                    else
                    {
                        _logger.LogWarning("Config was null and should not have been.");
                    }
                }

                return _regionNumber;
            }
        }

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

        /// <summary>
        /// Backing member variable for information about the tournament.
        /// </summary>
        public Events? _tournamentInfo = null;

        /// <summary>
        /// Gets information about the tournament.
        /// </summary>
        public Events? TournamentInfo
        {
            get
            {
                if (string.IsNullOrEmpty(_regionNumber))
                {
                    if (Config != null && !string.IsNullOrEmpty(RegionName))
                    {
                        _tournamentInfo = (from o in _context.Events
                                           where o.EventName.StartsWith(RegionName) && o.EventName.Contains("Tournament")
                                           select o).First();
                    }
                    else
                    {
                        _logger.LogWarning("Config was null or RegionName was null or empty; neither should have been.");
                    }
                }

                return _tournamentInfo;
            }
        }

        /// <summary>
        /// Backing member variable for the physical location where the tournament is being held.
        /// </summary>
        public string _tournamentLocation = string.Empty;

        /// <summary>
        /// Gets the physical location where the tournament is being held.
        /// </summary>
        public string TournamentLocation
        {
            get
            {
                // Only set the value the first time through.
                if (TournamentInfo != null && string.IsNullOrEmpty(_tournamentLocation))
                {
                    _tournamentLocation =
                        (!string.IsNullOrWhiteSpace(TournamentInfo.Location)
                        ? TournamentInfo.Location
                        : "TBA");
                }
                else
                {
                    // TODO: Test that this actually works.
                    _logger.LogWarning("TournamentInfo was null and should not have been.");
                }
                
                return _tournamentLocation;
            }
        }

        /// <summary>
        /// Backing member variable for the date and time when the tournament is being held.
        /// </summary>
        public string _tournamentTime = string.Empty;

        /// <summary>
        /// Gets the date and time when the tournament is being held.
        /// </summary>
        public string TournamentTime
        {
            get
            {
                if (TournamentInfo != null && string.IsNullOrEmpty(_tournamentTime))
                {
                    _tournamentTime =
                        (!string.IsNullOrWhiteSpace(TournamentInfo.Time)
                        ? TournamentInfo.Time
                        : "TBA");
                }
                else
                {
                    // TODO: Test that this actually works.
                    _logger.LogWarning("TournamentInfo was null and should not have been.");
                }

                return _tournamentTime;
            }
        }

        public string _displayableRegistrationName = string.Empty;

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
        public string DisplayableRegistrationName
        {
            get
            {
                if (string.IsNullOrEmpty(_displayableRegistrationName))
                {
                    // Make sure that CurrentRegistrationType has been set before calling this method.
                    // TODO: we should probably assert here if CurrentRegistrationType has not been set.
                    // TODO: we should definitely log an error here if CurrentRegistrationType has not been set.
                    if (CurrentRegistrationType == RegistrationType.None)
                    {
                        // TODO: Do nothing, since it's already string.Empty. We should be able to remove this.
                        _displayableRegistrationName = string.Empty;
                    }

                    switch (CurrentRegistrationType)
                    {
                        case RegistrationType.None:
                        case RegistrationType.Volunteer:
                            _displayableRegistrationName = "Invalid Registration";
                            break;

                        case RegistrationType.CoachesTraining:
                            _displayableRegistrationName = "Coaches Training Registration";
                            break;

                        case RegistrationType.Tournament:
                        case RegistrationType.Judges:
                        default:
                            _displayableRegistrationName = CurrentRegistrationType + " Registration";
                            break;
                    }
                }

                return _displayableRegistrationName;
            }
        }
    }
}
