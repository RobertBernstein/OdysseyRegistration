// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OdysseyRepository.cs" company="Tardis Technologies">
//   Copyright 2014-2025 Tardis Technologies. All rights reserved.
// </copyright>
// <summary>
//   The Odyssey registration database repository.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

// Decompiled with JetBrains decompiler
// Type: OdysseyMvc4.Models.OdysseyRepository
// Assembly: OdysseyMvc4, Version=1.0.5460.36587, Culture=neutral, PublicKeyToken=null
// MVID: 7B658547-521F-44CB-80FA-52857CB94B72
// Assembly location: C:\Users\rob\OneDrive\Odyssey\OdysseyProd\registration\bin\OdysseyMvc4.dll

using System.Collections;
using System.Globalization;
using System.Linq.Expressions;

namespace OdysseyMvc2024.Models
{
    /// <summary>
    /// The Odyssey registration database repository.
    /// </summary>
    public class OdysseyRepository : IOdysseyRepository
    {
        private const int TheNotSpecifiedProblemNumber = 0;
        private const int ThePrimaryProblemNumber = 6;
        private const int TheSpontaneousProblemNumber = 7;

        /// <summary>
        /// The database context.
        /// </summary>
        private readonly OdysseyEntities _context;

        /// <summary>
        /// The registration system configuration.
        /// </summary>
        /// <remarks>
        /// Benefits of Lazy Initialization:
        /// * Lazy Initialization: By using Lazy&lt;Dictionary&lt;string, string&gt;&gt;, the Config dictionary is initialized only once, the first time it's accessed.
        /// * Thread Safety: Lazy&lt;T&gt; is thread-safe by default, preventing multiple threads from causing multiple database queries.
        /// * Constructor Initialization: The Lazy&lt;T&gt; instance _config is initialized in the constructor, capturing the context needed to query the database.
        /// * Direct Property Access: The Config property returns _config.Value, ensuring the dictionary is available without additional checks.
        /// </remarks>
        // TODO: (Rob) Test that this works.
        private readonly Lazy<Dictionary<string, string>> _config;

        public OdysseyRepository(IOdysseyEntities context)
        {
            _context = (OdysseyEntities)context;

            // TODO: (Rob) Test that this works.
            _config = new Lazy<Dictionary<string, string>>(() =>
            {
                var config = _context.Configs.ToDictionary(d => d.Name, d => d.Value);
                config.Add("EndYear", (int.Parse(config["Year"]) + 1).ToString(CultureInfo.InvariantCulture));
                return config;
            });
        }

        ///// <summary>
        ///// The Coaches Training event information.
        ///// </summary>
        // TODO: (Rob) Consider splitting the OdysseyRepository class (after everything is converted to .NET (Core)) into BaseRegistrationRepository, JudgesRegistrationRepository, TournamentRegistrationRepository, etc., each with a single responsibility.
        // private Event coachesTrainingInfo;

        ///// <summary>
        ///// The list of Coaches Training divisions.
        ///// </summary>
        // private IEnumerable<CoachesTrainingDivision> divisions;

        /// <summary>
        /// The information for all Odyssey events this season.
        /// </summary>
        private IEnumerable<Event>? _events;

        /// <summary>
        /// The list of judges registered for the tournament.
        /// </summary>
        private IEnumerable<Judge>? _judges;

        /// <summary>
        /// The judges information.
        /// </summary>
        private Event? _judgesInfo;

        /// <summary>
        /// The primary problem.
        /// </summary>
        private IQueryable<Problem>? _primaryProblem;

        /// <summary>
        /// The problem choices.
        /// </summary>
        private IEnumerable<Problem>? _problemChoices;

        /// <summary>
        /// The problem choices without spontaneous.
        /// </summary>
        /// TODO: Is this still needed?  Can it be removed?  Why is it never initialized? - Rob, 12/17/2018.
        private IEnumerable<Problem>? _problemChoicesWithoutSpontaneous;

        /// <summary>
        /// The problem conflicts.
        /// </summary>
        private IEnumerable<Problem>? _problemConflicts;

        /// <summary>
        /// The problems.
        /// </summary>
        private IEnumerable<Problem>? _problems;

        /// <summary>
        /// The problems without primary or spontaneous.
        /// </summary>
        private IQueryable<Problem>? _problemsWithoutPrimaryOrSpontaneous;

        /// <summary>
        /// The problems without spontaneous.
        /// TODO: Delete?
        /// </summary>
        private IQueryable<Problem>? _problemsWithoutSpontaneous;

        /// <summary>
        /// The region name.
        /// </summary>
        private string? _regionName;

        /// <summary>
        /// The region number.
        /// </summary>
        private string? _regionNumber;

        ///// <summary>
        ///// The list of Coaches Training regions.
        ///// </summary>
        // private IEnumerable<CoachesTrainingRegion> regions;

        ///// <summary>
        ///// The list of Coaches Training roles.
        ///// </summary>
        // private IEnumerable<CoachesTrainingRole> roles;

        /// <summary>
        /// The list of schools participating in Odyssey this season.
        /// </summary>
        private IEnumerable? _schools;

        /// <summary>
        /// The Tournament event information.
        /// </summary>
        private Event? _tournamentInfo;

        ///// <summary>
        ///// The volunteer information.
        ///// </summary>
        // private Event? _volunteerInfo;

        ///// <summary>
        ///// Get the column names for the specified database table.
        ///// TODO: Uncomment this code if it's ever needed again. (9/19/2013)
        ///// </summary>
        ///// <param name="tableName">The table name to get the column names for.</param>
        ///// <returns>The <see><cref>string[]</cref></see>.</returns>
        ///// <remarks>This code is no longer used, but it was difficult to figure out how to query for this. So, it remains here but commented-out.</remarks>
        // public string[] GetColumnNames(string tableName)
        // {
        //     ObjectQuery tableContext;
        //     switch (tableName)
        //     {
        //         case "CoachesTrainingRegistration":
        //             tableContext = this.context.coaches_training;
        //             break;

        //         case "Judges":
        //             tableContext = this.context.judges;
        //             break;

        //         case "TournamentRegistration":
        //             tableContext = this.context.TournamentRegistrations;
        //             break;

        //         case "Volunteers":
        //             tableContext = this.context.Volunteers;
        //             break;

        //         default:
        //             tableContext = null;
        //             break;
        //     }

        //     var columnNames = new List<string>();
        //     QueryInfo qi = this.GetQueryInfo(tableContext);
        //     if (qi.CsFieldMap.Count > 0)
        //     {
        //         foreach (var field in qi.CsFieldMap)
        //         {
        //             // Handle the case where a column name contains a '?'
        //             columnNames.Add(field.Key.Replace("?", "_"));
        //         }
        //     }

        //     return columnNames.ToArray();
        // }

