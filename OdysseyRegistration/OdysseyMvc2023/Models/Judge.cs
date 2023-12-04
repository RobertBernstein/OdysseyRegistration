﻿// Decompiled with JetBrains decompiler
// Type: OdysseyMvc4.Models.Judge
// Assembly: OdysseyMvc4, Version=1.0.5460.36587, Culture=neutral, PublicKeyToken=null
// MVID: 7B658547-521F-44CB-80FA-52857CB94B72
// Assembly location: C:\Users\rob\OneDrive\Odyssey\OdysseyProd\registration\bin\OdysseyMvc4.dll

using System;

namespace OdysseyMvc2023.Models
{
    public class Judge
    {
        public int JudgeID { get; set; }

        public string TeamID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public string AddressLine2 { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string ZipCode { get; set; }

        public string DaytimePhone { get; set; }

        public string EveningPhone { get; set; }

        public string MobilePhone { get; set; }

        public string EmailAddress { get; set; }

        public string Notes { get; set; }

        public string ProblemChoice1 { get; set; }

        public string ProblemChoice2 { get; set; }

        public string ProblemChoice3 { get; set; }

        public string HasChildrenCompeting { get; set; }

        public string COI { get; set; }

        public string ProblemCOI1 { get; set; }

        public string ProblemCOI2 { get; set; }

        public string ProblemCOI3 { get; set; }

        public string ProblemAssigned { get; set; }

        public bool? InformationMailed_ { get; set; }

        public bool? AttendedJT_ { get; set; }

        public bool? Active { get; set; }

        public string WillingToBeScorechecker { get; set; }

        public string TshirtSize { get; set; }

        public string WantsCEUCredit { get; set; }

        public string YearsOfLongTermJudgingExperience { get; set; }

        public string YearsOfSpontaneousJudgingExperience { get; set; }

        public string PreviousPositions { get; set; }

        public string ProblemID { get; set; }

        public DateTime? TimeRegistered { get; set; }

        public DateTime? TimeAssignedToTeam { get; set; }

        public DateTime? TimeRegistrationStarted { get; set; }

        public string UserAgent { get; set; }
    }
}
