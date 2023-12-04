﻿// Decompiled with JetBrains decompiler
// Type: OdysseyMvc4.Models.Event
// Assembly: OdysseyMvc4, Version=1.0.5460.36587, Culture=neutral, PublicKeyToken=null
// MVID: 7B658547-521F-44CB-80FA-52857CB94B72
// Assembly location: C:\Users\rob\OneDrive\Odyssey\OdysseyProd\registration\bin\OdysseyMvc4.dll

using System;

namespace OdysseyMvc2023.Models
{
    public class Event
    {
        public int ID { get; set; }

        public string EventName { get; set; }

        public string LocationURL { get; set; }

        public string LocationURLColor { get; set; }

        public string LocationAddress { get; set; }

        public string LocationCity { get; set; }

        public string LocationState { get; set; }

        public string LocationPhone { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string Location { get; set; }

        public string Time { get; set; }

        public string EventCoordinatorName { get; set; }

        public string EventCoordinatorEmail { get; set; }

        public string EventCoordinatorPhone { get; set; }

        public string InformationURL { get; set; }

        public string LocationMapURL { get; set; }

        public string EventPayeeName { get; set; }

        public string EventPayeeAddress1 { get; set; }

        public string EventPayeeAddress2 { get; set; }

        public string EventPayeeCity { get; set; }

        public string EventPayeeState { get; set; }

        public string EventPayeeZipCode { get; set; }

        public string EventPayeePhone1 { get; set; }

        public string EventPayeeEmail1 { get; set; }

        public string EventCost { get; set; }

        public string LateEventCost { get; set; }

        public DateTime? LateEventCostStartDate { get; set; }

        public DateTime? PaymentDueDate { get; set; }

        public string EventMakeChecksOutTo { get; set; }

        public string EventVolunteerInformationMessage { get; set; }

        public string TeamsVolunteerWantsToSeeMessage { get; set; }

        public string EventMailBody { get; set; }
    }
}
