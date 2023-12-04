// Decompiled with JetBrains decompiler
// Type: OdysseyMvc4.ViewData.JudgesRegistration.Page03ViewData
// Assembly: OdysseyMvc4, Version=1.0.5460.36587, Culture=neutral, PublicKeyToken=null
// MVID: 7B658547-521F-44CB-80FA-52857CB94B72
// Assembly location: C:\Users\rob\OneDrive\Odyssey\OdysseyProd\registration\bin\OdysseyMvc4.dll

using OdysseyMvc2023.Models;

namespace OdysseyMvc2023.ViewData.JudgesRegistration
{
    public class Page03ViewData : BaseViewData
    {
        public Event JudgesInfo { get; set; }

        public Judge Judge { get; set; }

        public string MailBody { get; set; }

        public string MailErrorMessage { get; set; }

        public bool EmailAddressWasSpecified { get; set; }

        public string ErrorMessage { get; set; }
    }
}
