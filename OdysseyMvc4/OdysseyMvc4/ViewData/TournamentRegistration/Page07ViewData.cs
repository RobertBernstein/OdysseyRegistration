namespace OdysseyMvc4.ViewData.TournamentRegistration
{
    using OdysseyMvc4.ViewData;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class Page07ViewData : BaseViewData
    {
        public string Division123AndPrimaryListOfProblemsAsHtmlList { get; set; }

        public string Division123ListOfProblemsAsHtmlList { get; set; }

        public string Division123ProblemChoice { get; set; }

        public IEnumerable<SelectListItem> Division123ProblemDropDown { get; set; }

        public int DivisionOfTeam { get; set; }

        public string DivisionRadioGroup { get; set; }

        public string IsDoingSpontaneous { get; set; }

        public IEnumerable<SelectListItem> IsDoingSpontaneousDropDown { get; set; }

        public string PrimaryProblemName { get; set; }

        public string SelectedProblem { get; set; }
    }
}