        ///// <summary>
        ///// Gets or sets the Coaches Training event information.
        ///// </summary>
        // public Event CoachesTrainingInfo
        // {
        //     get
        //     {
        //         return this.coachesTrainingInfo ?? (this.CoachesTrainingInfo = (from o in this.context.Events
        //             where o.EventName.Contains("Coaches") && o.EventName.Contains("Training")
        //             select o).First());
        //     }

        //     set
        //     {
        //         this.coachesTrainingInfo = value;
        //     }
        // }
        // public Event CoachesTrainingInfo
        // {
        //     get
        //     {
        //         var coachesTrainingInfo = this.coachesTrainingInfo ??
        //                                   (CoachesTrainingInfo = ((IQueryable<Event>)this.context.Events).Where((Expression<Func<Event, bool>>)(o => o.EventName.Contains("Coaches") && o.EventName.Contains("Training"))).First());

        //         return coachesTrainingInfo;
        //     }
        //
        //     set => this.coachesTrainingInfo = value;
        // }

        ///// <summary>
        ///// Gets the list of coaches training registrations.
        ///// </summary>
        // public IEnumerable<CoachesTrainingRegistration> CoachesTrainingRegistrations
        // {
        //     get
        //     {
        //         return from c in this.context.CoachesTrainingRegistrations
        //             orderby c.RegistrationID
        //             select c;
        //     }
        // }
        // public IEnumerable<CoachesTrainingRegistration> CoachesTrainingRegistrations =>
        //     (IEnumerable<CoachesTrainingRegistration>)Queryable.OrderBy<CoachesTrainingRegistration, int>((IQueryable<CoachesTrainingRegistration>)this.context.CoachesTrainingRegistrations, (Expression<Func<CoachesTrainingRegistration, int>>)(c => c.RegistrationID));

        /// <summary>
        /// Gets the registration system configuration.
        /// </summary>
        // TODO: (Rob) Test that this works.
        public Dictionary<string, string> Config => _config.Value;
        // public Dictionary<string, string> Config
        // {
        //     get
        //     {
        //         // If config is null, run the LINQ query, assign the result to Config as a Dictionary, and return the result.
        //         if (this.config == null)
        //         {
        //             this.Config = (from c in this.context.Configs
        //                 select c).ToDictionary(d => d.Name, d => d.Value);
        //         }

        //         return this.config;
        //     }

        //     private set
        //     {
        //         this.config = value;
        //         this.config.Add("EndYear", (int.Parse(this.config["Year"]) + 1).ToString(CultureInfo.InvariantCulture));
        //     }
        // }

        public IEnumerable<Event>? Events
        {
            get
            {
                _events ??= [.. (from c in _context.Events
                                select c)];
                return _events;
            }
            private set => _events = value;
        }

        // public IEnumerable<CoachesTrainingDivision> Divisions
        // {
        //   get
        //   {
        //     IEnumerable<CoachesTrainingDivision> divisions = this.divisions;
        //     if (divisions == null)
        //       divisions = this.Divisions = (IEnumerable<CoachesTrainingDivision>) Queryable.OrderBy<CoachesTrainingDivision, byte>((IQueryable<CoachesTrainingDivision>) this.context.CoachesTrainingDivisions, (Expression<Func<CoachesTrainingDivision, byte>>) (d => d.ID));
        //     return divisions;
        //   }
        //
        //   private set => this.divisions = value;
        // }

        /// <summary>
        /// Gets the list of judges that are registered.
        /// </summary>
        public IEnumerable<Judge>? Judges
        {
            get
            {
                // If judges is null, run the LINQ query, assign the result to Judges, and return the result.
                _judges ??= [.. (from c in _context.Judges
                                select c)];
                return _judges;
            }

            private set => _judges = value;
        }

        /// <summary>
        /// Gets or sets the judges information for the tournament.
        /// </summary>
        public Event? JudgesInfo
        {
            get
            {
                var judgesInfo = _judgesInfo ?? (JudgesInfo = _context.Events.First(o => o.EventName.Contains("Judges") && o.EventName.Contains("Training")));
                return judgesInfo;
            }

            // TODO: Make this private.
            set => _judgesInfo = value;

            // get
            // {
            //     return this.judgesInfo ?? (this.JudgesInfo = (from o in this.context.Events
            //         where o.EventName.Contains("Judges") && o.EventName.Contains("Training")
            //         select o).First());
            // }
               
            // set
            // {
            //     this.judgesInfo = value;
            // }
        }

        /// <summary>
        /// Gets the primary problem.
        /// </summary>
        public IQueryable<Problem>? PrimaryProblem
        {
            get
            {
                // If primaryProblem is null, run the LINQ query, assign the result to PrimaryProblem, and return the result.
                // - The Primary problem is ProblemID 6
                var thePrimaryProblem = _primaryProblem ??
                                        (PrimaryProblem = _context.Problems.Where(p => p.ProblemID == ThePrimaryProblemNumber));

                return thePrimaryProblem;
            }

            private set => _primaryProblem = value;

            // get
            // {
            //     // If primaryProblem is null, run the LINQ query, assign the result to PrimaryProblem, and return the result.
            //     // - The Primary problem is ProblemID 6
            //     return this.primaryProblem ?? (this.PrimaryProblem = from p in this.context.Problems
            //         where p.ProblemID == 6
            //         select p);
            // }
               
            // private set
            // {
            //     this.primaryProblem = value;
            // }
        }

        /// <summary>
        /// Gets the problem choices for a web page drop-down list.
        /// </summary>
        public IEnumerable<Problem> ProblemChoices
        {
            get
            {
                // If problems is null, run the LINQ query, assign the result to Problems, and return the result
                var problems = _context.Problems.OrderBy(p => p.ProblemID);

                // TODO: (Rob) Should we just call the PrimaryProblem property here? 01/06/2025.
                if (problems.FirstOrDefault(problem => problem.ProblemID == ThePrimaryProblemNumber) is { } thePrimaryProblem)
                {
                    thePrimaryProblem.ProblemName += " (The Primary Problem)";
                }

                // TODO: (Rob) Test that the primary problem string gets added above.
                return _problemChoices ?? (ProblemChoices = problems);
            }

            private set => _problemChoices = value;

            // get
            // {
            //     // If problems is null, run the LINQ query, assign the result to Problems, and return the result
            //     IOrderedQueryable<Problem> temp = from p in this.context.Problems
            //         orderby p.ProblemID
            //         select p;
               
            //     Problem thePrimaryProblem = temp.FirstOrDefault(problem => problem.ProblemID == 6);
            //     if (thePrimaryProblem != null)
            //     {
            //         thePrimaryProblem.ProblemName += " (The Primary Problem)";
            //     }
               
            //     return this.problemChoices ?? (this.ProblemChoices = temp);
            // }
               
            // private set
            // {
            //     this.problemChoices = value;
            // }
        }

