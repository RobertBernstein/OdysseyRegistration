// Decompiled with JetBrains decompiler
// Type: OdysseyMvc4.ViewData.TournamentRegistration.Page10ViewData
// Assembly: OdysseyMvc4, Version=1.0.5460.36587, Culture=neutral, PublicKeyToken=null
// MVID: 7B658547-521F-44CB-80FA-52857CB94B72
// Assembly location: C:\Users\rob\OneDrive\Odyssey\OdysseyProd\registration\bin\OdysseyMvc4.dll

using System;

namespace OdysseyMvc2023.ViewData.TournamentRegistration
{
    public class Page10ViewData : BaseViewData
    {
        public bool AcceptingPayPal
        {
            get
            {
                bool result;
                bool.TryParse(this.Config[nameof(AcceptingPayPal)], out result);
                return result;
            }
        }

        public string Division { get; set; }

        public string JudgeErrorMessage { get; set; }

        public string JudgeFirstName { get; set; }

        public string JudgeLastName { get; set; }

        public string MailBody { get; set; }

        public string MailErrorMessage { get; set; }

        public string PaymentDueDate
        {
            get
            {
                DateTime? paymentDueDate = this.TournamentInfo.PaymentDueDate;
                return paymentDueDate.HasValue ? paymentDueDate.Value.ToLongDateString() : "TBA";
            }
        }

        public string ProblemName { get; set; }

        public string SchoolName { get; set; }

        public Models.TournamentRegistration TournamentRegistration { get; set; }

        public string VolunteerErrorMessage { get; set; }

        public string VolunteerFirstName { get; set; }

        public string VolunteerLastName { get; set; }
    }
}
