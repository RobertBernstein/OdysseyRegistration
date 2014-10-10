namespace OdysseyMvc4.ViewData.TournamentRegistration
{
    using OdysseyMvc4.Models;
    using OdysseyMvc4.ViewData;
    using System;
    using System.Runtime.CompilerServices;

    public class Page08ViewData : BaseViewData
    {
        public string SchedulingIssues { get; set; }

        public string SpecialConsiderations { get; set; }

        public OdysseyMvc4.Models.TournamentRegistration TournamentRegistration { get; set; }
    }
}

