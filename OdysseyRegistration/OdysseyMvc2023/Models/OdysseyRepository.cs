// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OdysseyRepository.cs" company="Tardis Technologies">
//   Copyright 2014-2024 Tardis Technologies. All rights reserved.
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

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;

namespace OdysseyMvc2023.Models
{
    /// <summary>
    /// The Odyssey registration database repository.
    /// </summary>
    public class OdysseyRepository
    {
        /// <summary>
        /// The database context.
        /// </summary>
        private readonly OdysseyEntities context = new OdysseyEntities();

        /// <summary>
        /// The coaches training info.
        /// </summary>
        //private Event coachesTrainingInfo;

        /// <summary>
        /// The registration system configuration.
        /// </summary>
        private Dictionary<string, string> config;

        /// <summary>
        /// The divisions.
        /// </summary>
        //private IEnumerable<CoachesTrainingDivision> divisions;

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
        private readonly IEnumerable<Problem> problemChoicesWithoutSpontaneous;

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
        //private IEnumerable<CoachesTrainingRegion> regions;

        /// <summary>
        /// The roles.
        /// </summary>
        //private IEnumerable<CoachesTrainingRole> roles;
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

        //public Event CoachesTrainingInfo
        //{
        //  get
        //  {
        //    Event coachesTrainingInfo = this.coachesTrainingInfo;
        //    if (coachesTrainingInfo == null)
        //      coachesTrainingInfo = this.CoachesTrainingInfo = Queryable.Where<Event>((IQueryable<Event>) this.context.Events, (Expression<Func<Event, bool>>) (o => o.EventName.Contains("Coaches") && o.EventName.Contains("Training"))).First<Event>();
        //    return coachesTrainingInfo;
        //  }
        //  set => this.coachesTrainingInfo = value;
        //}

        //public IEnumerable<CoachesTrainingRegistration> CoachesTrainingRegistrations => (IEnumerable<CoachesTrainingRegistration>) Queryable.OrderBy<CoachesTrainingRegistration, int>((IQueryable<CoachesTrainingRegistration>) this.context.CoachesTrainingRegistrations, (Expression<Func<CoachesTrainingRegistration, int>>) (c => c.RegistrationID));


        /// <summary>
        /// Gets the registration system configuration.
        /// </summary>
        public Dictionary<string, string> Config
        {
            get
            {
                // If config is null, run the LINQ query, assign the result to Config as a Dictionary, and return the result.
                if (this.config == null)
                {
                    // TODO: Use this as generated by JetBrains dotPeek or use code copied from old code?
                    //this.Config = Enumerable.ToDictionary<Config, string, string>((IEnumerable<Config>)Queryable
                    //    .Select<Config, Config>((IQueryable<Config>)this.context.Configs,
                    //        (Expression<Func<Config, Config>>)(c => c)),
                    //    (Func<Config, string>)(d => d.Name), (Func<Config, string>)(d => d.Value));
                    this.Config = (from c in this.context.Configs
                                   select c).ToDictionary(d => d.Name, d => d.Value);
                }

                return this.config;
            }

            private set
            {
                this.config = value;
                this.config.Add("EndYear", (int.Parse(this.config["Year"]) + 1).ToString((IFormatProvider)CultureInfo.InvariantCulture));
            }
        }

        //public IEnumerable<CoachesTrainingDivision> Divisions
        //{
        //  get
        //  {
        //    IEnumerable<CoachesTrainingDivision> divisions = this.divisions;
        //    if (divisions == null)
        //      divisions = this.Divisions = (IEnumerable<CoachesTrainingDivision>) Queryable.OrderBy<CoachesTrainingDivision, byte>((IQueryable<CoachesTrainingDivision>) this.context.CoachesTrainingDivisions, (Expression<Func<CoachesTrainingDivision, byte>>) (d => d.ID));
        //    return divisions;
        //  }
        //  private set => this.divisions = value;
        //}


