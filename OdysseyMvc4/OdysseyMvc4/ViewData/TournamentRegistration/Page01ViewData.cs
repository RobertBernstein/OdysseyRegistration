namespace OdysseyMvc4.ViewData.TournamentRegistration
{
    using OdysseyMvc4.ViewData;
    using System;

    public class Page01ViewData : BaseViewData
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

        public string LateEventCostStartDate
        {
            get
            {
                return (!base.TournamentInfo.LateEventCostStartDate.HasValue ? "TBA" : base.TournamentInfo.LateEventCostStartDate.Value.AddDays(-1.0).ToLongDateString());
            }
        }

        public string LateTeamRegistrationFee
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(base.TournamentInfo.LateEventCost))
                {
                    return ("$" + base.TournamentInfo.LateEventCost);
                }
                return string.Empty;
            }
        }

        public string PaymentDueDate
        {
            get
            {
                return (!base.TournamentInfo.PaymentDueDate.HasValue ? "TBA" : base.TournamentInfo.PaymentDueDate.Value.ToLongDateString());
            }
        }

        public string TeamRegistrationFee
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(base.TournamentInfo.EventCost))
                {
                    return ("$" + base.TournamentInfo.EventCost);
                }
                return "TBA";
            }
        }

        public string TournamentRegistrationCloseDateTime
        {
            get
            {
                DateTime time;
                if (base.Config["TournamentRegistrationCloseDateTime"] == null)
                {
                    return "TBA";
                }
                DateTime.TryParse(base.Config["TournamentRegistrationCloseDateTime"], out time);
                return time.ToLongDateString();
            }
        }
    }
}

