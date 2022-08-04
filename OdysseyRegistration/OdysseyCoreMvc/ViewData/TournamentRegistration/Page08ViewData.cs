// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Page08ViewData.cs" company="Tardis Technologies">
//   Copyright 2022 Tardis Technologies. All rights reserved.
// </copyright>
// <summary>
//   Defines the Page08ViewData type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OdysseyCoreMvc.ViewData.TournamentRegistration
{
    public class Page08ViewData : BaseViewData
    {
        public string? SchedulingIssues { get; set; }

        public string? SpecialConsiderations { get; set; }

        public Models.TournamentRegistration? TournamentRegistration { get; set; }
    }
}

