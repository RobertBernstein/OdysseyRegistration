// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Page09ViewData.cs" company="Tardis Technologies">
//   Copyright 2022 Tardis Technologies. All rights reserved.
// </copyright>
// <summary>
//   Defines the Page09ViewData type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OdysseyCoreMvc.ViewData.TournamentRegistration
{
    public class Page09ViewData : BaseViewData
    {
        public string? Division { get; set; }

        public string? IsDoingSpontaneous { get; set; }

        public string? JudgeFirstName { get; set; }

        public string? JudgeLastName { get; set; }

        public string? ProblemName { get; set; }

        public string? SchoolName { get; set; }

        public Models.TournamentRegistration? TournamentRegistration { get; set; }

        public string? VolunteerFirstName { get; set; }

        public string? VolunteerLastName { get; set; }
    }
}