        /// <summary>
        /// Gets the problem choices for a web page drop-down list, having excluded the spontaneous problem from the list.
        /// </summary>
        public IEnumerable<Problem> ProblemChoicesWithoutSpontaneous
        {
            get
            {
                var problems = _context.Problems.Where(p => p.ProblemName != "Spontaneous").OrderBy(p => p.ProblemID);

                // TODO: (Rob) Should we just call the PrimaryProblem property here? 01/06/2025.
                if (problems.FirstOrDefault(problem => problem.ProblemID == ThePrimaryProblemNumber) is { } thePrimaryProblem)
                {
                    thePrimaryProblem.ProblemName += " (The Primary Problem)";
                }

                // TODO: (Rob) Look into what this does, 12/12/2014.
                return _problemChoicesWithoutSpontaneous ?? (ProblemChoices = problems);
            }

            // get
            // {
            //     // If problems is null, run the LINQ query, assign the result to Problems, and return the result
            //     IQueryable<Problem> temp = from p in this.context.Problems
            //         orderby p.ProblemID
            //         where p.ProblemName != "Spontaneous"
            //         select p;
               
            //     Problem thePrimaryProblem = temp.FirstOrDefault(problem => problem.ProblemID == 6);
            //     if (thePrimaryProblem != null)
            //     {
            //         thePrimaryProblem.ProblemName += " (The Primary Problem)";
            //     }
               
            //     // TODO: Look into what this does - Rob, 12/12/2014.
            //     return this.problemChoicesWithoutSpontaneous ?? (this.ProblemChoices = temp);
            // }
        }

        /// <summary>
        /// Gets the problem conflicts for a web page drop-down list.
        /// TODO: (Rob) Is this used for a drop-down list? 01/06/2025.
        /// TODO: (Rob) Is this code needed/used at all? 01/06/2025.
        /// </summary>
        public IEnumerable<Problem> ProblemConflicts
        {
            get
            {
                // If problems is null, run the LINQ query, assign the result to Problems, and return the result.
                var problems = _context.Problems.Where(p => p.ProblemID != TheSpontaneousProblemNumber).OrderBy(p => p.ProblemID);

                // TODO: When the following was uncommented, " (The Primary Problem)" showed up twice in the same dropdown entry.
                // I have no idea why commenting this out solves the problem!  I need to revisit and fix this.

                // var primaryProblem = temp.Where(problem => problem.ProblemID == 6).FirstOrDefault();
                // if (primaryProblem != null)
                // {
                //     primaryProblem.ProblemName += " (The Primary Problem)";
                // }

                return _problemConflicts ?? (ProblemConflicts = problems);
            }

            private set => _problemConflicts = value;

            // get
            // {
            //     // If problems is null, run the LINQ query, assign the result to Problems, and return the result.
            //     IOrderedQueryable<Problem> temp = from p in this.context.Problems
            //         where p.ProblemID != 7
            //         orderby p.ProblemID
            //         select p;
               
            //     // TODO: When the following was uncommented, " (The Primary Problem)" showed up twice in the same dropdown entry.
            //     // I have no idea why commenting this out solves the problem!  I need to revisit and fix this.
               
            //     ////var primaryProblem = temp.Where(problem => problem.ProblemID == 6).FirstOrDefault();
            //     ////if (primaryProblem != null)
            //     ////{
            //     ////primaryProblem.ProblemName += " (The Primary Problem)";
            //     ////}
               
            //     return this.problemConflicts ?? (this.ProblemConflicts = temp);
            // }
               
            // private set
            // {
            //     this.problemConflicts = value;
            // }
        }

        public IEnumerable<Problem>? Problems
        {
            get
            {
                // If problems is null, run the LINQ query, assign the result to Problems, and return the result.
                // - Skip ProblemID 0, which is "Not Specified"
                // Note: p.ProblemCategory != null means that the ProblemCategory is not "No Preference" and is not "Spontaneous".
                var problems = _problems ?? (Problems = _context.Problems.Where(p => p.ProblemCategory != null).OrderBy(p => p.ProblemID));

                // TODO: (Rob) What happens when problems is null? 01/06/2025.
                return problems;
            }

            private set => _problems = value;

            // get
            // {
            //     // If problems is null, run the LINQ query, assign the result to Problems, and return the result.
            //     // - Skip ProblemID 0, which is "Not Specified"
            //     return this.problems ?? (this.Problems = from p in this.context.Problems
            //         where p.ProblemCategory != null // not "No Preference" and not "Spontaneous"
            //         orderby p.ProblemID
            //         select p);
            // }

            // private set
            // {
            //     this.problems = value;
            // }
        }

        /// <summary>
        /// Gets the list of problems excluding the primary and spontaneous problems.
        /// </summary>
        public IQueryable<Problem>? ProblemsWithoutPrimaryOrSpontaneous
        {
            get
            {
                // If problems is null, run the LINQ query, assign the result to Problems, and return the result.
                // - Skip ProblemIDs 0 ("Not Specified"), 6 (Primary), and 7 (Spontaneous)
                var primaryOrSpontaneous = _problemsWithoutPrimaryOrSpontaneous ?? (ProblemsWithoutPrimaryOrSpontaneous = _context.Problems
                        .Where(p => p.ProblemID != TheNotSpecifiedProblemNumber && p.ProblemID != ThePrimaryProblemNumber && p.ProblemID != TheSpontaneousProblemNumber)
                        .OrderBy(p => p.ProblemID));

                return primaryOrSpontaneous;
            }

            private set => _problemsWithoutPrimaryOrSpontaneous = value;
        }

        /// <summary>
        /// Gets the list of problems excluding the spontaneous problem.
        /// </summary>
        public IQueryable<Problem>? ProblemsWithoutSpontaneous
        {
            get
            {
                // If problems is null, run the LINQ query, assign the result to Problems, and return the result.
                // - Skip ProblemID 0, which is "Not Specified"
                var problemsWithoutSpontaneous = _problemsWithoutSpontaneous ?? (ProblemsWithoutSpontaneous = _context.Problems
                    .Where(p => p.ProblemID != TheNotSpecifiedProblemNumber && p.ProblemID != TheSpontaneousProblemNumber)
                    .OrderBy(p => p.ProblemID));

                return problemsWithoutSpontaneous;

                // return _problemsWithoutSpontaneous ?? (ProblemsWithoutSpontaneous = from p in _context.Problems
                //     where p.ProblemID != 0 && p.ProblemID != 7
                //     orderby p.ProblemID
                //     select p);
            }

            private set => _problemsWithoutSpontaneous = value;
        }

        /// <summary>
        /// Gets the region name.
        /// </summary>
        public string RegionName
        {
            // If _regionName is null, run the LINQ query, assign the result to RegionName, and return the result
            get => _regionName ?? (RegionName = Config[nameof(RegionName)]);

            private set => _regionName = value;
        }

