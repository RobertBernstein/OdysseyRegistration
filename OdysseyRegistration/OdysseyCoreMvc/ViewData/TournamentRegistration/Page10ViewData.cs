// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Page10 ViewData.cs" company="Tardis Technologies">
//   Copyright 2022 Tardis Technologies. All rights reserved.
// </copyright>
// <summary>
//   Defines the Page10 ViewData type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OdysseyCoreMvc.ViewData.TournamentRegistration
{
    public class Page10ViewData : BaseViewData
    {
        public bool AcceptingPayPal
        {
            get
            {
                bool.TryParse(base.Config["AcceptingPayPal"], out bool flag);
                return flag;
            }
        }

        public string? Division { get; set; }

        public string? JudgeErrorMessage { get; set; }

        public string? JudgeFirstName { get; set; }

        public string? JudgeLastName { get; set; }

        public string? MailBody { get; set; }

        public string? MailErrorMessage { get; set; }

        public string? PaymentDueDate
        {
            get
            {
                DateTime? paymentDueDate = base.TournamentInfo.PaymentDueDate;
                return (paymentDueDate.HasValue ? paymentDueDate.Value.ToLongDateString() : "TBA");
            }
        }

        public string? ProblemName { get; set; }

        public string? SchoolName { get; set; }

        public Models.TournamentRegistration? TournamentRegistration { get; set; }

        public string? VolunteerErrorMessage { get; set; }

        public string? VolunteerFirstName { get; set; }

        public string? VolunteerLastName { get; set; }
    }
}