        /// <summary>
        /// Gets the judges.
        /// </summary>
        public IEnumerable<Judge> Judges
        {
            get
            {
                // If judges is null, run the LINQ query, assign the result to Judges, and return the result.
                IEnumerable<Judge> judges = this.judges;
                if (judges == null)
                    judges = this.Judges = (IEnumerable<Judge>)Queryable.OrderBy<Judge, int>((IQueryable<Judge>)this.context.Judges, (Expression<Func<Judge, int>>)(j => j.JudgeID));
                return judges;
            }

            private set => this.judges = value;
        }

        /// <summary>
        /// Gets or sets the judges info.
        /// </summary>
        public Event JudgesInfo
        {
            get
            {
                Event judgesInfo = this.judgesInfo;
                if (judgesInfo == null)
                    judgesInfo = this.JudgesInfo = Queryable.Where<Event>((IQueryable<Event>)this.context.Events, (Expression<Func<Event, bool>>)(o => o.EventName.Contains("Judges") && o.EventName.Contains("Training"))).First<Event>();
                return judgesInfo;
            }
            set => this.judgesInfo = value;
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
                IQueryable<Problem> primaryProblem = this.primaryProblem;
                if (primaryProblem == null)
                    primaryProblem = this.PrimaryProblem = Queryable.Where<Problem>((IQueryable<Problem>)this.context.Problems, (Expression<Func<Problem, bool>>)(p => p.ProblemID == 6));
                return primaryProblem;
            }

            private set => this.primaryProblem = value;
        }

        /// <summary>
        /// Gets the problem choices.
        /// </summary>
        public IEnumerable<Problem> ProblemChoices
        {
            get
            {
                // If problems is null, run the LINQ query, assign the result to Problems, and return the result
                IOrderedQueryable<Problem> orderedQueryable = Queryable.OrderBy<Problem, int>((IQueryable<Problem>)this.context.Problems, (Expression<Func<Problem, int>>)(p => p.ProblemID));
                Problem problem1 = Queryable.FirstOrDefault<Problem>((IQueryable<Problem>)orderedQueryable, (Expression<Func<Problem, bool>>)(problem => problem.ProblemID == 6));
                if (problem1 != null)
                    problem1.ProblemName += " (The Primary Problem)";
                return this.problemChoices ?? (this.ProblemChoices = (IEnumerable<Problem>)orderedQueryable);
            }
            private set => this.problemChoices = value;
        }

        public IEnumerable<Problem> ProblemChoicesWithoutSpontaneous
        {
            get
            {
                IQueryable<Problem> queryable = Queryable.Where<Problem>((IQueryable<Problem>)Queryable.OrderBy<Problem, int>((IQueryable<Problem>)this.context.Problems, (Expression<Func<Problem, int>>)(p => p.ProblemID)), (Expression<Func<Problem, bool>>)(p => p.ProblemName != "Spontaneous"));
                Problem problem1 = Queryable.FirstOrDefault<Problem>(queryable, (Expression<Func<Problem, bool>>)(problem => problem.ProblemID == 6));
                if (problem1 != null)
                    problem1.ProblemName += " (The Primary Problem)";
                return this.problemChoicesWithoutSpontaneous ?? (this.ProblemChoices = (IEnumerable<Problem>)queryable);
            }
        }

        public IEnumerable<Problem> ProblemConflicts
        {
            get
            {
                // If problems is null, run the LINQ query, assign the result to Problems, and return the result.
                IOrderedQueryable<Problem> orderedQueryable = Queryable.OrderBy<Problem, int>(Queryable.Where<Problem>((IQueryable<Problem>)this.context.Problems, (Expression<Func<Problem, bool>>)(p => p.ProblemID != 7)), (Expression<Func<Problem, int>>)(p => p.ProblemID));

                // TODO: When the following was uncommented, " (The Primary Problem)" showed up twice in the same dropdown entry.
                // I have no idea why commenting this out solves the problem!  I need to revisit and fix this.

                ////var primaryProblem = temp.Where(problem => problem.ProblemID == 6).FirstOrDefault();
                ////if (primaryProblem != null)
                ////{
                ////primaryProblem.ProblemName += " (The Primary Problem)";
                ////}

                return this.problemConflicts ?? (this.ProblemConflicts = (IEnumerable<Problem>)orderedQueryable);
            }

            private set => this.problemConflicts = value;
        }

