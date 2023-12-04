// Decompiled with JetBrains decompiler
// Type: OdysseyMvc4.ViewData.TournamentRegistration.Page07ViewData
// Assembly: OdysseyMvc4, Version=1.0.5460.36587, Culture=neutral, PublicKeyToken=null
// MVID: 7B658547-521F-44CB-80FA-52857CB94B72
// Assembly location: C:\Users\rob\OneDrive\Odyssey\OdysseyProd\registration\bin\OdysseyMvc4.dll

using System.Collections.Generic;
using System.Web.Mvc;

namespace OdysseyMvc2023.ViewData.TournamentRegistration
{
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