        /// <summary>
        /// Gets the region number.
        /// </summary>
        public string RegionNumber
        {
            // If _regionName is null, run the LINQ query, assign the result to RegionName, and return the result
            get => _regionNumber ?? (RegionNumber = Config[nameof(RegionNumber)]);

            private set => _regionNumber = value;
        }

        ///// <summary>
        ///// Gets the regions.
        ///// </summary>
        // public IEnumerable<CoachesTrainingRegion> Regions
        // {
        //     get
        //     {
        //         // If _problems is null, run the LINQ query, assign the result to Problems, and return the result
        //         // - Skip ProblemID 0, which is "Not Specified"
        //         return this.regions ?? (this.Regions = from r in this.context.CoachesTrainingRegions
        //             orderby r.ID
        //             select r);
        //     }
        //
        //     private set
        //     {
        //         this.regions = value;
        //     }
        // }
        //
        // public IEnumerable<CoachesTrainingRegion> Regions
        // {
        //   get
        //   {
        //     IEnumerable<CoachesTrainingRegion> regions = this.regions;
        //     if (regions == null)
        //       regions = this.Regions = (IEnumerable<CoachesTrainingRegion>) Queryable.OrderBy<CoachesTrainingRegion, int>((IQueryable<CoachesTrainingRegion>) this.context.CoachesTrainingRegions, (Expression<Func<CoachesTrainingRegion, int>>) (r => r.ID));
        //     return regions;
        //   }
        //   private set => this.regions = value;
        // }

        ///// <summary>
        ///// Gets the roles.
        ///// </summary>
        //public IEnumerable<CoachesTrainingRole> Roles
        // {
        //     get
        //     {
        //         // If _problems is null, run the LINQ query, assign the result to Problems, and return the result
        //         // - Skip ProblemID 0, which is "Not Specified"
        //         return this.roles ?? (this.Roles = from r in this.context.CoachesTrainingRoles
        //             orderby r.ID
        //             select r);
        //     }
           
        //     private set
        //     {
        //         this.roles = value;
        //     }
        // }
        //
        // public IEnumerable<CoachesTrainingRole> Roles
        // {
        //   get
        //   {
        //     IEnumerable<CoachesTrainingRole> roles = this.roles;
        //     if (roles == null)
        //       roles = this.Roles = (IEnumerable<CoachesTrainingRole>) Queryable.OrderBy<CoachesTrainingRole, byte>((IQueryable<CoachesTrainingRole>) this.context.CoachesTrainingRoles, (Expression<Func<CoachesTrainingRole, byte>>) (r => r.ID));
        //     return roles;
        //   }
        //   private set => this.roles = value;
        // }

        public IEnumerable? Schools
        {
            get
            {
                var schools = _schools ?? (Schools = _context.Schools
                    .Where(s => s.Membership_1seen == "yes")
                    .OrderBy<School, string>(s => s.Name ?? string.Empty)
                    .Select(s => new
                    {
                        s.ID, s.Name
                    }));

                return schools;
            }

            private set => _schools = value;
        }

        public Event TournamentInfo
        {
            get
            {
                // TODO: Add try/catch.
                // TODO: Add logging for when "NoVA North Regional Tournament" cannot be found in the database and display the current value of any containing "Tournament".
                // TODO: Figure out how to cache this value so we don't query for/set it every time a page is displayed.
                // TODO: This is throwing an exception!
                Event tournamentInfo = _tournamentInfo ?? (TournamentInfo = Events.AsQueryable().Where(e => e.EventName.StartsWith(RegionName) && e.EventName.Contains("Tournament")).First());
                //Event tournamentInfo = this.tournamentInfo ?? (TournamentInfo = Queryable.Where<Event>(context.Events, o => o.EventName.StartsWith(RegionName) && o.EventName.Contains("Tournament")).First<Event>());

                return tournamentInfo;
            }

            set => _tournamentInfo = value;
        }

        public IQueryable TournamentRegistration => _context.TournamentRegistrations.Select<TournamentRegistration, TournamentRegistration>(t => t).AsQueryable();

        public IEnumerable<TournamentRegistration> TournamentRegistrations => _context.TournamentRegistrations.OrderBy(t => t.Id);

        ///// <summary>
        ///// Gets or sets the volunteer info.
        ///// TODO: Test that this works. I copied it from TournamentInfo - Rob, 12/12/2014.
        ///// </summary>
        // public Event? VolunteerInfo
        // {
        //     get
        //     {
        //         var volunteerInfo = this._volunteerInfo ?? (this.VolunteerInfo = this._context.Events.First(o => o.EventName.Contains("Volunteer")));

        //         return volunteerInfo;

        //         // return this.volunteerInfo ?? (this.VolunteerInfo = (from o in this.context.Events
        //         //     where o.EventName.Contains("Volunteer")
        //         //     select o).First());

        //         // TODO: Do we still need where o.EventName.StartsWith(this.RegionName)? I don't think so - Rob, 12/12/2014.
        //         // return this.volunteerInfo ?? (this.VolunteerInfo = (from o in this.context.Events
        //         //                                                     where o.EventName.StartsWith(this.RegionName) && o.EventName.Contains("Volunteer")
        //         //                                                     select o).First());
        //     }

        //     private set => _volunteerInfo = value;
        // }

        ///// <summary>
        ///// Gets the volunteers.
        ///// </summary>
        // public IQueryable Volunteers =>
        //     (IQueryable)((IQueryable<Volunteer>)this.context.Volunteers).Select<Volunteer, Volunteer>((Expression<Func<Volunteer, Volunteer>>)(v => v)).AsQueryable<Volunteer>();
        //
        // public IQueryable Volunteers
        // {
        //     get
        //     {
        //         return (from v in this.context.Volunteers
        //             select v).AsQueryable();
        //     }
        // }

        ///// <summary>
        ///// The add coaches training registration.
        ///// </summary>
        ///// <param name="newRegistration">
        ///// The new registration.
        ///// </param>
        ///// <returns>
        ///// The <see cref="int"/>.
        ///// </returns>
        // public int AddCoachesTrainingRegistration(CoachesTrainingRegistration newRegistration)
        // {
        //     if (newRegistration != null)
        //     {
        //         // SaveChanges returns the number of objects added to the database
        //         this.context.CoachesTrainingRegistrations.Add(newRegistration);
        //         return this.context.SaveChanges();
        //     }

        //     return 0;

        //     // if (newRegistration == null)
        //     //     return 0;
        //     // this.context.CoachesTrainingRegistrations.Add(newRegistration);
        //     // return this.context.SaveChanges();
        // }

        public int AddJudge(Judge newJudge)
        {
            if (newJudge != null)
            {
                // SaveChanges returns the number of objects added to the database
                _context.Judges.Add(newJudge);
                return _context.SaveChanges();
            }

            return 0;
        }

