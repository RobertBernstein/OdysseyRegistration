// Decompiled with JetBrains decompiler
// Type: OdysseyMvc4.ViewData.JudgesRegistration.Page01ViewData
// Assembly: OdysseyMvc4, Version=1.0.5460.36587, Culture=neutral, PublicKeyToken=null
// MVID: 7B658547-521F-44CB-80FA-52857CB94B72
// Assembly location: C:\Users\rob\OneDrive\Odyssey\OdysseyProd\registration\bin\OdysseyMvc4.dll

using OdysseyMvc2023.Models;

namespace OdysseyMvc2023.ViewData.JudgesRegistration
{
    public class Page01ViewData : BaseViewData
    {
        public Event JudgesInfo { get; set; }

        public string JudgesTrainingDate => this.JudgesInfo.StartDate.HasValue ? this.JudgesInfo.StartDate.Value.ToLongDateString() : "TBA";

        public string JudgesTrainingLocation => !string.IsNullOrWhiteSpace(this.JudgesInfo.Location) ? this.JudgesInfo.Location : "TBA";

        public string JudgesTrainingTime => !string.IsNullOrWhiteSpace(this.JudgesInfo.Time) ? this.JudgesInfo.Time : "TBA";

        public string MailRegionalDirectorHyperLink { get; set; }

        public string MailRegionalDirectorHyperLinkText { get; set; }
    }
}