        public IEnumerable<Problem> Problems
        {
            get
            {
                // If problems is null, run the LINQ query, assign the result to Problems, and return the result.
                // - Skip ProblemID 0, which is "Not Specified"
                IEnumerable<Problem> problems = this.problems;
                if (problems == null)
                    problems = this.Problems = (IEnumerable<Problem>)Queryable.OrderBy<Problem, int>(Queryable.Where<Problem>((IQueryable<Problem>)this.context.Problems, (Expression<Func<Problem, bool>>)(p => p.ProblemCategory != (object)null)), (Expression<Func<Problem, int>>)(p => p.ProblemID));
                return problems;
            }

            private set => this.problems = value;
        }

        public IQueryable<Problem> ProblemsWithoutPrimaryOrSpontaneous
        {
            get
            {
                IQueryable<Problem> primaryOrSpontaneous = this.problemsWithoutPrimaryOrSpontaneous;
                if (primaryOrSpontaneous == null)
                    primaryOrSpontaneous = this.ProblemsWithoutPrimaryOrSpontaneous = (IQueryable<Problem>)Queryable.OrderBy<Problem, int>(Queryable.Where<Problem>((IQueryable<Problem>)this.context.Problems, (Expression<Func<Problem, bool>>)(p => p.ProblemID != 0 && p.ProblemID != 6 && p.ProblemID != 7)), (Expression<Func<Problem, int>>)(p => p.ProblemID));
                return primaryOrSpontaneous;
            }
            private set => this.problemsWithoutPrimaryOrSpontaneous = value;
        }

        public IQueryable<Problem> ProblemsWithoutSpontaneous
        {
            get
            {
                IQueryable<Problem> withoutSpontaneous = this.problemsWithoutSpontaneous;
                if (withoutSpontaneous == null)
                    withoutSpontaneous = this.ProblemsWithoutSpontaneous = (IQueryable<Problem>)Queryable.OrderBy<Problem, int>(Queryable.Where<Problem>((IQueryable<Problem>)this.context.Problems, (Expression<Func<Problem, bool>>)(p => p.ProblemID != 0 && p.ProblemID != 7)), (Expression<Func<Problem, int>>)(p => p.ProblemID));
                return withoutSpontaneous;
            }
            private set => this.problemsWithoutSpontaneous = value;
        }

        public string RegionName
        {
            get => this.regionName ?? (this.RegionName = this.Config[nameof(RegionName)]);
            private set => this.regionName = value;
        }

        public string RegionNumber
        {
            get => this.regionNumber ?? (this.RegionNumber = this.Config[nameof(RegionNumber)]);
            private set => this.regionNumber = value;
        }

        //public IEnumerable<CoachesTrainingRegion> Regions
        //{
        //  get
        //  {
        //    IEnumerable<CoachesTrainingRegion> regions = this.regions;
        //    if (regions == null)
        //      regions = this.Regions = (IEnumerable<CoachesTrainingRegion>) Queryable.OrderBy<CoachesTrainingRegion, int>((IQueryable<CoachesTrainingRegion>) this.context.CoachesTrainingRegions, (Expression<Func<CoachesTrainingRegion, int>>) (r => r.ID));
        //    return regions;
        //  }
        //  private set => this.regions = value;
        //}

        //public IEnumerable<CoachesTrainingRole> Roles
        //{
        //  get
        //  {
        //    IEnumerable<CoachesTrainingRole> roles = this.roles;
        //    if (roles == null)
        //      roles = this.Roles = (IEnumerable<CoachesTrainingRole>) Queryable.OrderBy<CoachesTrainingRole, byte>((IQueryable<CoachesTrainingRole>) this.context.CoachesTrainingRoles, (Expression<Func<CoachesTrainingRole, byte>>) (r => r.ID));
        //    return roles;
        //  }
        //  private set => this.roles = value;
        //}

        public IEnumerable Schools
        {
            get
            {
                IEnumerable schools = this.schools;
                if (schools == null)
                    schools = this.Schools = (IEnumerable)Queryable.Select((IQueryable<School>)Queryable.OrderBy<School, string>(Queryable.Where<School>((IQueryable<School>)this.context.Schools, (Expression<Func<School, bool>>)(s => s.Membership_1seen == "yes")), (Expression<Func<School, string>>)(s => s.Name)), s => new
                    {
                        ID = s.ID,
                        Name = s.Name
                    });
                return schools;
            }
            private set => this.schools = value;
        }

