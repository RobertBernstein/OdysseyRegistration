// Decompiled with JetBrains decompiler
// Type: OdysseyMvc4.Models.TournamentRegistration
// Assembly: OdysseyMvc4, Version=1.0.5460.36587, Culture=neutral, PublicKeyToken=null
// MVID: 7B658547-521F-44CB-80FA-52857CB94B72
// Assembly location: C:\Users\rob\OneDrive\Odyssey\OdysseyProd\registration\bin\OdysseyMvc4.dll

using System;

namespace OdysseyMvc2023.Models
{
    public class TournamentRegistration
    {
        public int TeamID { get; set; }

        public string MembershipName { get; set; }

        public string MembershipNumber { get; set; }

        public int? ProblemID { get; set; }

        public string Division { get; set; }

        public int? SchoolID { get; set; }

        public string CoachFirstName { get; set; }

        public string CoachLastName { get; set; }

        public string CoachAddress { get; set; }

        public string CoachCity { get; set; }

        public string CoachState { get; set; }

        public string CoachZipCode { get; set; }

        public string CoachEveningPhone { get; set; }

        public string CoachDaytimePhone { get; set; }

        public string CoachMobilePhone { get; set; }

        public string CoachEmailAddress { get; set; }

        public string AltCoachFirstName { get; set; }

        public string AltCoachLastName { get; set; }

        public string AltCoachEveningPhone { get; set; }

        public string AltCoachDaytimePhone { get; set; }

        public string AltCoachMobilePhone { get; set; }

        public string AltCoachEmailAddress { get; set; }

        public string MemberFirstName1 { get; set; }

        public string MemberLastName1 { get; set; }

        public string MemberGrade1 { get; set; }

        public string MemberFirstName2 { get; set; }

        public string MemberLastName2 { get; set; }

        public string MemberGrade2 { get; set; }

        public string MemberFirstName3 { get; set; }

        public string MemberLastName3 { get; set; }

        public string MemberGrade3 { get; set; }

        public string MemberFirstName4 { get; set; }

        public string MemberLastName4 { get; set; }

        public string MemberGrade4 { get; set; }

        public string MemberFirstName5 { get; set; }

        public string MemberLastName5 { get; set; }

        public string MemberGrade5 { get; set; }

        public string MemberFirstName6 { get; set; }

        public string MemberLastName6 { get; set; }

        public string MemberGrade6 { get; set; }

        public string MemberFirstName7 { get; set; }

        public string MemberLastName7 { get; set; }

        public string MemberGrade7 { get; set; }

        public bool? Spontaneous { get; set; }

        public string Notes { get; set; }

        public string SpecialConsiderations { get; set; }

        public string SchedulingIssues { get; set; }

        public short? Paid { get; set; }

        public short? JudgeID { get; set; }

        public string TeamRegistrationFee { get; set; }

        public int? VolunteerID { get; set; }

        public DateTime? TimeRegistrationStarted { get; set; }

        public DateTime? TimeRegistered { get; set; }

        public string UserAgent { get; set; }
    }
}
