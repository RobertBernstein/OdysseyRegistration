// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Page01ViewData.cs" company="Tardis Technologies">
//   Copyright 2015 Tardis Technologies. All rights reserved.
// </copyright>
// <summary>
//   Defines the Page01ViewData type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

// Decompiled with JetBrains decompiler
// Type: OdysseyMvc4.ViewData.TournamentRegistration.Page01ViewData
// Assembly: OdysseyMvc4, Version=1.0.5460.36587, Culture=neutral, PublicKeyToken=null
// MVID: 7B658547-521F-44CB-80FA-52857CB94B72
// Assembly location: C:\Users\rob\OneDrive\Odyssey\OdysseyProd\registration\bin\OdysseyMvc4.dll

using OdysseyMvc2024.Models;
using System;

namespace OdysseyMvc2024.ViewData.TournamentRegistration
{
    /// <summary>
    /// The backing data for Page 01 of the Tournament Registration wizard.
    /// </summary>
    public class Page01ViewData : BaseViewData
    {
        public bool AcceptingPayPal
        {
            get
            {
                bool flag;
                bool.TryParse(Config[nameof(AcceptingPayPal)], out flag);
                return flag;
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

        public string LateTeamRegistrationFee => !string.IsNullOrWhiteSpace(this.TournamentInfo.LateEventCost)
            ? "$" + this.TournamentInfo.LateEventCost
            : string.Empty;

        public string PaymentDueDate => TournamentInfo.PaymentDueDate.HasValue
            ? this.TournamentInfo.PaymentDueDate.Value.ToLongDateString()
            : "TBA";

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
                if (this.Config[nameof(TournamentRegistrationCloseDateTime)] == null)
                {
                    return "TBA";
                }
				
                DateTime closeDateTime;
                DateTime.TryParse(this.Config[nameof(TournamentRegistrationCloseDateTime)], out closeDateTime);
                return closeDateTime.ToLongDateString();
            }
        }
    }
}