        public int AddTournamentRegistration(TournamentRegistration newRegistration)
        {
            if (newRegistration == null)
            {
                return 0;
            }

            _context.TournamentRegistrations.Add(newRegistration);
            return _context.SaveChanges();
        }

        // public int AddVolunteer(Volunteer newVolunteer, int? tournamentRegistrationId = null)
        // {
        //   if (newVolunteer == null)
        //     return 0;
        //   newVolunteer.TeamID = tournamentRegistrationId;
        //   this.context.Volunteers.Add(newVolunteer);
        //   return this.context.SaveChanges();
        // }

        /// <summary>
        /// Clear the team ID from a judge record in the database.
        /// </summary>
        /// <param name="judgeId">
        /// The judge ID for the judge record in the database.
        /// </param>
        /// <param name="judgeFirstName">
        /// The judge's first name.
        /// </param>
        /// <param name="judgeLastName">
        /// The judge's last name.
        /// </param>
        public void ClearTeamIdFromJudgeRecord(
            int judgeId,
            string judgeFirstName,
            string judgeLastName)
        {
            // Judge judge = Queryable.Where<Judge>((IQueryable<Judge>)this.context.Judges, (Expression<Func<Judge, bool>>)(j => j.JudgeID == judgeId)).FirstOrDefault<Judge>();
            Judge judgeRecord = (from j in _context.Judges
                                 where j.JudgeID == judgeId
                                 select j).FirstOrDefault();

            // if (judge == null)
            //     return;
            // judge.TeamID = (string)null;
            // this.context.SaveChanges();

            if (judgeRecord != null)
            {
                judgeRecord.TeamID = null;
                _context.SaveChanges();
            }
        }

        // TODO: Get this working under EF Core.
        /// <summary>
        /// Export the list of registered judges.
        /// </summary>
        /// <returns>
        /// The <see cref="IQueryable"/>.
        /// </returns>
        // public IQueryable<JudgesExport> ExportJudges() => Queryable.Select(Queryable.OrderBy(Queryable.SelectMany(Queryable.GroupJoin((IQueryable<Judge>)this.context.Judges, (IEnumerable<Problem>)this.context.Problems, (Expression<Func<Judge, string>>)(j => j.ProblemCOI1), (Expression<Func<Problem, string>>)(p => p.ProblemID.ToString(CultureInfo.InvariantCulture)), (j, jp) => new
        // public IQueryable<JudgesExport> ExportJudges()
        // {
        //     //      j = j,
        //     //      jp = jp
        //     //  }), data => data.jp, (data, y) => new
        //     //  {
        //     //\u003C\u003Eh__TransparentIdentifier6 = data,
        //     //      y = y
        //     //  }), data => j.JudgeID), data => new JudgesExport()
           
        //     // Solution for multiple joins came from https://stackoverflow.com/questions/267488/linq-to-sql-multiple-left-outer-joins
        //     return from j in this.context.Judges
        //            join p in this.context.Problems on j.ProblemCOI1 equals p.ProblemID.ToString(CultureInfo
        //                .InvariantCulture) into jp
        //            from y in jp
        //            orderby j.JudgeID
        //            select new JudgesExport
        //            {
        //                JudgeId = j.JudgeID,
        //                TeamId = j.TeamID,
        //                FirstName = j.FirstName,
        //                LastName = j.LastName,
        //                Address = j.Address,
        //                Address2 = j.AddressLine2,
        //                City = j.City,
        //                StateOrProvince = j.State,
        //                PostalCode = j.ZipCode,
        //                DaytimePhone = j.DaytimePhone,
        //                EveningPhone = j.EveningPhone,
        //                Email = j.EmailAddress,
        //                Notes = j.Notes,
        //                ProblemConflictOfInterest1 = y.ProblemName,
        //                ProblemConflictOfInterest2 = j.ProblemCOI2,
        //                ProblemConflictOfInterest3 = j.ProblemCOI3,
        //                ProblemChoice1 = j.ProblemChoice1,
        //                ProblemChoice2 = j.ProblemChoice2,
        //                ProblemChoice3 = j.ProblemChoice3,
        //                TshirtSize = j.TshirtSize,
        //                ContinuingEducationUnits = j.WantsCEUCredit,
        //                YearsOfLongTermJudgingExperience = j.YearsOfLongTermJudgingExperience,
        //                YearsOfSpontaneousJudgingExperience = j.YearsOfSpontaneousJudgingExperience,
        //                TimeRegistered = j.TimeRegistered,
        //                TimeAssignedToTeam = j.TimeAssignedToTeam,
        //                TimeRegistrationStarted = j.TimeRegistrationStarted,
        //                UserAgent = j.Headers["User-Agent"].ToString()
        //                //});
        //            };
        // }

        // public IQueryable<CoachesTrainingRegistration> GetCoachById(int coachId) => Queryable.Where<CoachesTrainingRegistration>((IQueryable<CoachesTrainingRegistration>) this.context.CoachesTrainingRegistrations, (Expression<Func<CoachesTrainingRegistration, bool>>) (c => c.RegistrationID == coachId));

        // public IQueryable<CoachesTrainingRegistration> GetCoachesTrainingRegistrationById(int id) => Queryable.Where<CoachesTrainingRegistration>((IQueryable<CoachesTrainingRegistration>) this.context.CoachesTrainingRegistrations, (Expression<Func<CoachesTrainingRegistration, bool>>) (c => c.RegistrationID == id));

        // public IQueryable<Judge> GetJudgeById(int judgeId) => Queryable.Where<Judge>((IQueryable<Judge>)this.context.Judges, (Expression<Func<Judge, bool>>)(j => j.JudgeID == judgeId));
        public IQueryable<Judge> GetJudgeById(int judgeId)
        {
            return from j in _context.Judges
                   where j.JudgeID == judgeId
                   select j;
        }

        public IQueryable<Judge> GetJudgeByIdAndName(
            int judgeId,
            string judgeFirstName,
            string judgeLastName)
        {
            // return Queryable.Where<Judge>((IQueryable<Judge>)this.context.Judges, (Expression<Func<Judge, bool>>)(j => j.JudgeID == judgeId && j.FirstName.ToLower() == judgeFirstName.ToLower() && j.LastName.ToLower() == judgeLastName.ToLower()));
            return from j in _context.Judges
                   where (j.JudgeID == judgeId) && (j.FirstName.ToLower() == judgeFirstName.ToLower()) && (j.LastName.ToLower() == judgeLastName.ToLower())
                   select j;
        }

        public short? GetJudgeIdFromTournamentRegistrationId(int tournamentRegistrationId) => (short?)Queryable.Where<TournamentRegistration>((IQueryable<TournamentRegistration>)_context.TournamentRegistrations, (Expression<Func<TournamentRegistration, bool>>)(t => t.Id == tournamentRegistrationId)).FirstOrDefault<TournamentRegistration>()?.JudgeID;

