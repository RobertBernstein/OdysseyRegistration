using JudgeRegistrationRazor.Data;
using JudgeRegistrationRazor.Models;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Globalization;

namespace JudgeRegistrationRazor.Pages.Shared
{
    public class BasePageModel(OdysseyContext context, ILogger<BasePageModel> logger) : PageModel
    {

        /// <summary>
        /// Gets a publicly accessible context for child classes.
        /// </summary>
        public OdysseyContext Context => context;

        /// <summary>
        /// Gets a publicly accessible logger for child classes.
        /// </summary>
        public ILogger<BasePageModel> Logger => logger;

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
                    _config = (from c in context.Configs
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
                // If _siteName is null, run the LINQ query, assign the result to RegionName, and return the result
                if (string.IsNullOrEmpty(_siteName))
                {
                    if (Config != null)
                    {
                        _regionName = Config["RegionName"];

                        // TODO: Test this.
                        _siteName = Request.GetEncodedPathAndQuery() != null
                            ? Request.Host.Host.ToLowerInvariant()
                            : "novanorth.org";

                        // Drop the "www." if present.
                        _siteName.Replace("www.", string.Empty);
                    }
                    else
                    {
                        logger.LogWarning("Config was null and should not have been.");
                    }
                }

                return _siteName;
            }
        }

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
                        logger.LogWarning("Config was null and should not have been.");
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
                        _regionNumber = Config["RegionNumber"];
                    }
                    else
                    {
                        logger.LogWarning("Config was null and should not have been.");
                    }
                }

                return _regionNumber;
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

        /// <summary>
        /// Gets or sets the current registration type.
        /// </summary>
        public RegistrationType CurrentRegistrationType { get; set; }

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
                    logger.LogWarning("TournamentInfo was null and should not have been.");
                }

                return _tournamentTime;
            }
        }

        /// <summary>
        /// Backing member variable for information about the tournament.
        /// </summary>
        public Event? _tournamentInfo = null;

        /// <summary>
        /// Gets information about the tournament.
        /// </summary>
        public Event? TournamentInfo
        {
            get
            {
                if (RegionName != null)
                {
                    _tournamentInfo ??= (from o in context.Events
                                         where o.EventName.StartsWith(RegionName) && o.EventName.Contains("Tournament")
                                         select o).FirstOrDefault();
                }
                else
                {
                    logger.LogWarning("RegionName was null and should not have been.");
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
                    logger.LogWarning("TournamentInfo was null and should not have been.");
                }

                return _tournamentLocation;
            }
        }
    }
}