        public Event TournamentInfo
        {
            get
            {
                // TODO: Add try/catch.
                // TODO: Add logging for when "NoVA North Regional Tournament" cannot be found in the database and display the current value of any containing "Tournament".
                // TODO: Figure out how to cache this value so we don't query for/set it every time a page is displayed.
                Event tournamentInfo = this.tournamentInfo ?? (this.TournamentInfo = Queryable.Where<Event>((IQueryable<Event>)this.context.Events, (Expression<Func<Event, bool>>)(o => o.EventName.StartsWith(this.RegionName) && o.EventName.Contains("Tournament"))).First<Event>());

                return tournamentInfo;
            }

            set => this.tournamentInfo = value;
        }

        public IQueryable TournamentRegistration => (IQueryable)Queryable.Select<TournamentRegistration, TournamentRegistration>((IQueryable<TournamentRegistration>)this.context.TournamentRegistrations, (Expression<Func<TournamentRegistration, TournamentRegistration>>)(t => t)).AsQueryable<TournamentRegistration>();

        public IEnumerable<TournamentRegistration> TournamentRegistrations => (IEnumerable<TournamentRegistration>)Queryable.OrderBy<TournamentRegistration, int>((IQueryable<TournamentRegistration>)this.context.TournamentRegistrations, (Expression<Func<TournamentRegistration, int>>)(t => t.TeamID));

        public Event VolunteerInfo
        {
            get
            {
                Event volunteerInfo = this.volunteerInfo;
                if (volunteerInfo == null)
                    volunteerInfo = this.VolunteerInfo = Queryable.Where<Event>((IQueryable<Event>)this.context.Events, (Expression<Func<Event, bool>>)(o => o.EventName.Contains("Volunteer"))).First<Event>();
                return volunteerInfo;
            }
            set => this.volunteerInfo = value;
        }

        //public IQueryable Volunteers => (IQueryable) Queryable.Select<Volunteer, Volunteer>((IQueryable<Volunteer>) this.context.Volunteers, (Expression<Func<Volunteer, Volunteer>>) (v => v)).AsQueryable<Volunteer>();

        //public int AddCoachesTrainingRegistration(CoachesTrainingRegistration newRegistration)
        //{
        //  if (newRegistration == null)
        //    return 0;
        //  this.context.CoachesTrainingRegistrations.Add(newRegistration);
        //  return this.context.SaveChanges();
        //}

        public int AddJudge(Judge newJudge)
        {
            if (newJudge == null)
                return 0;
            this.context.Judges.Add(newJudge);
            return this.context.SaveChanges();
        }

        public int AddTournamentRegistration(TournamentRegistration newRegistration)
        {
            if (newRegistration == null)
                return 0;
            this.context.TournamentRegistrations.Add(newRegistration);
            return this.context.SaveChanges();
        }

        //public int AddVolunteer(Volunteer newVolunteer, int? tournamentRegistrationId = null)
        //{
        //  if (newVolunteer == null)
        //    return 0;
        //  newVolunteer.TeamID = tournamentRegistrationId;
        //  this.context.Volunteers.Add(newVolunteer);
        //  return this.context.SaveChanges();
        //}

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
            //Judge judge = Queryable.Where<Judge>((IQueryable<Judge>)this.context.Judges, (Expression<Func<Judge, bool>>)(j => j.JudgeID == judgeId)).FirstOrDefault<Judge>();
            Judge judgeRecord = (from j in context.Judges
                                 where j.JudgeID == judgeId
                                 select j).FirstOrDefault();

            //if (judge == null)
            //    return;
            //judge.TeamID = (string)null;
            //this.context.SaveChanges();

