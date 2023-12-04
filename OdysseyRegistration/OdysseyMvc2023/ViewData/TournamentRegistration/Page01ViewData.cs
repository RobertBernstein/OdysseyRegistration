// Decompiled with JetBrains decompiler
// Type: OdysseyMvc4.ViewData.TournamentRegistration.Page01ViewData
// Assembly: OdysseyMvc4, Version=1.0.5460.36587, Culture=neutral, PublicKeyToken=null
// MVID: 7B658547-521F-44CB-80FA-52857CB94B72
// Assembly location: C:\Users\rob\OneDrive\Odyssey\OdysseyProd\registration\bin\OdysseyMvc4.dll

using System;

namespace OdysseyMvc2023.ViewData.TournamentRegistration
{
    public class Page01ViewData : BaseViewData
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

        public string LateEventCostStartDate
        {
            get
            {
                string eventCostStartDate;
                if (this.TournamentInfo.LateEventCostStartDate.HasValue)
                {
                    DateTime dateTime = this.TournamentInfo.LateEventCostStartDate.Value;
                    dateTime = dateTime.AddDays(-1.0);
                    eventCostStartDate = dateTime.ToLongDateString();
                }
                else
                    eventCostStartDate = "TBA";
                return eventCostStartDate;
            }
        }

        public string LateTeamRegistrationFee => !string.IsNullOrWhiteSpace(this.TournamentInfo.LateEventCost) ? "$" + this.TournamentInfo.LateEventCost : string.Empty;

        public string PaymentDueDate => !this.TournamentInfo.PaymentDueDate.HasValue ? "TBA" : this.TournamentInfo.PaymentDueDate.Value.ToLongDateString();

        public string TeamRegistrationFee => !string.IsNullOrWhiteSpace(this.TournamentInfo.EventCost) ? "$" + this.TournamentInfo.EventCost : "TBA";

        public string TournamentRegistrationCloseDateTime
        {
            get
            {
                if (this.Config[nameof(TournamentRegistrationCloseDateTime)] == null)
                    return "TBA";
                DateTime result;
                DateTime.TryParse(this.Config[nameof(TournamentRegistrationCloseDateTime)], out result);
                return result.ToLongDateString();
            }
        }
    }
}
