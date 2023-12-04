// Decompiled with JetBrains decompiler
// Type: OdysseyMvc4.ViewData.BaseViewData
// Assembly: OdysseyMvc4, Version=1.0.5460.36587, Culture=neutral, PublicKeyToken=null
// MVID: 7B658547-521F-44CB-80FA-52857CB94B72
// Assembly location: C:\Users\rob\OneDrive\Odyssey\OdysseyProd\registration\bin\OdysseyMvc4.dll

using System.Collections.Generic;
using OdysseyMvc2023.Models;

namespace OdysseyMvc2023.ViewData
{
    public class BaseViewData
    {
        public Dictionary<string, string> Config { get; set; }

        public string FriendlyRegistrationName { get; set; }

        public string PathToSiteCssFile { get; set; }

        public string RegionName { get; set; }

        public string RegionNumber { get; set; }

        public string SiteName { get; set; }

        public string TournamentDate => this.TournamentInfo.StartDate.HasValue ? this.TournamentInfo.StartDate.Value.ToLongDateString() : "TBA";

        public Event TournamentInfo { get; set; }

        public string TournamentLocation => !string.IsNullOrWhiteSpace(this.TournamentInfo.Location) ? this.TournamentInfo.Location : "TBA";

        public string TournamentTime => !string.IsNullOrWhiteSpace(this.TournamentInfo.Time) ? this.TournamentInfo.Time : "TBA";
    }
}
