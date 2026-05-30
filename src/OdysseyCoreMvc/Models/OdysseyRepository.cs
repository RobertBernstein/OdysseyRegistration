// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OdysseyRepository.cs" company="Tardis Technologies">
//   Copyright 2022 Tardis Technologies. All rights reserved.
// </copyright>
// <summary>
//   The Odyssey registration database repository.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OdysseyCoreMvc.Models
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
    public class OdysseyRepository
    {
        /// <summary>
        /// The database context.
        /// </summary>
        private readonly OdysseyEntities context = new();

        /// <summary>
        /// The coaches training info.
        /// </summary>
        private Events? coachesTrainingInfo;

        /// <summary>
        /// The divisions.
        /// </summary>
        private IEnumerable<CoachesTrainingDivisions>? divisions;

        /// <summary>
        /// The judges.
        /// </summary>
        private IEnumerable<Judges>? judges;

        /// <summary>
        /// The judges info.
        /// </summary>
        private Events? judgesInfo;

        /// <summary>
        /// The primary problem.
        /// </summary>
        private IQueryable<Problem>? primaryProblem;

        /// <summary>
        /// The problem choices.
        /// </summary>
        private IEnumerable<Problem>? problemChoices;

        /// <summary>
        /// The problem choices without spontaneous.
        /// </summary>
        /// TODO: Is this still needed?  Can it be removed?  Why is it never initialized? - Rob, 12/17/2018.
        private IEnumerable<Problem>? problemChoicesWithoutSpontaneous;

        /// <summary>
        /// The problem conflicts.
        /// </summary>
        private IEnumerable<Problem>? problemConflicts;

        /// <summary>
        /// The problems.
        /// </summary>
        private IEnumerable<Problem>? problems;

        /// <summary>
        /// The problems without primary or spontaneous.
        /// </summary>
        private IQueryable<Problem>? problemsWithoutPrimaryOrSpontaneous;

        /// <summary>
        /// The problems without spontaneous.
        /// TODO: Delete?
        /// </summary>
        private IQueryable<Problem> problemsWithoutSpontaneous;

        /// <summary>
        /// The regions.
        /// </summary>
        private IEnumerable<CoachesTrainingRegions> regions;

        /// <summary>
        /// The roles.
        /// </summary>
        private IEnumerable<CoachesTrainingRoles> roles;

        /// <summary>
        /// The schools.
        /// </summary>
        private IEnumerable schools;

        /// <summary>
        /// The volunteer info.
        /// </summary>
        private Events volunteerInfo;

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
        public Events CoachesTrainingInfo
        {
            get
            {
                return this.coachesTrainingInfo ?? (this.CoachesTrainingInfo = (from o in this.context.Events
                                                                                where o.EventName.Contains("Coaches") && o.EventName.Contains("Training")
                                                                                select o).First());
            }

            set
            {
                this.coachesTrainingInfo = value;
            }
        }

        /// <summary>
        /// Gets the coaches training registrations.
        /// </summary>
        public IEnumerable<CoachesTrainingRegistrations> CoachesTrainingRegistrations
        {
            get
            {
                return from c in this.context.CoachesTrainingRegistrations
                       orderby c.RegistrationId
                       select c;
            }
        }

        /// <summary>
        /// Gets the divisions.
        /// </summary>
        public IEnumerable<CoachesTrainingDivisions> Divisions
        {
            get
            {
                // If divisions is null, run the LINQ query, assign the result to Divisions, and return the result.
                return this.divisions ?? (this.Divisions = from d in this.context.CoachesTrainingDivisions
                                                           orderby d.Id
                                                           select d);
            }

            private set
            {
                this.divisions = value;
            }
        }

        /// <summary>
        /// Gets the judges.
        /// </summary>
        public IEnumerable<Judges> Judges
        {
            get
            {
                // If judges is null, run the LINQ query, assign the result to Judges, and return the result.
                return this.judges ?? (this.Judges = from j in this.context.Judges
                                                     orderby j.JudgeId
                                                     select j);
            }

            private set
            {
                this.judges = value;
            }
        }

        /// <summary>
        /// Gets or sets the judges info.
        /// </summary>
        public Events JudgesInfo
        {
            get
            {
                return this.judgesInfo ?? (this.JudgesInfo = (from o in this.context.Events
                                                              where o.EventName.Contains("Judges") && o.EventName.Contains("Training")
                                                              select o).First());
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
                return this.primaryProblem ?? (this.PrimaryProblem = from p in this.context.Problems
                                                                     where p.ProblemId == 6
                                                                     select p);
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
                // If problems is null, run the LINQ query, assign the result to ProblemChoices, and return the result.
                IOrderedQueryable<Problem> temp = from p in this.context.Problems
                                                  orderby p.ProblemId
                                                  select p;

                var thePrimaryProblem = temp.FirstOrDefault(problem => problem.ProblemId == 6);

                if (thePrimaryProblem != null)
                {
                    thePrimaryProblem.ProblemName += " (The Primary Problem)";
                }

                return this.problemChoices ?? (this.ProblemChoices = temp);
            }

            private set => problemChoices = value;
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
                                           orderby p.ProblemId
                                           where p.ProblemName != "Spontaneous"
                                           select p;

                var thePrimaryProblem = temp.FirstOrDefault(problem => problem.ProblemId == 6);

                if (thePrimaryProblem != null)
                {
                    thePrimaryProblem.ProblemName += " (The Primary Problem)";
                }

                // TODO: Look into what this does - Rob, 12/12/2014.
                // TODO: Is this fixed now? - Rob, 01/01/2025.
                return this.problemChoicesWithoutSpontaneous ?? (this.ProblemChoicesWithoutSpontaneous = temp);
            }

            private set => problemChoicesWithoutSpontaneous = value;
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
                                                  where p.ProblemId != 7
                                                  orderby p.ProblemId
                                                  select p;

                // TODO: When the following was uncommented, " (The Primary Problem)" showed up twice in the same dropdown entry.
                // I have no idea why commenting this out solves the problem!  I need to revisit and fix this.

                ////var primaryProblem = temp.Where(problem => problem.ProblemID == 6).FirstOrDefault();
                ////if (primaryProblem != null)
                ////{
                ////primaryProblem.ProblemName += " (The Primary Problem)";
                ////}

                return this.problemConflicts ?? (this.ProblemConflicts = temp);
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
                return this.problems ?? (this.Problems = from p in this.context.Problems
                                                         where p.ProblemCategory != null // not "No Preference" and not "Spontaneous"
                                                         orderby p.ProblemId
                                                         select p);
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
                return this.problemsWithoutPrimaryOrSpontaneous ?? (this.ProblemsWithoutPrimaryOrSpontaneous = from p in this.context.Problems
                                                                                                               where p.ProblemId != 0 && p.ProblemId != 6 && p.ProblemId != 7
                                                                                                               orderby p.ProblemId
                                                                                                               select p);
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
                return this.problemsWithoutSpontaneous ?? (this.ProblemsWithoutSpontaneous = from p in this.context.Problems
                                                                                             where p.ProblemId != 0 && p.ProblemId != 7
                                                                                             orderby p.ProblemId
                                                                                             select p);
            }

            private set
            {
                this.problemsWithoutSpontaneous = value;
            }
        }

        /// <summary>
        /// Gets the regions.
        /// </summary>
        public IEnumerable<CoachesTrainingRegions> Regions
        {
            get
            {
                // If _problems is null, run the LINQ query, assign the result to Problems, and return the result
                // - Skip ProblemID 0, which is "Not Specified"
                return this.regions ?? (this.Regions = from r in this.context.CoachesTrainingRegions
                                                       orderby r.Id
                                                       select r);
            }

            private set
            {
                this.regions = value;
            }
        }

        /// <summary>
        /// Gets the roles.
        /// </summary>
        public IEnumerable<CoachesTrainingRoles> Roles
        {
            get
            {
                // If _problems is null, run the LINQ query, assign the result to Problems, and return the result
                // - Skip ProblemID 0, which is "Not Specified"
                return this.roles ?? (this.Roles = from r in this.context.CoachesTrainingRoles
                                                   orderby r.Id
                                                   select r);
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
                return this.schools ?? (this.Schools = from s in this.context.Schools
                                                       where s.Membership1seen == "yes"
                                                       orderby s.Name
                                                       select new { s.Id, s.Name });
            }

            private set
            {
                this.schools = value;
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
                       orderby t.TeamId
                       select t;
            }
        }

        /// <summary>
        /// Gets or sets the volunteer info.
        /// TODO: Test that this works. I copied it from TournamentInfo - Rob, 12/12/2014.
        /// </summary>
        public Events VolunteerInfo
        {
            get
            {
                return this.volunteerInfo ?? (this.VolunteerInfo = (from o in this.context.Events
                                                                    where o.EventName.Contains("Volunteer")
                                                                    select o).First());

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
        public int AddCoachesTrainingRegistration(CoachesTrainingRegistrations newRegistration)
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
        public int AddJudge(Judges newJudge)
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
        public int AddVolunteer(Volunteers newVolunteer, int? tournamentRegistrationId = null)
        {
            // TODO: Add exception handling.
            if (newVolunteer != null)
            {
                newVolunteer.TeamId = tournamentRegistrationId;
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
            Judges judgeRecord = (from j in this.context.Judges
                                  where j.JudgeId == judgeId
                                  select j).FirstOrDefault();

            if (judgeRecord != null)
            {
                judgeRecord.TeamId = null;
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
                   join p in this.context.Problems on j.ProblemCoi1 equals p.ProblemId.ToString(CultureInfo.InvariantCulture) into jp
                   from y in jp
                   orderby j.JudgeId
                   select new JudgesExport
                              {
                                  JudgeId = j.JudgeId,
                                  TeamId = j.TeamId,
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
                                  ProblemConflictOfInterest2 = j.ProblemCoi2,
                                  ProblemConflictOfInterest3 = j.ProblemCoi3,
                                  ProblemChoice1 = j.ProblemChoice1,
                                  ProblemChoice2 = j.ProblemChoice2,
                                  ProblemChoice3 = j.ProblemChoice3,
                                  TshirtSize = j.TshirtSize,
                                  ContinuingEducationUnits = j.WantsCeucredit,
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
        public IQueryable<CoachesTrainingRegistrations> GetCoachById(int coachId)
        {
            return from c in this.context.CoachesTrainingRegistrations
                   where c.RegistrationId == coachId
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
        public IQueryable<CoachesTrainingRegistrations> GetCoachesTrainingRegistrationById(int id)
        {
            return from c in this.context.CoachesTrainingRegistrations
                   where c.RegistrationId == id
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
        public IQueryable<Judges> GetJudgeById(int judgeId)
        {
            return from j in this.context.Judges
                   where j.JudgeId == judgeId
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
        public IQueryable<Judges> GetJudgeByIdAndName(int judgeId, string judgeFirstName, string judgeLastName)
        {
            return from j in this.context.Judges
                   where (j.JudgeId == judgeId) && (j.FirstName.ToLower() == judgeFirstName.ToLower()) && (j.LastName.ToLower() == judgeLastName.ToLower())
                   select j;
        }

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
                this.context.TournamentRegistrations.FirstOrDefault(t => t.TeamId == tournamentRegistrationId);

            return firstJudgeIdRecordOrDefault?.JudgeId;
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
            Judges judgeRecord = (from j in this.context.Judges
                                 where j.JudgeId == judgeId
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
                                                   where t.TeamId == id
                                                   select t).FirstOrDefault();

            if (registration == null)
            {
                return null;
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
        public string? GetProblemNameFromProblemId(int? problemId)
        {
            try
            {
                // TODO: Verify that this still works - Rob, 12/12/2014.
                if (!problemId.HasValue)
                {
                    return null;
                }

                Problem firstProblemNameRecordOrDefault = (from p in this.context.Problems
                                                           where p.ProblemId == problemId
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
            Schools firstSchoolRecordOrDefault = (from s in this.context.Schools
                                                  where s.Id == schoolId
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
                    where t.TeamId == tournamentRegistrationId
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
        public Volunteers GetVolunteerById(int? volunteerId)
        {
            return this.context.Volunteers.FirstOrDefault(v => v.VolunteerId == volunteerId);
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
        public Volunteers GetVolunteerByIdAndName(int volunteerId, string volunteerFirstName, string volunteerLastName)
        {
            return
                this.context.Volunteers.FirstOrDefault(
                    v =>
                    v.VolunteerId == volunteerId &&
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
            return this.context.TournamentRegistrations.First(t => t.TeamId == tournamentRegistrationId).VolunteerId;
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
        public int UpdateJudge(int judgeId, int pageNumber, Judges newRegistrationData)
        {
            IQueryable<Judges> judgesFound = from j in this.context.Judges
                                            where j.JudgeId == judgeId
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
                    judgesFound.First().ProblemCoi1 = newRegistrationData.ProblemCoi1;
                    judgesFound.First().ProblemCoi2 = newRegistrationData.ProblemCoi2;
                    judgesFound.First().ProblemCoi3 = newRegistrationData.ProblemCoi3;
                    judgesFound.First().YearsOfLongTermJudgingExperience = newRegistrationData.YearsOfLongTermJudgingExperience;
                    judgesFound.First().YearsOfSpontaneousJudgingExperience = newRegistrationData.YearsOfSpontaneousJudgingExperience;
                    judgesFound.First().PreviousPositions = newRegistrationData.PreviousPositions;
                    judgesFound.First().WillingToBeScorechecker = newRegistrationData.WillingToBeScorechecker;
                    judgesFound.First().TshirtSize = newRegistrationData.TshirtSize;
                    judgesFound.First().WantsCeucredit = newRegistrationData.WantsCeucredit;
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
            IQueryable<Judges> judgesFound = from j in this.context.Judges
                                             where j.JudgeId == judgeId
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
                Judges judgeRecord = (from j in this.context.Judges
                                      where j.JudgeId == judgeId
                                      select j).First();

                // Has this judge already been assigned to another team?
                if (!string.IsNullOrWhiteSpace(judgeRecord.TeamId))
                {
                    // Is this just the Coach backing up in the registration, changing a few values, and trying to
                    // re-register the team with the same judge?  If so, that's ok.
                    int teamId;
                    if (int.TryParse(judgeRecord.TeamId, out teamId) && (teamId == tournamentRegistrationId))
                    {
                        return 0;
                    }

                    // If this judge has already been assigned to another team, it must have happened after Page03 was filled in
                    errorMessage =
                        "The selected judge has already been assigned to another team. &nbsp;" +
                        "The webmaster has been notified and you will be contacted about how to complete your registration.";
                }

                judgeRecord.TeamId = tournamentRegistrationId.ToString(CultureInfo.InvariantCulture);
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
            IQueryable<TournamentRegistration> registration = this.context.TournamentRegistrations.Where(r => r.TeamId == id);

            // If no registrations are returned from the database, just return failure.
            if (!registration.Any())
            {
                return 0;
            }

            switch (pageNumber)
            {
                case 2:
                    registration.First().SchoolId = newRegistrationData.SchoolId;
                    break;

                case 3:
                    registration.First().JudgeId = newRegistrationData.JudgeId;
                    break;

                case 4:
                    registration.First().VolunteerId = newRegistrationData.VolunteerId;
                    break;

                case 5:
                    registration.First().CoachFirstName = newRegistrationData.CoachFirstName;
                    registration.First().CoachLastName = newRegistrationData.CoachLastName;
                    registration.First().CoachAddress = newRegistrationData.CoachAddress;
                    registration.First().CoachCity = newRegistrationData.CoachCity;
                    registration.First().CoachState = newRegistrationData.CoachState;
                    registration.First().CoachZipCode = newRegistrationData.CoachZipCode;
                    registration.First().CoachEveningPhone = newRegistrationData.CoachEveningPhone;
                    registration.First().CoachDaytimePhone = newRegistrationData.CoachDaytimePhone;
                    registration.First().CoachMobilePhone = newRegistrationData.CoachMobilePhone;
                    registration.First().CoachEmailAddress = newRegistrationData.CoachEmailAddress;
                    registration.First().AltCoachFirstName = newRegistrationData.AltCoachFirstName;
                    registration.First().AltCoachLastName = newRegistrationData.AltCoachLastName;
                    registration.First().AltCoachEveningPhone = newRegistrationData.AltCoachEveningPhone;
                    registration.First().AltCoachDaytimePhone = newRegistrationData.AltCoachDaytimePhone;
                    registration.First().AltCoachMobilePhone = newRegistrationData.AltCoachMobilePhone;
                    registration.First().AltCoachEmailAddress = newRegistrationData.AltCoachEmailAddress;
                    break;

                case 6:
                    registration.First().MemberFirstName1 = newRegistrationData.MemberFirstName1;
                    registration.First().MemberLastName1 = newRegistrationData.MemberLastName1;
                    registration.First().MemberGrade1 = newRegistrationData.MemberGrade1;
                    registration.First().MemberFirstName2 = newRegistrationData.MemberFirstName2;
                    registration.First().MemberLastName2 = newRegistrationData.MemberLastName2;
                    registration.First().MemberGrade2 = newRegistrationData.MemberGrade2;
                    registration.First().MemberFirstName3 = newRegistrationData.MemberFirstName3;
                    registration.First().MemberLastName3 = newRegistrationData.MemberLastName3;
                    registration.First().MemberGrade3 = newRegistrationData.MemberGrade3;
                    registration.First().MemberFirstName4 = newRegistrationData.MemberFirstName4;
                    registration.First().MemberLastName4 = newRegistrationData.MemberLastName4;
                    registration.First().MemberGrade4 = newRegistrationData.MemberGrade4;
                    registration.First().MemberFirstName5 = newRegistrationData.MemberFirstName5;
                    registration.First().MemberLastName5 = newRegistrationData.MemberLastName5;
                    registration.First().MemberGrade5 = newRegistrationData.MemberGrade5;
                    registration.First().MemberFirstName6 = newRegistrationData.MemberFirstName6;
                    registration.First().MemberLastName6 = newRegistrationData.MemberLastName6;
                    registration.First().MemberGrade6 = newRegistrationData.MemberGrade6;
                    registration.First().MemberFirstName7 = newRegistrationData.MemberFirstName7;
                    registration.First().MemberLastName7 = newRegistrationData.MemberLastName7;
                    registration.First().MemberGrade7 = newRegistrationData.MemberGrade7;
                    break;

                case 7:
                    registration.First().ProblemId = newRegistrationData.ProblemId;
                    registration.First().Division = newRegistrationData.Division;
                    registration.First().Spontaneous = newRegistrationData.Spontaneous;
                    break;

                case 8:
                    registration.First().SchedulingIssues = newRegistrationData.SchedulingIssues;
                    registration.First().SpecialConsiderations = newRegistrationData.SpecialConsiderations;
                    break;

                case 10:
                    registration.First().TimeRegistered = newRegistrationData.TimeRegistered;
                    break;
            }

            // SaveChanges returns the number of objects updated in the database
            return this.context.SaveChanges();
        }

        /// <summary>
        /// Update the Volunteer record in the database.
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
        /// The number of objects updated in the database.
        /// </returns>
        public int UpdateVolunteer(int volunteerId, int pageNumber, Volunteers newRegistrationData)
        {
            Volunteers volunteerFound = this.context.Volunteers.FirstOrDefault(v => v.VolunteerId == volunteerId);

            if (volunteerFound == null)
            {
                return 0;
            }

            switch (pageNumber)
            {
                case 2:
                    // TODO: Can we just say volunteerFound = newRegistrationData?
                    volunteerFound.FirstName = newRegistrationData.FirstName;
                    volunteerFound.LastName = newRegistrationData.LastName;
                    volunteerFound.DaytimePhone = newRegistrationData.DaytimePhone;
                    volunteerFound.EveningPhone = newRegistrationData.EveningPhone;
                    volunteerFound.MobilePhone = newRegistrationData.MobilePhone;
                    volunteerFound.EmailAddress = newRegistrationData.EmailAddress;
                    volunteerFound.VolunteerWantsToSee = newRegistrationData.VolunteerWantsToSee;
                    volunteerFound.Notes = newRegistrationData.Notes;

                    break;

                case 3:
                    volunteerFound.TimeRegistered = DateTime.Now;
                    break;
            }

            // SaveChanges returns the number of objects updated in the database.
            return this.context.SaveChanges();
        }

        /// <summary>
        /// Update the e-mail address for a volunteer in the Volunteer database table.
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
            Volunteers volunteerFound = this.context.Volunteers.FirstOrDefault(v => v.VolunteerId == volunteerId);

            if (volunteerFound == null)
            {
                return 0;
            }

            // Update the volunteer's e-mail address.
            volunteerFound.EmailAddress = email;

            // SaveChanges returns the number of objects updated in the database.
            return this.context.SaveChanges();
        }

        /// <summary>
        /// The update volunteer record with tournament registration id.
        /// </summary>
        /// <param name="volunteerId">
        /// The volunteer id.
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
        public int UpdateVolunteerRecordWithTournamentRegistrationId(int volunteerId, int tournamentRegistrationId, out string errorMessage)
        {
            // TODO: Check the TeamID.  If it's the same as the currently registering team, just return.
            errorMessage = string.Empty;

            Volunteers volunteerRecord = (from v in this.context.Volunteers
                                          where v.VolunteerId == volunteerId
                                          select v).First();

            // Has this volunteer already been assigned to a team?
            if (volunteerRecord.TeamId != null)
            {
                // Is this just the Coach backing up in the registration, changing a few values, and trying to
                // re-register the team with the same volunteer?  If so, that's ok.
                int teamId = (int)volunteerRecord.TeamId;
                if (teamId == tournamentRegistrationId)
                {
                    return 0;
                }

                // If this volunteer has already been assigned to another team, it
                // must have happened after Page04 was filled in
                errorMessage =
                    "The selected volunteer has already been assigned to another team. &nbsp;" +
                    "The webmaster has been notified and you will be contacted about how to complete your registration.";
            }

            volunteerRecord.TeamId = tournamentRegistrationId;
            volunteerRecord.TimeAssignedToTeam = DateTime.Now;

            // SaveChanges returns the number of objects added to the database
            return this.context.SaveChanges();
        }

        /////// <summary>
        /////// The get query info.
        /////// TODO: Uncomment all of this (9/19/2013)
        /////// </summary>
        /////// <param name="tableName">
        /////// The table name.
        /////// </param>
        /////// <returns>
        /////// The <see cref="QueryInfo"/>.
        /////// </returns>
        /////// <remarks>
        /////// This QueryInfo code comes from http://blogs.msdn.com/alexj/archive/2008/02/11/rolling-your-own-sql-update-on-top-of-the-entity-framework-part-3.aspx
        /////// </remarks>
        ////[SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        ////private QueryInfo GetQueryInfo(ObjectQuery tableName)
        ////{
        ////    var info = new QueryInfo
        ////                   {
        ////                       OriginalSql = tableName.ToTraceString()
        ////                   };

        ////    // Get the table name
        ////    info.TableName = info.OriginalSql.Between("FROM ", "AS [Extent1]")[0].Trim();

        ////    // Get the aliases so we can build the map
        ////    string[] bits = info.OriginalSql.Split(new[] { "FROM " }, StringSplitOptions.None);

        ////    ////Utilities.Assert(
        ////    ////    bits.Length == 2,
        ////    ////    () => new InvalidOperationException("Unexpectedly complex SQL tree with 2 or more FROM's encountered\n\r" + info.OriginalSQL)
        ////    ////);

        ////    string trunc = bits[0].Replace("SELECT ", "").Trim();
        ////    string[] aliases = trunc.Split(',');
        ////    aliases = aliases.Select(a => a.Trim()).ToArray();

        ////    // get an array of arrays in the form [DBfieldName][EntityPropertyName],...]
        ////    var keyvaluepairs =
        ////        aliases.Where(a => a.StartsWith("[Extent1].["))
        ////               .Select(a => a.Replace("[Extent1].[", ""))
        ////               .Select(a => a.Replace("] AS [", ","))
        ////               .Select(a => a.Replace("]", ""))
        ////               .Select(a => a.Split(','));

        ////    // Set up the map from array
        ////    foreach (var keyvaluepair in keyvaluepairs)
        ////    {
        ////        info.CsFieldMap[keyvaluepair[1]] = keyvaluepair[0];
        ////    }

        ////    ////Make sure the key is simple
        ////    ////Utilities.Assert(Context.Keys.Length == 1,
        ////    ////    () => new NotSupportedException("Only works for Entities with simple keys")
        ////    ////);

        ////    ////Construct an Alias for the SELECTOR
        ////    ////string keyAlias = _context.Keys.Select(key => string.Format("[Extent1].[{0}] AS [{1}]", info.CsFieldMap[key], key)).FirstOrDefault();

        ////    ////Store the filterSQL
        ////    ////info.RestrictingSQL = string.Format("SELECT {0} FROM {1}", keyAlias, bits[1]);

        ////    return info;
        ////}
    }
}