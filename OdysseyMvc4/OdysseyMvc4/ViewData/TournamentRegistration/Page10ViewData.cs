namespace OdysseyMvc4.ViewData.TournamentRegistration
{
    using OdysseyMvc4.Models;
    using OdysseyMvc4.ViewData;
    using System;
    using System.Runtime.CompilerServices;

    public class Page10ViewData : BaseViewData
    {
        public bool AcceptingPayPal
        {
            get
            {
                bool flag;
                bool.TryParse(base.Config["AcceptingPayPal"], out flag);
                return flag;
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
                DateTime? paymentDueDate = base.TournamentInfo.PaymentDueDate;
                return (paymentDueDate.HasValue ? paymentDueDate.Value.ToLongDateString() : "TBA");
            }
        }

        public string ProblemName { get; set; }

        public string SchoolName { get; set; }

        public OdysseyMvc4.Models.TournamentRegistration TournamentRegistration { get; set; }

        public string VolunteerErrorMessage { get; set; }

        public string VolunteerFirstName { get; set; }

        public string VolunteerLastName { get; set; }
    }
}

