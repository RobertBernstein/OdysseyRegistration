﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace OdysseyCoreMvc.Models
{
    public partial class Events
    {
        public int Id { get; set; }
        public string EventName { get; set; }
        public string LocationUrl { get; set; }
        public string LocationUrlcolor { get; set; }
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
        public string InformationUrl { get; set; }
        public string LocationMapUrl { get; set; }
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