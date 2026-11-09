// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PaymentStatusReportViewData.cs" company="Tardis Technologies">
//   Copyright 2026 Tardis Technologies. All rights reserved.
// </copyright>
// <summary>
//   View models for the tournament registration payment status report.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OdysseyMvc2024.ViewData.TournamentRegistration
{
    public class PaymentStatusReportViewData : BaseViewData
    {
        public const string AllFilterValue = "all";
        public const string PaidFilterValue = "paid";
        public const string UnpaidFilterValue = "unpaid";

        public required string PaidFilter { get; set; }

        public required IReadOnlyList<PaymentStatusReportItemViewData> Rows { get; set; }
    }

    public class PaymentStatusReportItemViewData
    {
        public string CoachLastName { get; set; } = string.Empty;

        public bool Paid { get; set; }

        public string ProblemName { get; set; } = string.Empty;

        public string SchoolName { get; set; } = string.Empty;

        public int TeamId { get; set; }

        public DateTime? TimeRegistered { get; set; }
    }
}
