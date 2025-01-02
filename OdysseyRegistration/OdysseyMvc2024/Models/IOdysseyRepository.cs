// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OdysseyRepository.cs" company="Tardis Technologies">
//   Copyright 2014-2024 Tardis Technologies. All rights reserved.
// </copyright>
// <summary>
//   The Odyssey registration database repository.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections;

namespace OdysseyMvc2024.Models
{
    public interface IOdysseyRepository
    {
        Dictionary<string, string>? Config { get; }
        IEnumerable<Judge>? Judges { get; }
        Event? JudgesInfo { get; set; }
        IQueryable<Problem>? PrimaryProblem { get; }
        IEnumerable<Problem> ProblemChoices { get; }
        IEnumerable<Problem> ProblemChoicesWithoutSpontaneous { get; }
        IEnumerable<Problem> ProblemConflicts { get; }
        IEnumerable<Problem>? Problems { get; }
        IQueryable<Problem>? ProblemsWithoutPrimaryOrSpontaneous { get; }
        IQueryable<Problem>? ProblemsWithoutSpontaneous { get; }
        string RegionName { get; }
        string RegionNumber { get; }
        IEnumerable? Schools { get; }
        Event TournamentInfo { get; set; }
        IQueryable TournamentRegistration { get; }
        IEnumerable<TournamentRegistration> TournamentRegistrations { get; }
        // Event? VolunteerInfo { get; }

        int AddJudge(Judge newJudge);
        int AddTournamentRegistration(TournamentRegistration newRegistration);
        void ClearTeamIdFromJudgeRecord(int judgeId, string judgeFirstName, string judgeLastName);
        IQueryable<Judge> GetJudgeById(int judgeId);
        IQueryable<Judge> GetJudgeByIdAndName(int judgeId, string judgeFirstName, string judgeLastName);
        short? GetJudgeIdFromTournamentRegistrationId(int tournamentRegistrationId);
        void GetJudgeNameFromJudgeId(short? judgeId, out string judgeFirstName, out string judgeLastName);
        List<string> GetMemberGradesByRegistration(int id);
        string GetProblemNameFromProblemId(int? problemId);
        string GetSchoolNameFromSchoolId(int? schoolId);
        TournamentRegistration GetTournamentRegistrationById(int tournamentRegistrationId);
        int? GetVolunteerIdFromTournamentRegistrationId(int tournamentRegistrationId);
        int UpdateJudge(int judgeId, int pageNumber, Judge newRegistrationData);
        int UpdateJudgeEmail(int judgeId, string email);
        int UpdateJudgeRecordWithTournamentRegistrationId(short? judgeId, int tournamentRegistrationId, out string errorMessage);
        int UpdateTournamentRegistration(int id, int pageNumber, TournamentRegistration newRegistrationData);
    }
}