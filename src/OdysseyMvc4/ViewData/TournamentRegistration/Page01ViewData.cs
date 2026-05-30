// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Page01ViewData.cs" company="Tardis Technologies">
//   Copyright 2015 Tardis Technologies. All rights reserved.
// </copyright>
// <summary>
//   Defines the Page01ViewData type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OdysseyMvc4.ViewData.TournamentRegistration
{
    using System;

    using OdysseyMvc4.ViewData;

    public class Page01ViewData : BaseViewData
    {
        public bool AcceptingPayPal
        {
            get
            {
                bool flag;
                bool.TryParse(this.Config["AcceptingPayPal"], out flag);
                return flag;
            }
        }

        public string LateEventCostStartDate
        {
            get
            {
                return this.TournamentInfo.LateEventCostStartDate.HasValue
                    ? this.TournamentInfo.LateEventCostStartDate.Value.AddDays(-1.0).ToLongDateString()
                    : "TBA";
            }
        }

        public string LateTeamRegistrationFee
        {
            get
            {
                return !string.IsNullOrWhiteSpace(this.TournamentInfo.LateEventCost)
                           ? "$" + this.TournamentInfo.LateEventCost
                           : string.Empty;
            }
        }

        public string PaymentDueDate
        {
            get
            {
                return this.TournamentInfo.PaymentDueDate.HasValue
                    ? this.TournamentInfo.PaymentDueDate.Value.ToLongDateString()
                    : "TBA";
            }
        }

        /// <summary>
        /// Gets the team registration fee.  If the late registration date has passed, returns the higher, late fee.
        /// </summary>
        public string TeamRegistrationFee
        {
            get
            {
                var eventRegistrationFee = !string.IsNullOrWhiteSpace(this.TournamentInfo.EventCost)
                    ? "$" + this.TournamentInfo.EventCost
                    : "TBA";

                var lateEventRegistrationFee = !string.IsNullOrWhiteSpace(this.TournamentInfo.LateEventCost)
                    ? "$" + this.TournamentInfo.LateEventCost
                    : "TBA";

                // If we have not set a late event cost start date, then the standard registration fee is the only one
                // for this year.
                if (this.TournamentInfo.LateEventCostStartDate == null)
                {
                    return eventRegistrationFee;
                }

                bool registrationFeeIsLate =
                    DateTime.Compare((DateTime)this.TournamentInfo.LateEventCostStartDate, DateTime.Now) < 0;

                return !registrationFeeIsLate ? eventRegistrationFee : lateEventRegistrationFee;
            }
        }

        public string TournamentRegistrationCloseDateTime
        {
            get
            {
                DateTime time;
                if (this.Config["TournamentRegistrationCloseDateTime"] == null)
                {
                    return "TBA";
                }

                DateTime.TryParse(this.Config["TournamentRegistrationCloseDateTime"], out time);
                return time.ToLongDateString();
            }
        }
    }
}
