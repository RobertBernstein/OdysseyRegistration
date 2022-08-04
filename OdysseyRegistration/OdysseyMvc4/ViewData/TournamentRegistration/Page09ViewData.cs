namespace OdysseyMvc4.ViewData.TournamentRegistration
{
    using OdysseyMvc4.Models;
    using OdysseyMvc4.ViewData;
    using System;
    using System.Runtime.CompilerServices;

    public class Page09ViewData : BaseViewData
    {
        public string Division { get; set; }

        public string IsDoingSpontaneous { get; set; }

        public string JudgeFirstName { get; set; }

        public string JudgeLastName { get; set; }

        public string ProblemName { get; set; }

        public string SchoolName { get; set; }

        public OdysseyMvc4.Models.TournamentRegistration TournamentRegistration { get; set; }

        public string VolunteerFirstName { get; set; }

        public string VolunteerLastName { get; set; }
    }
}