        public void GetJudgeNameFromJudgeId(
          short? judgeId,
          out string judgeFirstName,
          out string judgeLastName)
        {
            Judge judge = Queryable.Where<Judge>((IQueryable<Judge>)_context.Judges, (Expression<Func<Judge, bool>>)(j => (int?)j.JudgeID == (int?)judgeId)).FirstOrDefault<Judge>();
            if (judge == null)
            {
                judgeFirstName = (string)null;
                judgeLastName = (string)null;
            }
            else
            {
                judgeFirstName = judge.FirstName;
                judgeLastName = judge.LastName;
            }
        }

        public List<string> GetMemberGradesByRegistration(int id)
        {
            TournamentRegistration tournamentRegistration = Queryable.Where<TournamentRegistration>((IQueryable<TournamentRegistration>)_context.TournamentRegistrations, (Expression<Func<TournamentRegistration, bool>>)(t => t.Id == id)).FirstOrDefault<TournamentRegistration>();
            if (tournamentRegistration == null)
                return (List<string>)null;
            return new List<string>()
      {
        tournamentRegistration.MemberGrade1,
        tournamentRegistration.MemberGrade2,
        tournamentRegistration.MemberGrade3,
        tournamentRegistration.MemberGrade4,
        tournamentRegistration.MemberGrade5,
        tournamentRegistration.MemberGrade6,
        tournamentRegistration.MemberGrade7
      };
        }