            if (judgeRecord != null)
            {
                judgeRecord.TeamID = null;
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Export the list of registered judges.
        /// </summary>
        /// <returns>
        /// The <see cref="IQueryable"/>.
        /// </returns>
        //public IQueryable<JudgesExport> ExportJudges() => Queryable.Select(Queryable.OrderBy(Queryable.SelectMany(Queryable.GroupJoin((IQueryable<Judge>)this.context.Judges, (IEnumerable<Problem>)this.context.Problems, (Expression<Func<Judge, string>>)(j => j.ProblemCOI1), (Expression<Func<Problem, string>>)(p => p.ProblemID.ToString(CultureInfo.InvariantCulture)), (j, jp) => new
        public IQueryable<JudgesExport> ExportJudges()
        {
            //      j = j,
            //      jp = jp
            //  }), data => data.jp, (data, y) => new
            //  {
            //\u003C\u003Eh__TransparentIdentifier6 = data,
            //      y = y
            //  }), data => j.JudgeID), data => new JudgesExport()

            // Solution for multiple joins came from https://stackoverflow.com/questions/267488/linq-to-sql-multiple-left-outer-joins
            return from j in this.context.Judges
                   join p in this.context.Problems on j.ProblemCOI1 equals p.ProblemID.ToString(CultureInfo
                       .InvariantCulture) into jp
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
                       UserAgent = j.UserAgent
                       //});
                   };
        }

        //public IQueryable<CoachesTrainingRegistration> GetCoachById(int coachId) => Queryable.Where<CoachesTrainingRegistration>((IQueryable<CoachesTrainingRegistration>) this.context.CoachesTrainingRegistrations, (Expression<Func<CoachesTrainingRegistration, bool>>) (c => c.RegistrationID == coachId));

        //public IQueryable<CoachesTrainingRegistration> GetCoachesTrainingRegistrationById(int id) => Queryable.Where<CoachesTrainingRegistration>((IQueryable<CoachesTrainingRegistration>) this.context.CoachesTrainingRegistrations, (Expression<Func<CoachesTrainingRegistration, bool>>) (c => c.RegistrationID == id));

        //public IQueryable<Judge> GetJudgeById(int judgeId) => Queryable.Where<Judge>((IQueryable<Judge>)this.context.Judges, (Expression<Func<Judge, bool>>)(j => j.JudgeID == judgeId));
        public IQueryable<Judge> GetJudgeById(int judgeId)
        {
            return from j in this.context.Judges
                   where j.JudgeID == judgeId
                   select j;
        }

        public IQueryable<Judge> GetJudgeByIdAndName(
            int judgeId,
            string judgeFirstName,
            string judgeLastName)
        {
            //return Queryable.Where<Judge>((IQueryable<Judge>)this.context.Judges, (Expression<Func<Judge, bool>>)(j => j.JudgeID == judgeId && j.FirstName.ToLower() == judgeFirstName.ToLower() && j.LastName.ToLower() == judgeLastName.ToLower()));
            return from j in this.context.Judges
                   where (j.JudgeID == judgeId) && (j.FirstName.ToLower() == judgeFirstName.ToLower()) && (j.LastName.ToLower() == judgeLastName.ToLower())
                   select j;
        }

        public short? GetJudgeIdFromTournamentRegistrationId(int tournamentRegistrationId) => (short?)Queryable.Where<TournamentRegistration>((IQueryable<TournamentRegistration>)this.context.TournamentRegistrations, (Expression<Func<TournamentRegistration, bool>>)(t => t.TeamID == tournamentRegistrationId)).FirstOrDefault<TournamentRegistration>()?.JudgeID;

