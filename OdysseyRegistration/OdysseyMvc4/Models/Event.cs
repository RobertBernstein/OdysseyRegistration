//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OdysseyMvc4.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Event
    {
        public int ID { get; set; }
        public string EventName { get; set; }
        public string LocationURL { get; set; }
        public string LocationURLColor { get; set; }
        public string LocationAddress { get; set; }
        public string LocationCity { get; set; }
        public string LocationState { get; set; }
        public string LocationPhone { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
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
        public Nullable<System.DateTime> LateEventCostStartDate { get; set; }
        public Nullable<System.DateTime> PaymentDueDate { get; set; }
        public string EventMakeChecksOutTo { get; set; }
        public string EventVolunteerInformationMessage { get; set; }
        public string TeamsVolunteerWantsToSeeMessage { get; set; }
        public string EventMailBody { get; set; }
    }
}