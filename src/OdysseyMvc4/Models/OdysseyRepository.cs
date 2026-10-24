// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OdysseyRepository.cs" company="Tardis Technologies">
//   Copyright 2021 Tardis Technologies. All rights reserved.
// </copyright>
// <summary>
//   The Odyssey registration database repository.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OdysseyMvc4.Models
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Linq;

    /// <summary>
    /// The Odyssey registration database repository.
    /// </summary>
    public class OdysseyRepository : IOdysseyRepository
    {
        /// <summary>
        /// The database context.
        /// </summary>
        private readonly OdysseyEntities context = new OdysseyEntities();

        /// <summary>
        /// The coaches training info.
        /// </summary>
        private Event coachesTrainingInfo;

        /// <summary>
        /// The registration system configuration.
        /// </summary>
        private IDictionary<string, string> config;

        /// <summary>
        /// The divisions.
        /// </summary>
        private IEnumerable<CoachesTrainingDivision> divisions;

        /// <summary>
        /// The judges.
        /// </summary>
        private IEnumerable<Judge> judges;

        /// <summary>
        /// The judges info.
        /// </summary>
        private Event judgesInfo;

        /// <summary>
        /// The primary problem.
        /// </summary>
        private IQueryable<Problem> primaryProblem;

        /// <summary>
        /// The problem choices.
        /// </summary>
        private IEnumerable<Problem> problemChoices;

        /// <summary>
        /// The problem choices without spontaneous.
        /// </summary>
        /// TODO: Is this still needed?  Can it be removed?  Why is it never initialized? - Rob, 12/17/2018.
        private IEnumerable<Problem> problemChoicesWithoutSpontaneous;

        /// <summary>
        /// The problem conflicts.
        /// </summary>
        private IEnumerable<Problem> problemConflicts;

        /// <summary>
        /// The problems.
        /// </summary>
        private IEnumerable<Problem> problems;

        /// <summary>
        /// The problems without primary or spontaneous.
        /// </summary>
        private IQueryable<Problem> problemsWithoutPrimaryOrSpontaneous;

        /// <summary>
        /// The problems without spontaneous.
        /// TODO: Delete?
        /// </summary>
        private IQueryable<Problem> problemsWithoutSpontaneous;

        /// <summary>
        /// The region name.
        /// </summary>
        private string regionName;

        /// <summary>
        /// The region number.
        /// </summary>
        private string regionNumber;

        /// <summary>
        /// The regions.
        /// </summary>
        private IEnumerable<CoachesTrainingRegion> regions;

        /// <summary>
        /// The roles.
        /// </summary>
        private IEnumerable<CoachesTrainingRole> roles;

        /// <summary>
        /// The schools.
        /// </summary>
        private IEnumerable schools;

        /// <summary>
        /// The tournament info.
        /// </summary>
        private Event tournamentInfo;

        /// <summary>
        /// The volunteer info.
        /// </summary>
        private Event volunteerInfo;

        ///// <summary>
        ///// The get column names.
        ///// TODO: Uncomment these (9/19/2013)
        ///// </summary>
        ///// <param name="tableName">
        ///// The table name.
        ///// </param>
        ///// <returns>
        ///// The <see>
        /////     <cref>string[]</cref>
        ///// </see>
        /////     .
        ///// </returns>
        ////public string[] GetColumnNames(string tableName)
        ////{
        ////    ObjectQuery tableContext;
        ////    switch (tableName)
        ////    {
        ////        case "CoachesTrainingRegistration":
        ////            tableContext = this.context.coaches_training;
        ////            break;

        ////        case "Judges":
        ////            tableContext = this.context.judges;
        ////            break;

        ////        case "TournamentRegistration":
        ////            tableContext = this.context.TournamentRegistrations;
        ////            break;

        ////        case "Volunteers":
        ////            tableContext = this.context.Volunteers;
        ////            break;

        ////        default:
        ////            tableContext = null;
        ////            break;
        ////    }

        ////    var columnNames = new List<string>();
        ////    QueryInfo qi = this.GetQueryInfo(tableContext);
        ////    if (qi.CsFieldMap.Count > 0)
        ////    {
        ////        foreach (var field in qi.CsFieldMap)
        ////        {
        ////            // Handle the case where a column name contains a '?'
        ////            columnNames.Add(field.Key.Replace("?", "_"));
        ////        }
        ////    }

        ////    return columnNames.ToArray();
        ////}

        /// <summary>
        /// Gets or sets the coaches training info.
        /// </summary>
        public Event CoachesTrainingInfo
        {
            get
            {
                if (this.coachesTrainingInfo == null)
                {
                    this.CoachesTrainingInfo = (from o in this.context.Events
                                                where o.EventName.Contains("Coaches") && o.EventName.Contains("Training")
                                                select o).First();
                }

                return this.coachesTrainingInfo;
            }

            set
            {
                this.coachesTrainingInfo = value;
            }
        }

        /// <summary>
        /// Gets the coaches training registrations.
        /// </summary>
        public IEnumerable<CoachesTrainingRegistration> CoachesTrainingRegistrations
        {
            get
            {
                return from c in this.context.CoachesTrainingRegistrations
                       orderby c.RegistrationID
                       select c;
            }
        }

        /// <summary>
        /// Gets the registration system configuration.
        /// </summary>
        public IDictionary<string, string> Config
        {
            get
            {
                // If config is null, run the LINQ query, assign the result to Config as a Dictionary, and return the result.
                if (this.config == null)
                {
                    this.Config = (from c in this.context.Configs
                                   select c).ToDictionary(d => d.Name, d => d.Value, StringComparer.OrdinalIgnoreCase);
                }

                return this.config;
            }

            private set
            {
                this.config = value;
                this.config.Add("EndYear", (int.Parse(this.config["Year"], CultureInfo.InvariantCulture) + 1).ToString(CultureInfo.InvariantCulture));
            }
        }

        /// <summary>
        /// Gets the divisions.
        /// </summary>
        public IEnumerable<CoachesTrainingDivision> Divisions
        {
            get
            {
                // If divisions is null, run the LINQ query, assign the result to Divisions, and return the result.
                if (this.divisions == null)
                {
                    this.Divisions = from d in this.context.CoachesTrainingDivisions
                                     orderby d.ID
                                     select d;
                }

                return this.divisions;
            }

            private set
            {
                this.divisions = value;
            }
        }

        /// <summary>
        /// Gets the judges.
        /// </summary>
        public IEnumerable<Judge> Judges
        {
            get
            {
                // If judges is null, run the LINQ query, assign the result to Judges, and return the result.
                if (this.judges == null)
                {
                    this.Judges = from j in this.context.Judges
                                  orderby j.JudgeID
                                  select j;
                }

                return this.judges;
            }

            private set
            {
                this.judges = value;
            }
        }

        /// <summary>
        /// Gets or sets the judges info.
        /// </summary>
        public Event JudgesInfo
        {
            get
            {
                if (this.judgesInfo == null)
                {
                    this.JudgesInfo = (from o in this.context.Events
                                       where o.EventName.Contains("Judges") && o.EventName.Contains("Training")
                                       select o).First();
                }

                return this.judgesInfo;
            }

            set
            {
                this.judgesInfo = value;
            }
        }

        /// <summary>
        /// Gets the primary problem.
        /// </summary>
        public IQueryable<Problem> PrimaryProblem
        {
            get
            {
                // If primaryProblem is null, run the LINQ query, assign the result to PrimaryProblem, and return the result.
                // - The Primary problem is ProblemID 6
                if (this.primaryProblem == null)
                {
                    this.PrimaryProblem = from p in this.context.Problems
                                          where p.ProblemID == 6
                                          select p;
                }

                return this.primaryProblem;
            }

            private set
            {
                this.primaryProblem = value;
            }
        }

        /// <summary>
        /// Gets the problem choices.
        /// </summary>
        public IEnumerable<Problem> ProblemChoices
        {
            get
            {
                // If problems is null, run the LINQ query, assign the result to Problems, and return the result
                IOrderedQueryable<Problem> temp = from p in this.context.Problems
                                                  orderby p.ProblemID
                                                  select p;

                Problem thePrimaryProblem = temp.FirstOrDefault(problem => problem.ProblemID == 6);
                if (thePrimaryProblem != null)
                {
                    thePrimaryProblem.ProblemName += " (The Primary Problem)";
                }

                if (this.problemChoices == null)
                {
                    this.ProblemChoices = temp;
                }

                return this.problemChoices;
            }

            private set
            {
                this.problemChoices = value;
            }
        }

        /// <summary>
        /// Gets the problem choices without spontaneous.
        /// </summary>
        public IEnumerable<Problem> ProblemChoicesWithoutSpontaneous
        {
            get
            {
                // If problems is null, run the LINQ query, assign the result to Problems, and return the result
                IQueryable<Problem> temp = from p in this.context.Problems
                                           orderby p.ProblemID
                                           where p.ProblemName != "Spontaneous"
                                           select p;

                Problem thePrimaryProblem = temp.FirstOrDefault(problem => problem.ProblemID == 6);
                if (thePrimaryProblem != null)
                {
                    thePrimaryProblem.ProblemName += " (The Primary Problem)";
                }

                // TODO: Look into what this does - Rob, 12/12/2014.
                if (this.problemChoicesWithoutSpontaneous == null)
                {
                    this.problemChoicesWithoutSpontaneous = temp;
                }

                return this.problemChoicesWithoutSpontaneous;
            }
        }

        /// <summary>
        /// Gets the problem conflicts.
        /// </summary>
        public IEnumerable<Problem> ProblemConflicts
        {
            get
            {
                // If problems is null, run the LINQ query, assign the result to Problems, and return the result.
                IOrderedQueryable<Problem> temp = from p in this.context.Problems
                                                  where p.ProblemID != 7
                                                  orderby p.ProblemID
                                                  select p;

                // TODO: When the following was uncommented, " (The Primary Problem)" showed up twice in the same dropdown entry.
                // I have no idea why commenting this out solves the problem!  I need to revisit and fix this.

                ////var primaryProblem = temp.Where(problem => problem.ProblemID == 6).FirstOrDefault();
                ////if (primaryProblem != null)
                ////{
                ////primaryProblem.ProblemName += " (The Primary Problem)";
                ////}

                if (this.problemConflicts == null)
                {
                    this.ProblemConflicts = temp;
                }

                return this.problemConflicts;
            }

            private set
            {
                this.problemConflicts = value;
            }
        }

        /// <summary>
        /// Gets the problems.
        /// </summary>
        public IEnumerable<Problem> Problems
        {
            get
            {
                // If problems is null, run the LINQ query, assign the result to Problems, and return the result.
                // - Skip ProblemID 0, which is "Not Specified"
                if (this.problems == null)
                {
                    this.Problems = from p in this.context.Problems
                                    where p.ProblemCategory != null // not "No Preference" and not "Spontaneous"
                                    orderby p.ProblemID
                                    select p;
                }

                return this.problems;
            }

            private set
            {
                this.problems = value;
            }
        }

        /// <summary>
        /// Gets the problems without primary or spontaneous.
        /// </summary>
        public IQueryable<Problem> ProblemsWithoutPrimaryOrSpontaneous
        {
            get
            {
                // If problems is null, run the LINQ query, assign the result to Problems, and return the result.
                // - Skip ProblemIDs 0 ("Not Specified"), 6 (Primary), and 7 (Spontaneous)
                if (this.problemsWithoutPrimaryOrSpontaneous == null)
                {
                    this.ProblemsWithoutPrimaryOrSpontaneous = from p in this.context.Problems
                                                               where p.ProblemID != 0 && p.ProblemID != 6 && p.ProblemID != 7
                                                               orderby p.ProblemID
                                                               select p;
                }

                return this.problemsWithoutPrimaryOrSpontaneous;
            }

            private set
            {
                this.problemsWithoutPrimaryOrSpontaneous = value;
            }
        }

        /// <summary>
        /// Gets the problems without spontaneous.
        /// </summary>
        public IQueryable<Problem> ProblemsWithoutSpontaneous
        {
            get
            {
                // If problems is null, run the LINQ query, assign the result to Problems, and return the result.
                // - Skip ProblemID 0, which is "Not Specified"
                if (this.problemsWithoutSpontaneous == null)
                {
                    this.ProblemsWithoutSpontaneous = from p in this.context.Problems
                                                      where p.ProblemID != 0 && p.ProblemID != 7
                                                      orderby p.ProblemID
                                                      select p;
                }

                return this.problemsWithoutSpontaneous;
            }

            private set
            {
                this.problemsWithoutSpontaneous = value;
            }
        }

        /// <summary>
        /// Gets the region name.
        /// </summary>
        public string RegionName
        {
            get
            {
                // If _regionName is null, run the LINQ query, assign the result to RegionName, and return the result
                if (this.regionName == null)
                {
                    this.RegionName = this.Config["RegionName"];
                }

                return this.regionName;
            }

            private set
            {
                this.regionName = value;
            }
        }

        /// <summary>
        /// Gets the region number.
        /// </summary>
        public string RegionNumber
        {
            get
            {
                // If _regionName is null, run the LINQ query, assign the result to RegionName, and return the result
                if (this.regionNumber == null)
                {
                    this.RegionNumber = this.Config["RegionNumber"];
                }

                return this.regionNumber;
            }

            private set
            {
                this.regionNumber = value;
            }
        }

        /// <summary>
        /// Gets the regions.
        /// </summary>
        public IEnumerable<CoachesTrainingRegion> Regions
        {
            get
            {
                // If _problems is null, run the LINQ query, assign the result to Problems, and return the result
                // - Skip ProblemID 0, which is "Not Specified"
                if (this.regions == null)
                {
                    this.Regions = from r in this.context.CoachesTrainingRegions
                                   orderby r.ID
                                   select r;
                }

                return this.regions;
            }

            private set
            {
                this.regions = value;
            }
        }

        /// <summary>
        /// Gets the roles.
        /// </summary>
        public IEnumerable<CoachesTrainingRole> Roles
        {
            get
            {
                // If _problems is null, run the LINQ query, assign the result to Problems, and return the result
                // - Skip ProblemID 0, which is "Not Specified"
                if (this.roles == null)
                {
                    this.Roles = from r in this.context.CoachesTrainingRoles
                                 orderby r.ID
                                 select r;
                }

                return this.roles;
            }

            private set
            {
                this.roles = value;
            }
        }

        /// <summary>
        /// Gets the schools.
        /// </summary>
        public IEnumerable Schools
        {
            get
            {
                // If schools is null, run the LINQ query, assign the result to Schools, and return the result.
                if (this.schools == null)
                {
                    this.Schools = from s in this.context.Schools
                                   where s.Membership_1seen == "yes"
                                   orderby s.Name
                                   select new { s.ID, s.Name };
                }

                return this.schools;
            }

            private set
            {
                this.schools = value;
            }
        }

        /// <summary>
        /// Gets or sets the tournament info.
        /// </summary>
        public Event TournamentInfo
        {
            get
            {
                if (this.tournamentInfo == null)
                {
                    this.TournamentInfo = (from o in this.context.Events
                                           where o.EventName.StartsWith(this.RegionName) && o.EventName.Contains("Tournament")
                                           select o).First();
                }

                return this.tournamentInfo;
            }

            set
            {
                this.tournamentInfo = value;
            }
        }

        /// <summary>
        /// Gets the tournament registration.
        /// </summary>
        public IQueryable TournamentRegistration
        {
            get
            {
                return (from t in this.context.TournamentRegistrations
                        select t).AsQueryable();
            }
        }

        /// <summary>
        /// Gets the tournament registrations.
        /// </summary>
        public IEnumerable<TournamentRegistration> TournamentRegistrations
        {
            get
            {
                return from t in this.context.TournamentRegistrations
                       orderby t.TeamID
                       select t;
            }
        }

        /// <summary>
        /// Gets or sets the volunteer info.
        /// TODO: Test that this works. I copied it from TournamentInfo - Rob, 12/12/2014.
        /// </summary>
        public Event VolunteerInfo
        {
            get
            {
                if (this.volunteerInfo == null)
                {
                    this.VolunteerInfo = (from o in this.context.Events
                                          where o.EventName.Contains("Volunteer")
                                          select o).First();
                }

                return this.volunteerInfo;

                // TODO: Do we still need where o.EventName.StartsWith(this.RegionName)? I don't think so - Rob, 12/12/2014.
                ////return this.volunteerInfo ?? (this.VolunteerInfo = (from o in this.context.Events
                ////                                                    where o.EventName.StartsWith(this.RegionName) && o.EventName.Contains("Volunteer")
                ////                                                    select o).First());
            }

            set
            {
                this.volunteerInfo = value;
            }
        }

        /// <summary>
        /// Gets the volunteers.
        /// </summary>
        public IQueryable Volunteers
        {
            get
            {
                return (from v in this.context.Volunteers
                        select v).AsQueryable();
            }
        }

        /// <summary>
        /// The add coaches training registration.
        /// </summary>
        /// <param name="newRegistration">
        /// The new registration.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int AddCoachesTrainingRegistration(CoachesTrainingRegistration newRegistration)
        {
            if (newRegistration != null)
            {
                // SaveChanges returns the number of objects added to the database
                this.context.CoachesTrainingRegistrations.Add(newRegistration);
                return this.context.SaveChanges();
            }

            return 0;
        }

        /// <summary>
        /// Add a judge registration to the judges table.
        /// </summary>
        /// <param name="newJudge">
        /// The new judge.
        /// </param>
        /// <returns>
        /// The number of objects added to the database.
        /// </returns>
        public int AddJudge(Judge newJudge)
        {
            if (newJudge != null)
            {
                // SaveChanges returns the number of objects added to the database
                this.context.Judges.Add(newJudge);
                return this.context.SaveChanges();
            }

            return 0;
        }

        /// <summary>
        /// The add tournament registration.
        /// </summary>
        /// <param name="newRegistration">
        /// The registration.
        /// </param>
        /// <returns>
        /// The <see cref="int"/> representing the number of database records created.
        /// </returns>
        public int AddTournamentRegistration(TournamentRegistration newRegistration)
        {
            if (newRegistration != null)
            {
                // SaveChanges returns the number of objects added to the database
                this.context.TournamentRegistrations.Add(newRegistration);
                return this.context.SaveChanges();
            }

            return 0;
        }

        /// <summary>
        /// Add a volunteer to the Volunteer database table.
        /// </summary>
        /// <param name="newVolunteer">
        /// The volunteer to add to the Volunteer database table.
        /// </param>
        /// <param name="tournamentRegistrationId">
        /// The ID of the team with which this volunteer is associated.
        /// </param>
        /// <returns>
        /// The number of objects added to the database, i.e. the number of database
        /// records created.
        /// </returns>
        public int AddVolunteer(Volunteer newVolunteer, int? tournamentRegistrationId = null)
        {
            // TODO: Add exception handling.
            if (newVolunteer != null)
            {
                newVolunteer.TeamID = tournamentRegistrationId;
                this.context.Volunteers.Add(newVolunteer);
                return this.context.SaveChanges();
            }

            return 0;
        }

        /// <summary>
        /// The clear team id from judge record.
        /// </summary>
        /// <param name="judgeId">
        /// The judge id.
        /// </param>
        /// <param name="judgeFirstName">
        /// The judge first name.
        /// </param>
        /// <param name="judgeLastName">
        /// The judge last name.
        /// </param>
        public void ClearTeamIdFromJudgeRecord(int judgeId, string judgeFirstName, string judgeLastName)
        {
            Judge judgeRecord = (from j in this.context.Judges
                                 where j.JudgeID == judgeId
                                 select j).FirstOrDefault();

            if (judgeRecord != null)
            {
                judgeRecord.TeamID = null;
                this.context.SaveChanges();
            }
        }

        /// <summary>
        /// The export judges.
        /// </summary>
        /// <returns>
        /// The <see cref="IQueryable"/>.
        /// </returns>
        public IQueryable<JudgesExport> ExportJudges()
        {
            // TODO: The p.ProblemID.ToString() below in the join blows up at runtime.  This is a known problem
            // with Linq-to-Entities.  http://stackoverflow.com/questions/1228318/linq-int-to-string
            // I have left it in because it compiles, but at some point, I should figure out how to execute this
            // query properly.

            // Solution for multiple joins came from http://stackoverflow.com/questions/267488/linq-to-sql-multiple-left-outer-joins
            return from j in this.context.Judges
                   join p in this.context.Problems on j.ProblemCOI1 equals p.ProblemID.ToString(CultureInfo.InvariantCulture) into jp
                   from y in jp
                   orderby j.JudgeID
                   select new JudgesExport
                              {
                                  JudgeId = j.JudgeID,
                                  TeamId = j.TeamID,
                                  FirstName = j.FirstName,
                                  LastName = j.LastName,
                                  Address = j.Address,
                                  Address2 = j.AddressLine2,
                                  City = j.City,
                                  StateOrProvince = j.State,
                                  PostalCode = j.ZipCode,
                                  DaytimePhone = j.DaytimePhone,
                                  EveningPhone = j.EveningPhone,
                                  Email = j.EmailAddress,
                                  Notes = j.Notes,
                                  ProblemConflictOfInterest1 = y.ProblemName,
                                  ProblemConflictOfInterest2 = j.ProblemCOI2,
                                  ProblemConflictOfInterest3 = j.ProblemCOI3,
                                  ProblemChoice1 = j.ProblemChoice1,
                                  ProblemChoice2 = j.ProblemChoice2,
                                  ProblemChoice3 = j.ProblemChoice3,
                                  TshirtSize = j.TshirtSize,
                                  ContinuingEducationUnits = j.WantsCEUCredit,
                                  YearsOfLongTermJudgingExperience = j.YearsOfLongTermJudgingExperience,
                                  YearsOfSpontaneousJudgingExperience = j.YearsOfSpontaneousJudgingExperience,
                                  TimeRegistered = j.TimeRegistered,
                                  TimeAssignedToTeam = j.TimeAssignedToTeam,
                                  TimeRegistrationStarted = j.TimeRegistrationStarted,
                                  UserAgent = j.UserAgent,
                              };
        }

        /// <summary>
        /// The get coach by id.
        /// </summary>
        /// <param name="coachId">
        /// The coach id.
        /// </param>
        /// <returns>
        /// The <see cref="IQueryable"/>.
        /// </returns>
        public IQueryable<CoachesTrainingRegistration> GetCoachById(int coachId)
        {
            return from c in this.context.CoachesTrainingRegistrations
                   where c.RegistrationID == coachId
                   select c;
        }

        /// <summary>
        /// The get coaches training registration by id.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="IQueryable"/>.
        /// </returns>
        public IQueryable<CoachesTrainingRegistration> GetCoachesTrainingRegistrationById(int id)
        {
            return from c in this.context.CoachesTrainingRegistrations
                   where c.RegistrationID == id
                   select c;
        }

        /// <summary>
        /// The get judge by id.
        /// </summary>
        /// <param name="judgeId">
        /// The judge id.
        /// </param>
        /// <returns>
        /// The <see cref="IQueryable"/>.
        /// </returns>
        public IQueryable<Judge> GetJudgeById(int judgeId)
        {
            return from j in this.context.Judges
                   where j.JudgeID == judgeId
                   select j;
        }

        /// <summary>
        /// The get judge by id and name.
        /// </summary>
        /// <param name="judgeId">
        /// The judge id.
        /// </param>
        /// <param name="judgeFirstName">
        /// The judge first name.
        /// </param>
        /// <param name="judgeLastName">
        /// The judge last name.
        /// </param>
        /// <returns>
        /// The <see cref="IQueryable"/>.
        /// </returns>
        #pragma warning disable MA0011, RCS1155
        public IQueryable<Judge> GetJudgeByIdAndName(int judgeId, string judgeFirstName, string judgeLastName)
        {
            return from j in this.context.Judges
                   where (j.JudgeID == judgeId) && (j.FirstName.ToLower() == judgeFirstName.ToLower()) && (j.LastName.ToLower() == judgeLastName.ToLower())
                   select j;
        }
#pragma warning restore MA0011, RCS1155

        ///// TODO: Uncomment these (9/19/2013)
        ////public IQueryable<TournamentRegistration> GetTournamentRegistrationRecords(int pageIndex, int pageSize)
        ////{
        ////    var query = from t in _context.TournamentRegistration
        ////                join s in _context.Schools on t.SchoolID equals s.ID
        ////                orderby t.TeamID
        ////                select new { t.TeamID, s.Name };

        ////    return query.Skip(pageIndex * pageSize).Take(pageSize);

        ////}

        ////public IQueryable<MarkAsPaidTournamentRecord> GetTournamentRecordsToMarkAsPaid(int pageIndex, int pageSize)
        ////{
        ////    // Solution for multiple joins came from http://stackoverflow.com/questions/267488/linq-to-sql-multiple-left-outer-joins
        ////    return from t in this.context.TournamentRegistrations
        ////           join s in this.context.Schools on t.SchoolID equals s.ID into ts
        ////           from x in ts
        ////           join p in this.context.problems on t.ProblemID equals p.ProblemID into tp
        ////           from y in tp
        ////           orderby t.TeamID
        ////           select new MarkAsPaidTournamentRecord
        ////                   {
        ////                       TeamId = t.TeamID,
        ////                       ProblemName = y.ProblemName,
        ////                       Division = t.Division,
        ////                       SchoolName = x.Name,
        ////                       CoachFirstName = t.CoachFirstName,
        ////                       CoachLastName = t.CoachLastName,
        ////                       CoachEmail = t.CoachEmail,
        ////                       Paid = t.Paid,
        ////                       TeamRegistrationFee = t.TeamRegistrationFee
        ////                   };
        ////}

        /// <summary>
        /// Get the judge record ID from the tournament registration ID.
        /// </summary>
        /// <param name="tournamentRegistrationId">
        /// The tournament registration ID used to look up the judge ID.
        /// </param>
        /// <returns>
        /// The judge ID as a <see><cref>short?</cref></see>.
        /// </returns>
        public short? GetJudgeIdFromTournamentRegistrationId(int tournamentRegistrationId)
        {
            var firstJudgeIdRecordOrDefault =
                this.context.TournamentRegistrations.FirstOrDefault(t => t.TeamID == tournamentRegistrationId);

            return firstJudgeIdRecordOrDefault?.JudgeID;
        }

        /// <summary>
        /// The get judge name from judge id.
        /// </summary>
        /// <param name="judgeId">
        /// The judge id.
        /// </param>
        /// <param name="judgeFirstName">
        /// The judge first name.  Defaults to null.
        /// </param>
        /// <param name="judgeLastName">
        /// The judge last name.  Defaults to null.
        /// </param>
        public void GetJudgeNameFromJudgeId(short? judgeId, out string judgeFirstName, out string judgeLastName)
        {
            Judge judgeRecord = (from j in this.context.Judges
                                 where j.JudgeID == judgeId
                                 select j).FirstOrDefault();

            if (judgeRecord == null)
            {
                judgeFirstName = null;
                judgeLastName = null;
                return;
            }

            judgeFirstName = judgeRecord.FirstName;
            judgeLastName = judgeRecord.LastName;
        }

        /// <summary>
        /// The get member grades by registration.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see>
        ///     <cref>List</cref>
        /// </see>
        ///     .
        /// </returns>
        public List<string> GetMemberGradesByRegistration(int id)
        {
            TournamentRegistration registration = (from t in this.context.TournamentRegistrations
                                                   where t.TeamID == id
                                                   select t).FirstOrDefault();

            if (registration == null)
            {
                return new List<string>();
            }

            return new List<string>
                       {
                           registration.MemberGrade1,
                           registration.MemberGrade2,
                           registration.MemberGrade3,
                           registration.MemberGrade4,
                           registration.MemberGrade5,
                           registration.MemberGrade6,
                           registration.MemberGrade7
                       };
        }

        /// <summary>
        /// The get problem name from problem id.
        /// </summary>
        /// <param name="problemId">
        /// The problem id.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetProblemNameFromProblemId(int? problemId)
        {
            try
            {
                // TODO: Verify that this still works - Rob, 12/12/2014.
                if (!problemId.HasValue)
                {
                    return null;
                }

                Problem firstProblemNameRecordOrDefault = (from p in this.context.Problems
                                                           where p.ProblemID == problemId
                                                           select p).FirstOrDefault();

                return firstProblemNameRecordOrDefault != null ? firstProblemNameRecordOrDefault.ProblemName : null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// The get school name from school id.
        /// </summary>
        /// <param name="schoolId">
        /// The school id.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetSchoolNameFromSchoolId(int? schoolId)
        {
            School firstSchoolRecordOrDefault = (from s in this.context.Schools
                                                 where s.ID == schoolId
                                                 select s).FirstOrDefault();

            return firstSchoolRecordOrDefault != null ? firstSchoolRecordOrDefault.Name : null;
        }

        /// <summary>
        /// The get tournament registration by id.
        /// </summary>
        /// <param name="tournamentRegistrationId">
        /// The tournament registration id.
        /// </param>
        /// <returns>
        /// The <see cref="TournamentRegistration"/>.
        /// </returns>
        public TournamentRegistration GetTournamentRegistrationById(int tournamentRegistrationId)
        {
            return (from t in this.context.TournamentRegistrations
                    where t.TeamID == tournamentRegistrationId
                    select t).FirstOrDefault();
        }

        /// <summary>
        /// The get volunteer by id.
        /// </summary>
        /// <param name="volunteerId">
        /// The volunteer id.
        /// </param>
        /// <returns>
        /// The <see cref="Volunteer"/>.
        /// </returns>
        public Volunteer GetVolunteerById(int? volunteerId)
        {
            return this.context.Volunteers.FirstOrDefault(v => v.VolunteerID == volunteerId);
        }

        /// <summary>
        /// Return a volunteer record from the database given a specific Volunteer ID,
        /// first name, and last name.
        /// </summary>
        /// <param name="volunteerId">
        /// The volunteer id.
        /// </param>
        /// <param name="volunteerFirstName">
        /// The volunteer first name.
        /// </param>
        /// <param name="volunteerLastName">
        /// The volunteer last name.
        /// </param>
        /// <returns>
        /// The first matching volunteer from the database if found, null otherwise.
        /// </returns>
        /// TODO: Verify that this still works - Rob, 12/12/2014.
        public Volunteer GetVolunteerByIdAndName(int volunteerId, string volunteerFirstName, string volunteerLastName)
        {
            return
                this.context.Volunteers.FirstOrDefault(
                    v =>
                    v.VolunteerID == volunteerId &&
                    string.Equals(v.FirstName, volunteerFirstName, StringComparison.CurrentCultureIgnoreCase) &&
                    string.Equals(v.LastName, volunteerLastName, StringComparison.CurrentCultureIgnoreCase));
        }

        /// <summary>
        /// The get volunteer id from tournament registration id.
        /// </summary>
        /// <param name="tournamentRegistrationId">
        /// The tournament registration id.
        /// </param>
        /// <returns>
        /// The Volunteer ID as an <see>
        ///     <cref>int?</cref>
        /// </see>.
        /// </returns>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        public int? GetVolunteerIdFromTournamentRegistrationId(int tournamentRegistrationId)
        {
            return this.context.TournamentRegistrations.First(t => t.TeamID == tournamentRegistrationId).VolunteerID;
        }

        /// <summary>
        /// The update judge.
        /// </summary>
        /// <param name="judgeId">
        /// The judge id.
        /// </param>
        /// <param name="pageNumber">
        /// The page number.
        /// </param>
        /// <param name="newRegistrationData">
        /// The new registration data.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int UpdateJudge(int judgeId, int pageNumber, Judge newRegistrationData)
        {
            IQueryable<Judge> judgesFound = from j in this.context.Judges
                                            where j.JudgeID == judgeId
                                            select j;

            if (!judgesFound.Any())
            {
                return 0;
            }

            switch (pageNumber)
            {
                case 2:
                    judgesFound.First().FirstName = newRegistrationData.FirstName;
                    judgesFound.First().LastName = newRegistrationData.LastName;
                    judgesFound.First().Address = newRegistrationData.Address;
                    judgesFound.First().AddressLine2 = newRegistrationData.AddressLine2;
                    judgesFound.First().City = newRegistrationData.City;
                    judgesFound.First().State = newRegistrationData.State;
                    judgesFound.First().ZipCode = newRegistrationData.ZipCode;
                    judgesFound.First().EveningPhone = newRegistrationData.EveningPhone;
                    judgesFound.First().DaytimePhone = newRegistrationData.DaytimePhone;
                    judgesFound.First().MobilePhone = newRegistrationData.MobilePhone;
                    judgesFound.First().EmailAddress = newRegistrationData.EmailAddress;
                    judgesFound.First().ProblemChoice1 = newRegistrationData.ProblemChoice1;
                    judgesFound.First().ProblemChoice2 = newRegistrationData.ProblemChoice2;
                    judgesFound.First().ProblemChoice3 = newRegistrationData.ProblemChoice3;
                    judgesFound.First().HasChildrenCompeting = newRegistrationData.HasChildrenCompeting;
                    judgesFound.First().ProblemCOI1 = newRegistrationData.ProblemCOI1;
                    judgesFound.First().ProblemCOI2 = newRegistrationData.ProblemCOI2;
                    judgesFound.First().ProblemCOI3 = newRegistrationData.ProblemCOI3;
                    judgesFound.First().YearsOfLongTermJudgingExperience = newRegistrationData.YearsOfLongTermJudgingExperience;
                    judgesFound.First().YearsOfSpontaneousJudgingExperience = newRegistrationData.YearsOfSpontaneousJudgingExperience;
                    judgesFound.First().PreviousPositions = newRegistrationData.PreviousPositions;
                    judgesFound.First().WillingToBeScorechecker = newRegistrationData.WillingToBeScorechecker;
                    judgesFound.First().TshirtSize = newRegistrationData.TshirtSize;
                    judgesFound.First().WantsCEUCredit = newRegistrationData.WantsCEUCredit;
                    judgesFound.First().Notes = newRegistrationData.Notes;

                    // TODO: Is the following still used? - Rob, 12/12/2014.
                    judgesFound.First().PreviousPositions = newRegistrationData.PreviousPositions;

                    break;

                case 3:
                    judgesFound.First().TimeRegistered = DateTime.Now;
                    break;
            }

            // SaveChanges returns the number of objects updated in the database
            return this.context.SaveChanges();
        }

        /// <summary>
        /// Update the e-mail address for a judge in the judges database table.
        /// </summary>
        /// <param name="judgeId">
        /// The ID of the judge database record to update.
        /// </param>
        /// <param name="email">
        /// The updated e-mail address.
        /// </param>
        /// <returns>
        /// The number of objects updated in the database.
        /// </returns>
        public int UpdateJudgeEmail(int judgeId, string email)
        {
            IQueryable<Judge> judgesFound = from j in this.context.Judges
                                            where j.JudgeID == judgeId
                                            select j;

            if (!judgesFound.Any())
            {
                return 0;
            }

            // Update the judge's e-mail address
            judgesFound.First().EmailAddress = email;

            // SaveChanges returns the number of objects updated in the database
            return this.context.SaveChanges();
        }

        /// <summary>
        /// The update judge record with tournament registration id.
        /// </summary>
        /// <param name="judgeId">
        /// The judge id.
        /// </param>
        /// <param name="tournamentRegistrationId">
        /// The tournament registration id.
        /// </param>
        /// <param name="errorMessage">
        /// The error message.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int UpdateJudgeRecordWithTournamentRegistrationId(short? judgeId, int tournamentRegistrationId, out string errorMessage)
        {
            // TODO: Check the TeamID.  If it's the same as the currently registering team, just return.
            errorMessage = string.Empty;

            // TODO: Can this be replaced by judgeId.HasValue? - Rob, 12/12/2014.
            if (judgeId != null)
            {
                Judge judgeRecord = (from j in this.context.Judges
                                     where j.JudgeID == judgeId
                                     select j).First();

                // Has this judge already been assigned to another team?
                if (!string.IsNullOrWhiteSpace(judgeRecord.TeamID))
                {
                    // Is this just the Coach backing up in the registration, changing a few values, and trying to
                    // re-register the team with the same judge?  If so, that's ok.
                    int teamId;
                    if (int.TryParse(judgeRecord.TeamID, NumberStyles.Integer, CultureInfo.InvariantCulture, out teamId) && (teamId == tournamentRegistrationId))
                    {
                        return 0;
                    }

                    // If this judge has already been assigned to another team, it must have happened after Page03 was filled in
                    errorMessage =
                        "The selected judge has already been assigned to another team. &nbsp;" +
                        "The webmaster has been notified and you will be contacted about how to complete your registration.";
                }

                judgeRecord.TeamID = tournamentRegistrationId.ToString(CultureInfo.InvariantCulture);
                judgeRecord.TimeAssignedToTeam = DateTime.Now;

                // SaveChanges returns the number of objects added to the database
                return this.context.SaveChanges();
            }

            return 0;
        }

        /// <summary>
        /// The update tournament registration.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <param name="pageNumber">
        /// The page number.
        /// </param>
        /// <param name="newRegistrationData">
        /// The new registration data.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int UpdateTournamentRegistration(int id, int pageNumber, TournamentRegistration newRegistrationData)
        {
            IQueryable<TournamentRegistration> registration = this.context.TournamentRegistrations.Where(r => r.TeamID == id);

            if (!registration.Any())
            {
                return 0;
            }

            TournamentRegistration existingRegistration = registration.First();

            switch (pageNumber)
            {
                case 2:
                    ApplyTournamentPageTwoUpdates(existingRegistration, newRegistrationData);
                    break;

                case 3:
                    ApplyTournamentPageThreeUpdates(existingRegistration, newRegistrationData);
                    break;

                case 4:
                    ApplyTournamentPageFourUpdates(existingRegistration, newRegistrationData);
                    break;

                case 5:
                    ApplyTournamentPageFiveUpdates(existingRegistration, newRegistrationData);
                    break;

                case 6:
                    ApplyTournamentPageSixUpdates(existingRegistration, newRegistrationData);
                    break;

                case 7:
                    ApplyTournamentPageSevenUpdates(existingRegistration, newRegistrationData);
                    break;

                case 8:
                    ApplyTournamentPageEightUpdates(existingRegistration, newRegistrationData);
                    break;

                case 10:
                    existingRegistration.TimeRegistered = newRegistrationData.TimeRegistered;
                    break;
            }

            return this.context.SaveChanges();
        }

        private static void ApplyTournamentPageTwoUpdates(TournamentRegistration target, TournamentRegistration source)
        {
            target.SchoolID = source.SchoolID;
        }

        private static void ApplyTournamentPageThreeUpdates(TournamentRegistration target, TournamentRegistration source)
        {
            target.JudgeID = source.JudgeID;
        }

        private static void ApplyTournamentPageFourUpdates(TournamentRegistration target, TournamentRegistration source)
        {
            target.VolunteerID = source.VolunteerID;
        }

        private static void ApplyTournamentPageFiveUpdates(TournamentRegistration target, TournamentRegistration source)
        {
            target.CoachFirstName = source.CoachFirstName;
            target.CoachLastName = source.CoachLastName;
            target.CoachAddress = source.CoachAddress;
            target.CoachCity = source.CoachCity;
            target.CoachState = source.CoachState;
            target.CoachZipCode = source.CoachZipCode;
            target.CoachEveningPhone = source.CoachEveningPhone;
            target.CoachDaytimePhone = source.CoachDaytimePhone;
            target.CoachMobilePhone = source.CoachMobilePhone;
            target.CoachEmailAddress = source.CoachEmailAddress;
            target.AltCoachFirstName = source.AltCoachFirstName;
            target.AltCoachLastName = source.AltCoachLastName;
            target.AltCoachEveningPhone = source.AltCoachEveningPhone;
            target.AltCoachDaytimePhone = source.AltCoachDaytimePhone;
            target.AltCoachMobilePhone = source.AltCoachMobilePhone;
            target.AltCoachEmailAddress = source.AltCoachEmailAddress;
        }

        private static void ApplyTournamentPageSixUpdates(TournamentRegistration target, TournamentRegistration source)
        {
            target.MemberFirstName1 = source.MemberFirstName1;
            target.MemberLastName1 = source.MemberLastName1;
            target.MemberGrade1 = source.MemberGrade1;
            target.MemberFirstName2 = source.MemberFirstName2;
            target.MemberLastName2 = source.MemberLastName2;
            target.MemberGrade2 = source.MemberGrade2;
            target.MemberFirstName3 = source.MemberFirstName3;
            target.MemberLastName3 = source.MemberLastName3;
            target.MemberGrade3 = source.MemberGrade3;
            target.MemberFirstName4 = source.MemberFirstName4;
            target.MemberLastName4 = source.MemberLastName4;
            target.MemberGrade4 = source.MemberGrade4;
            target.MemberFirstName5 = source.MemberFirstName5;
            target.MemberLastName5 = source.MemberLastName5;
            target.MemberGrade5 = source.MemberGrade5;
            target.MemberFirstName6 = source.MemberFirstName6;
            target.MemberLastName6 = source.MemberLastName6;
            target.MemberGrade6 = source.MemberGrade6;
            target.MemberFirstName7 = source.MemberFirstName7;
            target.MemberLastName7 = source.MemberLastName7;
            target.MemberGrade7 = source.MemberGrade7;
        }

        private static void ApplyTournamentPageSevenUpdates(TournamentRegistration target, TournamentRegistration source)
        {
            target.ProblemID = source.ProblemID;
            target.Division = source.Division;
            target.Spontaneous = source.Spontaneous;
        }

        private static void ApplyTournamentPageEightUpdates(TournamentRegistration target, TournamentRegistration source)
        {
            target.SchedulingIssues = source.SchedulingIssues;
            target.SpecialConsiderations = source.SpecialConsiderations;
        }

        /// <summary>
        /// The update volunteer.
        /// </summary>
        /// <param name="volunteerId">
        /// The volunteer id.
        /// </param>
        /// <param name="pageNumber">
        /// The page number.
        /// </param>
        /// <param name="newRegistrationData">
        /// The new registration data.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int UpdateVolunteer(int volunteerId, int pageNumber, Volunteer newRegistrationData)
        {
            Volunteer volunteerFound = this.context.Volunteers.FirstOrDefault(v => v.VolunteerID == volunteerId);

            if (volunteerFound == null)
            {
                return 0;
            }

            switch (pageNumber)
            {
                case 2:
                    volunteerFound.FirstName = newRegistrationData.FirstName;
                    volunteerFound.LastName = newRegistrationData.LastName;
                    volunteerFound.DaytimePhone = newRegistrationData.DaytimePhone;
                    volunteerFound.EveningPhone = newRegistrationData.EveningPhone;
                    volunteerFound.MobilePhone = newRegistrationData.MobilePhone;
                    volunteerFound.EmailAddress = newRegistrationData.EmailAddress;
                    volunteerFound.Notes = newRegistrationData.Notes;
                    volunteerFound.VolunteerWantsToSee = newRegistrationData.VolunteerWantsToSee;
                    break;

                case 3:
                    volunteerFound.TimeRegistered = DateTime.Now;
                    break;
            }

            return this.context.SaveChanges();
        }

        /// <summary>
        /// Update the e-mail address for a volunteer in the volunteers database table.
        /// </summary>
        /// <param name="volunteerId">
        /// The ID of the volunteer database record to update.
        /// </param>
        /// <param name="email">
        /// The updated e-mail address.
        /// </param>
        /// <returns>
        /// The number of objects updated in the database.
        /// </returns>
        public int UpdateVolunteerEmail(int volunteerId, string email)
        {
            Volunteer volunteerFound = this.context.Volunteers.FirstOrDefault(v => v.VolunteerID == volunteerId);

            if (volunteerFound == null)
            {
                return 0;
            }

            volunteerFound.EmailAddress = email;
            return this.context.SaveChanges();
        }
    }
}
