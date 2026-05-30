// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IOdysseyRepository.cs" company="Tardis Technologies">
//   Copyright 2021 Tardis Technologies. All rights reserved.
// </copyright>
// <summary>
//   Repository pattern interface for all database operations.
//   Supports unit testing via dependency injection.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OdysseyMvc4.Models
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Repository pattern interface for all database operations.
    /// </summary>
    public interface IOdysseyRepository
    {
        Dictionary<string, string> Config { get; }
        Event CoachesTrainingInfo { get; set; }
        IEnumerable<CoachesTrainingDivision> Divisions { get; }
        IEnumerable<Judge> Judges { get; }
        Event JudgesInfo { get; set; }
        IQueryable<Problem> PrimaryProblem { get; }
        IEnumerable<Problem> ProblemChoices { get; }
        IEnumerable<Problem> ProblemChoicesWithoutSpontaneous { get; }
        IEnumerable<Problem> ProblemConflicts { get; }
        IEnumerable<Problem> Problems { get; }
        IQueryable<Problem> ProblemsWithoutPrimaryOrSpontaneous { get; }
        IQueryable<Problem> ProblemsWithoutSpontaneous { get; }
        string RegionName { get; }
        string RegionNumber { get; }
        IEnumerable<CoachesTrainingRegion> Regions { get; }
        IEnumerable<CoachesTrainingRole> Roles { get; }
        IEnumerable Schools { get; }
        Event TournamentInfo { get; set; }
        IQueryable TournamentRegistration { get; }
        IEnumerable<TournamentRegistration> TournamentRegistrations { get; }
        Event VolunteerInfo { get; set; }

        int AddCoachesTrainingRegistration(CoachesTrainingRegistration newRegistration);
        int AddJudge(Judge newJudge);
        int AddTournamentRegistration(TournamentRegistration newRegistration);
        int AddVolunteer(Volunteer newVolunteer, int? tournamentRegistrationId = null);
        void ClearTeamIdFromJudgeRecord(int judgeId, string judgeFirstName, string judgeLastName);
        IQueryable<CoachesTrainingRegistration> GetCoachesTrainingRegistrationById(int id);
        IQueryable<Judge> GetJudgeById(int judgeId);
        IQueryable<Judge> GetJudgeByIdAndName(int judgeId, string judgeFirstName, string judgeLastName);
        short? GetJudgeIdFromTournamentRegistrationId(int tournamentRegistrationId);
        void GetJudgeNameFromJudgeId(short? judgeId, out string judgeFirstName, out string judgeLastName);
        List<string> GetMemberGradesByRegistration(int id);
        string GetProblemNameFromProblemId(int? problemId);
        string GetSchoolNameFromSchoolId(int? schoolId);
        TournamentRegistration GetTournamentRegistrationById(int tournamentRegistrationId);
        Volunteer GetVolunteerById(int? volunteerId);
        Volunteer GetVolunteerByIdAndName(int volunteerId, string volunteerFirstName, string volunteerLastName);
        int UpdateJudge(int judgeId, int pageNumber, Judge newRegistrationData);
        int UpdateJudgeEmail(int judgeId, string email);
        int UpdateJudgeRecordWithTournamentRegistrationId(short? judgeId, int tournamentRegistrationId, out string errorMessage);
        int UpdateTournamentRegistration(int id, int pageNumber, TournamentRegistration newRegistrationData);
        int UpdateVolunteer(int volunteerId, int pageNumber, Volunteer newRegistrationData);
        int UpdateVolunteerEmail(int volunteerId, string email);
    }
}