        public string? GetProblemNameFromProblemId(int? problemId)
        {
            try
            {
                return !problemId.HasValue ? null : _context.Problems.FirstOrDefault(p => p.ProblemID == problemId)?.ProblemName;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public string? GetSchoolNameFromSchoolId(int? schoolId) =>
            _context.Schools.FirstOrDefault(s => (int?)s.ID == schoolId)?.Name;

        public TournamentRegistration? GetTournamentRegistrationById(int tournamentRegistrationId) =>
            _context.TournamentRegistrations.FirstOrDefault(t => t.Id == tournamentRegistrationId);

        // public Volunteer GetVolunteerById(int? volunteerId) => Queryable.FirstOrDefault<Volunteer>((IQueryable<Volunteer>) this.context.Volunteers, (Expression<Func<Volunteer, bool>>) (v => (int?) v.VolunteerID == volunteerId));

        // public Volunteer GetVolunteerByIdAndName(
        //   int volunteerId,
        //   string volunteerFirstName,
        //   string volunteerLastName)
        // {
        //   return Queryable.FirstOrDefault<Volunteer>((IQueryable<Volunteer>) this.context.Volunteers, (Expression<Func<Volunteer, bool>>) (v => v.VolunteerID == volunteerId && string.Equals(v.FirstName, volunteerFirstName, StringComparison.CurrentCultureIgnoreCase) && string.Equals(v.LastName, volunteerLastName, StringComparison.CurrentCultureIgnoreCase)));
        // }

        // public int? GetVolunteerIdFromTournamentRegistrationId(int tournamentRegistrationId) =>
        //     _context.TournamentRegistrations.First(t => t.Id == tournamentRegistrationId).VolunteerID;

        public int UpdateJudge(int judgeId, int pageNumber, Judge newRegistrationData)
        {
            var source = _context.Judges.Where(j => j.JudgeID == judgeId);

            if (!source.Any())
            {
                return 0;
            }

            switch (pageNumber)
            {
                case 2:
                    source.First<Judge>().FirstName = newRegistrationData.FirstName;
                    source.First<Judge>().LastName = newRegistrationData.LastName;
                    source.First<Judge>().Address = newRegistrationData.Address;
                    source.First<Judge>().AddressLine2 = newRegistrationData.AddressLine2;
                    source.First<Judge>().City = newRegistrationData.City;
                    source.First<Judge>().State = newRegistrationData.State;
                    source.First<Judge>().ZipCode = newRegistrationData.ZipCode;
                    source.First<Judge>().EveningPhone = newRegistrationData.EveningPhone;
                    source.First<Judge>().DaytimePhone = newRegistrationData.DaytimePhone;
                    source.First<Judge>().MobilePhone = newRegistrationData.MobilePhone;
                    source.First<Judge>().EmailAddress = newRegistrationData.EmailAddress;
                    source.First<Judge>().ProblemChoice1 = newRegistrationData.ProblemChoice1;
                    source.First<Judge>().ProblemChoice2 = newRegistrationData.ProblemChoice2;
                    source.First<Judge>().ProblemChoice3 = newRegistrationData.ProblemChoice3;
                    source.First<Judge>().HasChildrenCompeting = newRegistrationData.HasChildrenCompeting;
                    source.First<Judge>().ProblemCOI1 = newRegistrationData.ProblemCOI1;
                    source.First<Judge>().ProblemCOI2 = newRegistrationData.ProblemCOI2;
                    source.First<Judge>().ProblemCOI3 = newRegistrationData.ProblemCOI3;
                    source.First<Judge>().YearsOfLongTermJudgingExperience = newRegistrationData.YearsOfLongTermJudgingExperience;
                    source.First<Judge>().YearsOfSpontaneousJudgingExperience = newRegistrationData.YearsOfSpontaneousJudgingExperience;
                    source.First<Judge>().PreviousPositions = newRegistrationData.PreviousPositions;
                    source.First<Judge>().WillingToBeScorechecker = newRegistrationData.WillingToBeScorechecker;
                    source.First<Judge>().TshirtSize = newRegistrationData.TshirtSize;
                    source.First<Judge>().WantsCEUCredit = newRegistrationData.WantsCEUCredit;
                    source.First<Judge>().Notes = newRegistrationData.Notes;
                    source.First<Judge>().PreviousPositions = newRegistrationData.PreviousPositions;
                    break;
                case 3:
                    source.First<Judge>().TimeRegistered = new DateTime?(DateTime.Now);
                    break;
            }

            return _context.SaveChanges();
        }

        public int UpdateJudgeEmail(int judgeId, string email)
        {
            var judgesFound = _context.Judges.Where(j => j.JudgeID == judgeId);

            if (!judgesFound.Any())
            {
                return 0;
            }

            judgesFound.First().EmailAddress = email;

            return _context.SaveChanges();
        }

        public int UpdateJudgeRecordWithTournamentRegistrationId(
          short? judgeId,
          int tournamentRegistrationId,
          out string errorMessage)
        {
            errorMessage = string.Empty;
            var nullable = judgeId;
            if (!(nullable.HasValue ? new int?((int)nullable.GetValueOrDefault()) : new int?()).HasValue)
                return 0;
            Judge judge = Queryable.Where<Judge>((IQueryable<Judge>)_context.Judges, (Expression<Func<Judge, bool>>)(j => (int?)j.JudgeID == (int?)judgeId)).First<Judge>();
            if (!string.IsNullOrWhiteSpace(judge.TeamID))
            {
                int result;
                if (int.TryParse(judge.TeamID, out result) && result == tournamentRegistrationId)
                    return 0;
                errorMessage = "The selected judge has already been assigned to another team. &nbsp;The webmaster has been notified and you will be contacted about how to complete your registration.";
            }
            judge.TeamID = tournamentRegistrationId.ToString((IFormatProvider)CultureInfo.InvariantCulture);
            judge.TimeAssignedToTeam = new DateTime?(DateTime.Now);
            return _context.SaveChanges();
        }

        public int UpdateTournamentRegistration(
          int id,
          int pageNumber,
          TournamentRegistration? newRegistrationData)
        {
            var tournamentRegistrations = _context.TournamentRegistrations.Where(r => r.Id == id);
            
            if (!tournamentRegistrations.Any())
            {
                return 0;
            }

            // TODO: (Rob) We should log when newRegistrationData is null here, possibly take other actions. 01/06/2025.
            if (newRegistrationData is null)
            {
                return 0;
            }

            switch (pageNumber)
            {
                case 2:
                    tournamentRegistrations.First().SchoolID = newRegistrationData.SchoolID;
                    break;

                case 3:
                    tournamentRegistrations.First().JudgeID = newRegistrationData.JudgeID;
                    break;

                case 4:
                    tournamentRegistrations.First().VolunteerID = newRegistrationData.VolunteerID;
                    break;

                case 5:
                    tournamentRegistrations.First().CoachFirstName = newRegistrationData.CoachFirstName;
                    tournamentRegistrations.First().CoachLastName = newRegistrationData.CoachLastName;
                    tournamentRegistrations.First().CoachAddress = newRegistrationData.CoachAddress;
                    tournamentRegistrations.First().CoachCity = newRegistrationData.CoachCity;
                    tournamentRegistrations.First().CoachState = newRegistrationData.CoachState;
                    tournamentRegistrations.First().CoachZipCode = newRegistrationData.CoachZipCode;
                    tournamentRegistrations.First().CoachEveningPhone = newRegistrationData.CoachEveningPhone;
                    tournamentRegistrations.First().CoachDaytimePhone = newRegistrationData.CoachDaytimePhone;
                    tournamentRegistrations.First().CoachMobilePhone = newRegistrationData.CoachMobilePhone;
                    tournamentRegistrations.First().CoachEmailAddress = newRegistrationData.CoachEmailAddress;
                    tournamentRegistrations.First().AltCoachFirstName = newRegistrationData.AltCoachFirstName;
                    tournamentRegistrations.First().AltCoachLastName = newRegistrationData.AltCoachLastName;
                    tournamentRegistrations.First().AltCoachEveningPhone = newRegistrationData.AltCoachEveningPhone;
                    tournamentRegistrations.First().AltCoachDaytimePhone = newRegistrationData.AltCoachDaytimePhone;
                    tournamentRegistrations.First().AltCoachMobilePhone = newRegistrationData.AltCoachMobilePhone;
                    tournamentRegistrations.First().AltCoachEmailAddress = newRegistrationData.AltCoachEmailAddress;
                    break;

                case ThePrimaryProblemNumber:
                    tournamentRegistrations.First().MemberFirstName1 = newRegistrationData.MemberFirstName1;
                    tournamentRegistrations.First().MemberLastName1 = newRegistrationData.MemberLastName1;
                    tournamentRegistrations.First().MemberGrade1 = newRegistrationData.MemberGrade1;
                    tournamentRegistrations.First().MemberFirstName2 = newRegistrationData.MemberFirstName2;
                    tournamentRegistrations.First().MemberLastName2 = newRegistrationData.MemberLastName2;
                    tournamentRegistrations.First().MemberGrade2 = newRegistrationData.MemberGrade2;
                    tournamentRegistrations.First().MemberFirstName3 = newRegistrationData.MemberFirstName3;
                    tournamentRegistrations.First().MemberLastName3 = newRegistrationData.MemberLastName3;
                    tournamentRegistrations.First().MemberGrade3 = newRegistrationData.MemberGrade3;
                    tournamentRegistrations.First().MemberFirstName4 = newRegistrationData.MemberFirstName4;
                    tournamentRegistrations.First().MemberLastName4 = newRegistrationData.MemberLastName4;
                    tournamentRegistrations.First().MemberGrade4 = newRegistrationData.MemberGrade4;
                    tournamentRegistrations.First().MemberFirstName5 = newRegistrationData.MemberFirstName5;
                    tournamentRegistrations.First().MemberLastName5 = newRegistrationData.MemberLastName5;
                    tournamentRegistrations.First().MemberGrade5 = newRegistrationData.MemberGrade5;
                    tournamentRegistrations.First().MemberFirstName6 = newRegistrationData.MemberFirstName6;
                    tournamentRegistrations.First().MemberLastName6 = newRegistrationData.MemberLastName6;
                    tournamentRegistrations.First().MemberGrade6 = newRegistrationData.MemberGrade6;
                    tournamentRegistrations.First().MemberFirstName7 = newRegistrationData.MemberFirstName7;
                    tournamentRegistrations.First().MemberLastName7 = newRegistrationData.MemberLastName7;
                    tournamentRegistrations.First().MemberGrade7 = newRegistrationData.MemberGrade7;
                    break;

                case TheSpontaneousProblemNumber:
                    tournamentRegistrations.First().ProblemID = newRegistrationData.ProblemID;
                    tournamentRegistrations.First().Division = newRegistrationData.Division;
                    tournamentRegistrations.First().Spontaneous = newRegistrationData.Spontaneous;
                    break;

                case 8:
                    tournamentRegistrations.First().SchedulingIssues = newRegistrationData.SchedulingIssues;
                    tournamentRegistrations.First().SpecialConsiderations = newRegistrationData.SpecialConsiderations;
                    break;

                case 10:
                    tournamentRegistrations.First().TimeRegistered = newRegistrationData.TimeRegistered;
                    break;
            }

            return _context.SaveChanges();
        }

        // public int UpdateVolunteer(int volunteerId, int pageNumber, Volunteer newRegistrationData)
        // {
        //   Volunteer volunteer = Queryable.FirstOrDefault<Volunteer>((IQueryable<Volunteer>) this.context.Volunteers, (Expression<Func<Volunteer, bool>>) (v => v.VolunteerID == volunteerId));
        //   if (volunteer == null)
        //     return 0;
        //   switch (pageNumber)
        //   {
        //     case 2:
        //       volunteer.FirstName = newRegistrationData.FirstName;
        //       volunteer.LastName = newRegistrationData.LastName;
        //       volunteer.DaytimePhone = newRegistrationData.DaytimePhone;
        //       volunteer.EveningPhone = newRegistrationData.EveningPhone;
        //       volunteer.MobilePhone = newRegistrationData.MobilePhone;
        //       volunteer.EmailAddress = newRegistrationData.EmailAddress;
        //       volunteer.VolunteerWantsToSee = newRegistrationData.VolunteerWantsToSee;
        //       volunteer.Notes = newRegistrationData.Notes;
        //       break;
        //     case 3:
        //       volunteer.TimeRegistered = new DateTime?(DateTime.Now);
        //       break;
        //   }
        //   return this.context.SaveChanges();
        // }

        // public int UpdateVolunteerEmail(int volunteerId, string email)
        // {
        //   Volunteer volunteer = Queryable.FirstOrDefault<Volunteer>((IQueryable<Volunteer>) this.context.Volunteers, (Expression<Func<Volunteer, bool>>) (v => v.VolunteerID == volunteerId));
        //   if (volunteer == null)
        //     return 0;
        //   volunteer.EmailAddress = email;
        //   return this.context.SaveChanges();
        // }

        ///// <summary>
        ///// The update volunteer record with tournament registration id.
        ///// </summary>
        ///// <param name="volunteerId">
        ///// The volunteer id.
        ///// </param>
        ///// <param name="tournamentRegistrationId">
        ///// The tournament registration id.
        ///// </param>
        ///// <param name="errorMessage">
        ///// The error message.
        ///// </param>
        ///// <returns>
        ///// The <see cref="int"/>.
        ///// </returns>
        // public int UpdateVolunteerRecordWithTournamentRegistrationId(
        //   int volunteerId,
        //   int tournamentRegistrationId,
        //   out string errorMessage)
        // {
        //   errorMessage = string.Empty;
        //
        //   // TODO: Check the TeamID.  If it's the same as the currently registering team, just return.
        //   Volunteer volunteer = Queryable.Where<Volunteer>((IQueryable<Volunteer>) this.context.Volunteers, (Expression<Func<Volunteer, bool>>) (v => v.VolunteerID == volunteerId)).First<Volunteer>();
        //   if (volunteer.TeamID.HasValue)
        //   {
        //     if (volunteer.TeamID.Value == tournamentRegistrationId)
        //       return 0;
        //     errorMessage = "The selected volunteer has already been assigned to another team. &nbsp;The webmaster has been notified and you will be contacted about how to complete your registration.";
        //   }
        //   volunteer.TeamID = new int?(tournamentRegistrationId);
        //   volunteer.TimeAssignedToTeam = new DateTime?(DateTime.Now);
        //   return this.context.SaveChanges();
        // }
        // public int UpdateVolunteerRecordWithTournamentRegistrationId(int volunteerId, int tournamentRegistrationId, out string errorMessage)
        // {
        //     // TODO: Check the TeamID.  If it's the same as the currently registering team, just return.
        //     errorMessage = string.Empty;
           
        //     Volunteer volunteerRecord = (from v in this.context.Volunteers
        //         where v.VolunteerID == volunteerId
        //         select v).First();
           
        //     // Has this volunteer already been assigned to a team?
        //     if (volunteerRecord.TeamID != null)
        //     {
        //         // Is this just the Coach backing up in the registration, changing a few values, and trying to
        //         // re-register the team with the same volunteer?  If so, that's ok.
        //         int teamId = (int)volunteerRecord.TeamID;
        //         if (teamId == tournamentRegistrationId)
        //         {
        //             return 0;
        //         }
           
        //         // If this volunteer has already been assigned to another team, it
        //         // must have happened after Page04 was filled in
        //         errorMessage =
        //             "The selected volunteer has already been assigned to another team. &nbsp;" +
        //             "The webmaster has been notified and you will be contacted about how to complete your registration.";
        //     }
           
        //     volunteerRecord.TeamID = tournamentRegistrationId;
        //     volunteerRecord.TimeAssignedToTeam = DateTime.Now;
           
        //     // SaveChanges returns the number of objects added to the database
        //     return this.context.SaveChanges();
        // }


        ///// <summary>
        ///// The get query info.
        ///// TODO: Uncomment all of this (9/19/2013)
        ///// </summary>
        ///// <param name="tableName">
        ///// The table name.
        ///// </param>
        ///// <returns>
        ///// The <see cref="QueryInfo"/>.
        ///// </returns>
        ///// <remarks>
        ///// This QueryInfo code comes from http://blogs.msdn.com/alexj/archive/2008/02/11/rolling-your-own-sql-update-on-top-of-the-entity-framework-part-3.aspx
        ///// </remarks>
        // [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        // private QueryInfo GetQueryInfo(ObjectQuery tableName)
        // {
        //     var info = new QueryInfo
        //                    {
        //                        OriginalSql = tableName.ToTraceString()
        //                    };

        //     // Get the table name
        //     info.TableName = info.OriginalSql.Between("FROM ", "AS [Extent1]")[0].Trim();

        //     // Get the aliases so we can build the map
        //     string[] bits = info.OriginalSql.Split(new[] { "FROM " }, StringSplitOptions.None);

        //     ////Utilities.Assert(
        //     ////    bits.Length == 2,
        //     ////    () => new InvalidOperationException("Unexpectedly complex SQL tree with 2 or more FROM's encountered\n\r" + info.OriginalSQL)
        //     ////);

        //     string trunc = bits[0].Replace("SELECT ", "").Trim();
        //     string[] aliases = trunc.Split(',');
        //     aliases = aliases.Select(a => a.Trim()).ToArray();

        //     // get an array of arrays in the form [DBfieldName][EntityPropertyName],...]
        //     var keyvaluepairs =
        //         aliases.Where(a => a.StartsWith("[Extent1].["))
        //                .Select(a => a.Replace("[Extent1].[", ""))
        //                .Select(a => a.Replace("] AS [", ","))
        //                .Select(a => a.Replace("]", ""))
        //                .Select(a => a.Split(','));

        //     // Set up the map from array
        //     foreach (var keyvaluepair in keyvaluepairs)
        //     {
        //         info.CsFieldMap[keyvaluepair[1]] = keyvaluepair[0];
        //     }

        //     ////Make sure the key is simple
        //     ////Utilities.Assert(Context.Keys.Length == 1,
        //     ////    () => new NotSupportedException("Only works for Entities with simple keys")
        //     ////);

        //     ////Construct an Alias for the SELECTOR
        //     ////string keyAlias = _context.Keys.Select(key => string.Format("[Extent1].[{0}] AS [{1}]", info.CsFieldMap[key], key)).FirstOrDefault();

        //     ////Store the filterSQL
        //     ////info.RestrictingSQL = string.Format("SELECT {0} FROM {1}", keyAlias, bits[1]);

        //     return info;
        // }
    }
}