        public void GetJudgeNameFromJudgeId(
          short? judgeId,
          out string judgeFirstName,
          out string judgeLastName)
        {
            Judge judge = Queryable.Where<Judge>((IQueryable<Judge>)this.context.Judges, (Expression<Func<Judge, bool>>)(j => (int?)j.JudgeID == (int?)judgeId)).FirstOrDefault<Judge>();
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
            TournamentRegistration tournamentRegistration = Queryable.Where<TournamentRegistration>((IQueryable<TournamentRegistration>)this.context.TournamentRegistrations, (Expression<Func<TournamentRegistration, bool>>)(t => t.TeamID == id)).FirstOrDefault<TournamentRegistration>();
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

        public string GetProblemNameFromProblemId(int? problemId)
        {
            try
            {
                if (!problemId.HasValue)
                    return (string)null;
                return Queryable.Where<Problem>((IQueryable<Problem>)this.context.Problems, (Expression<Func<Problem, bool>>)(p => (int?)p.ProblemID == problemId)).FirstOrDefault<Problem>()?.ProblemName;
            }
            catch (Exception ex)
            {
                return (string)null;
            }
        }

        public string GetSchoolNameFromSchoolId(int? schoolId) => Queryable.Where<School>((IQueryable<School>)this.context.Schools, (Expression<Func<School, bool>>)(s => (int?)s.ID == schoolId)).FirstOrDefault<School>()?.Name;

        public TournamentRegistration GetTournamentRegistrationById(
          int tournamentRegistrationId)
        {
            return Queryable.Where<TournamentRegistration>((IQueryable<TournamentRegistration>)this.context.TournamentRegistrations, (Expression<Func<TournamentRegistration, bool>>)(t => t.TeamID == tournamentRegistrationId)).FirstOrDefault<TournamentRegistration>();
        }

        //public Volunteer GetVolunteerById(int? volunteerId) => Queryable.FirstOrDefault<Volunteer>((IQueryable<Volunteer>) this.context.Volunteers, (Expression<Func<Volunteer, bool>>) (v => (int?) v.VolunteerID == volunteerId));

        //public Volunteer GetVolunteerByIdAndName(
        //  int volunteerId,
        //  string volunteerFirstName,
        //  string volunteerLastName)
        //{
        //  return Queryable.FirstOrDefault<Volunteer>((IQueryable<Volunteer>) this.context.Volunteers, (Expression<Func<Volunteer, bool>>) (v => v.VolunteerID == volunteerId && string.Equals(v.FirstName, volunteerFirstName, StringComparison.CurrentCultureIgnoreCase) && string.Equals(v.LastName, volunteerLastName, StringComparison.CurrentCultureIgnoreCase)));
        //}

        public int? GetVolunteerIdFromTournamentRegistrationId(int tournamentRegistrationId) => Queryable.First<TournamentRegistration>((IQueryable<TournamentRegistration>)this.context.TournamentRegistrations, (Expression<Func<TournamentRegistration, bool>>)(t => t.TeamID == tournamentRegistrationId)).VolunteerID;

        public int UpdateJudge(int judgeId, int pageNumber, Judge newRegistrationData)
        {
            IQueryable<Judge> source = Queryable.Where<Judge>((IQueryable<Judge>)this.context.Judges, (Expression<Func<Judge, bool>>)(j => j.JudgeID == judgeId));
            if (!source.Any<Judge>())
                return 0;
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
            return this.context.SaveChanges();
        }

        public int UpdateJudgeEmail(int judgeId, string email)
        {
            IQueryable<Judge> source = Queryable.Where<Judge>((IQueryable<Judge>)this.context.Judges, (Expression<Func<Judge, bool>>)(j => j.JudgeID == judgeId));
            if (!source.Any<Judge>())
                return 0;
            source.First<Judge>().EmailAddress = email;
            return this.context.SaveChanges();
        }

        public int UpdateJudgeRecordWithTournamentRegistrationId(
          short? judgeId,
          int tournamentRegistrationId,
          out string errorMessage)
        {
            errorMessage = string.Empty;
            short? nullable = judgeId;
            if (!(nullable.HasValue ? new int?((int)nullable.GetValueOrDefault()) : new int?()).HasValue)
                return 0;
            Judge judge = Queryable.Where<Judge>((IQueryable<Judge>)this.context.Judges, (Expression<Func<Judge, bool>>)(j => (int?)j.JudgeID == (int?)judgeId)).First<Judge>();
            if (!string.IsNullOrWhiteSpace(judge.TeamID))
            {
                int result;
                if (int.TryParse(judge.TeamID, out result) && result == tournamentRegistrationId)
                    return 0;
                errorMessage = "The selected judge has already been assigned to another team. &nbsp;The webmaster has been notified and you will be contacted about how to complete your registration.";
            }
            judge.TeamID = tournamentRegistrationId.ToString((IFormatProvider)CultureInfo.InvariantCulture);
            judge.TimeAssignedToTeam = new DateTime?(DateTime.Now);
            return this.context.SaveChanges();
        }

        public int UpdateTournamentRegistration(
          int id,
          int pageNumber,
          TournamentRegistration newRegistrationData)
        {
            IQueryable<TournamentRegistration> source = Queryable.Where<TournamentRegistration>((IQueryable<TournamentRegistration>)this.context.TournamentRegistrations, (Expression<Func<TournamentRegistration, bool>>)(r => r.TeamID == id));
            if (!source.Any<TournamentRegistration>())
                return 0;
            switch (pageNumber)
            {
                case 2:
                    source.First<TournamentRegistration>().SchoolID = newRegistrationData.SchoolID;
                    break;
                case 3:
                    source.First<TournamentRegistration>().JudgeID = newRegistrationData.JudgeID;
                    break;
                case 4:
                    source.First<TournamentRegistration>().VolunteerID = newRegistrationData.VolunteerID;
                    break;
                case 5:
                    source.First<TournamentRegistration>().CoachFirstName = newRegistrationData.CoachFirstName;
                    source.First<TournamentRegistration>().CoachLastName = newRegistrationData.CoachLastName;
                    source.First<TournamentRegistration>().CoachAddress = newRegistrationData.CoachAddress;
                    source.First<TournamentRegistration>().CoachCity = newRegistrationData.CoachCity;
                    source.First<TournamentRegistration>().CoachState = newRegistrationData.CoachState;
                    source.First<TournamentRegistration>().CoachZipCode = newRegistrationData.CoachZipCode;
                    source.First<TournamentRegistration>().CoachEveningPhone = newRegistrationData.CoachEveningPhone;
                    source.First<TournamentRegistration>().CoachDaytimePhone = newRegistrationData.CoachDaytimePhone;
                    source.First<TournamentRegistration>().CoachMobilePhone = newRegistrationData.CoachMobilePhone;
                    source.First<TournamentRegistration>().CoachEmailAddress = newRegistrationData.CoachEmailAddress;
                    source.First<TournamentRegistration>().AltCoachFirstName = newRegistrationData.AltCoachFirstName;
                    source.First<TournamentRegistration>().AltCoachLastName = newRegistrationData.AltCoachLastName;
                    source.First<TournamentRegistration>().AltCoachEveningPhone = newRegistrationData.AltCoachEveningPhone;
                    source.First<TournamentRegistration>().AltCoachDaytimePhone = newRegistrationData.AltCoachDaytimePhone;
                    source.First<TournamentRegistration>().AltCoachMobilePhone = newRegistrationData.AltCoachMobilePhone;
                    source.First<TournamentRegistration>().AltCoachEmailAddress = newRegistrationData.AltCoachEmailAddress;
                    break;
                case 6:
                    source.First<TournamentRegistration>().MemberFirstName1 = newRegistrationData.MemberFirstName1;
                    source.First<TournamentRegistration>().MemberLastName1 = newRegistrationData.MemberLastName1;
                    source.First<TournamentRegistration>().MemberGrade1 = newRegistrationData.MemberGrade1;
                    source.First<TournamentRegistration>().MemberFirstName2 = newRegistrationData.MemberFirstName2;
                    source.First<TournamentRegistration>().MemberLastName2 = newRegistrationData.MemberLastName2;
                    source.First<TournamentRegistration>().MemberGrade2 = newRegistrationData.MemberGrade2;
                    source.First<TournamentRegistration>().MemberFirstName3 = newRegistrationData.MemberFirstName3;
                    source.First<TournamentRegistration>().MemberLastName3 = newRegistrationData.MemberLastName3;
                    source.First<TournamentRegistration>().MemberGrade3 = newRegistrationData.MemberGrade3;
                    source.First<TournamentRegistration>().MemberFirstName4 = newRegistrationData.MemberFirstName4;
                    source.First<TournamentRegistration>().MemberLastName4 = newRegistrationData.MemberLastName4;
                    source.First<TournamentRegistration>().MemberGrade4 = newRegistrationData.MemberGrade4;
                    source.First<TournamentRegistration>().MemberFirstName5 = newRegistrationData.MemberFirstName5;
                    source.First<TournamentRegistration>().MemberLastName5 = newRegistrationData.MemberLastName5;
                    source.First<TournamentRegistration>().MemberGrade5 = newRegistrationData.MemberGrade5;
                    source.First<TournamentRegistration>().MemberFirstName6 = newRegistrationData.MemberFirstName6;
                    source.First<TournamentRegistration>().MemberLastName6 = newRegistrationData.MemberLastName6;
                    source.First<TournamentRegistration>().MemberGrade6 = newRegistrationData.MemberGrade6;
                    source.First<TournamentRegistration>().MemberFirstName7 = newRegistrationData.MemberFirstName7;
                    source.First<TournamentRegistration>().MemberLastName7 = newRegistrationData.MemberLastName7;
                    source.First<TournamentRegistration>().MemberGrade7 = newRegistrationData.MemberGrade7;
                    break;
                case 7:
                    source.First<TournamentRegistration>().ProblemID = newRegistrationData.ProblemID;
                    source.First<TournamentRegistration>().Division = newRegistrationData.Division;
                    source.First<TournamentRegistration>().Spontaneous = newRegistrationData.Spontaneous;
                    break;
                case 8:
                    source.First<TournamentRegistration>().SchedulingIssues = newRegistrationData.SchedulingIssues;
                    source.First<TournamentRegistration>().SpecialConsiderations = newRegistrationData.SpecialConsiderations;
                    break;
                case 10:
                    source.First<TournamentRegistration>().TimeRegistered = newRegistrationData.TimeRegistered;
                    break;
            }
            return this.context.SaveChanges();
        }

        //public int UpdateVolunteer(int volunteerId, int pageNumber, Volunteer newRegistrationData)
        //{
        //  Volunteer volunteer = Queryable.FirstOrDefault<Volunteer>((IQueryable<Volunteer>) this.context.Volunteers, (Expression<Func<Volunteer, bool>>) (v => v.VolunteerID == volunteerId));
        //  if (volunteer == null)
        //    return 0;
        //  switch (pageNumber)
        //  {
        //    case 2:
        //      volunteer.FirstName = newRegistrationData.FirstName;
        //      volunteer.LastName = newRegistrationData.LastName;
        //      volunteer.DaytimePhone = newRegistrationData.DaytimePhone;
        //      volunteer.EveningPhone = newRegistrationData.EveningPhone;
        //      volunteer.MobilePhone = newRegistrationData.MobilePhone;
        //      volunteer.EmailAddress = newRegistrationData.EmailAddress;
        //      volunteer.VolunteerWantsToSee = newRegistrationData.VolunteerWantsToSee;
        //      volunteer.Notes = newRegistrationData.Notes;
        //      break;
        //    case 3:
        //      volunteer.TimeRegistered = new DateTime?(DateTime.Now);
        //      break;
        //  }
        //  return this.context.SaveChanges();
        //}

        //public int UpdateVolunteerEmail(int volunteerId, string email)
        //{
        //  Volunteer volunteer = Queryable.FirstOrDefault<Volunteer>((IQueryable<Volunteer>) this.context.Volunteers, (Expression<Func<Volunteer, bool>>) (v => v.VolunteerID == volunteerId));
        //  if (volunteer == null)
        //    return 0;
        //  volunteer.EmailAddress = email;
        //  return this.context.SaveChanges();
        //}

        //public int UpdateVolunteerRecordWithTournamentRegistrationId(
        //  int volunteerId,
        //  int tournamentRegistrationId,
        //  out string errorMessage)
        //{
        //  errorMessage = string.Empty;
        //  Volunteer volunteer = Queryable.Where<Volunteer>((IQueryable<Volunteer>) this.context.Volunteers, (Expression<Func<Volunteer, bool>>) (v => v.VolunteerID == volunteerId)).First<Volunteer>();
        //  if (volunteer.TeamID.HasValue)
        //  {
        //    if (volunteer.TeamID.Value == tournamentRegistrationId)
        //      return 0;
        //    errorMessage = "The selected volunteer has already been assigned to another team. &nbsp;The webmaster has been notified and you will be contacted about how to complete your registration.";
        //  }
        //  volunteer.TeamID = new int?(tournamentRegistrationId);
        //  volunteer.TimeAssignedToTeam = new DateTime?(DateTime.Now);
        //  return this.context.SaveChanges();
        //}
    }
}
