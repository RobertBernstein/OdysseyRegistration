// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Page01ViewData.cs" company="Tardis Technologies">
//   Copyright 2022 Tardis Technologies. All rights reserved.
// </copyright>
// <summary>
//   Defines the Page01ViewData type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OdysseyCoreMvc.ViewData.TournamentRegistration
{
    public class Page01ViewData : BaseViewData
    {
        public bool AcceptingPayPal
        {
            get
            {
                bool flag;
                bool.TryParse(Config["AcceptingPayPal"], out flag);
                return flag;
            }
        }

        public string LateEventCostStartDate
        {
            get
            {
                return TournamentInfo.LateEventCostStartDate.HasValue
                    ? TournamentInfo.LateEventCostStartDate.Value.AddDays(-1.0).ToLongDateString()
                    : "TBA";
            }
        }

        public string LateTeamRegistrationFee
        {
            get
            {
                return !string.IsNullOrWhiteSpace(TournamentInfo.LateEventCost)
                           ? "$" + TournamentInfo.LateEventCost
                           : string.Empty;
            }
        }

        public string PaymentDueDate
        {
            get
            {
                return TournamentInfo.PaymentDueDate.HasValue
                    ? TournamentInfo.PaymentDueDate.Value.ToLongDateString()
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
                var eventRegistrationFee = !string.IsNullOrWhiteSpace(TournamentInfo.EventCost)
                    ? "$" + TournamentInfo.EventCost
                    : "TBA";

                var lateEventRegistrationFee = !string.IsNullOrWhiteSpace(TournamentInfo.LateEventCost)
                    ? "$" + TournamentInfo.LateEventCost
                    : "TBA";

                // If we have not set a late event cost start date, then the standard registration fee is the only one
                // for this year.
                if (TournamentInfo.LateEventCostStartDate == null)
                {
                    return eventRegistrationFee;
                }

                bool registrationFeeIsLate =
                    DateTime.Compare((DateTime)TournamentInfo.LateEventCostStartDate, DateTime.Now) < 0;

                return !registrationFeeIsLate ? eventRegistrationFee : lateEventRegistrationFee;
            }
        }

        public string TournamentRegistrationCloseDateTime
        {
            get
            {
                DateTime time;
                if (Config["TournamentRegistrationCloseDateTime"] == null)
                {
                    return "TBA";
                }

                DateTime.TryParse(Config["TournamentRegistrationCloseDateTime"], out time);
                return time.ToLongDateString();
            }
        }
    }
}
