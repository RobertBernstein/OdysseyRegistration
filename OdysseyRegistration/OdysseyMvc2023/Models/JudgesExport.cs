// Decompiled with JetBrains decompiler
// Type: OdysseyMvc4.Models.JudgesExport
// Assembly: OdysseyMvc4, Version=1.0.5460.36587, Culture=neutral, PublicKeyToken=null
// MVID: 7B658547-521F-44CB-80FA-52857CB94B72
// Assembly location: C:\Users\rob\OneDrive\Odyssey\OdysseyProd\registration\bin\OdysseyMvc4.dll

using System;

namespace OdysseyMvc2023.Models
{
    public class JudgesExport
    {
        public int JudgeId { get; set; }

        public string TeamId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public string Address2 { get; set; }

        public string City { get; set; }

        public string StateOrProvince { get; set; }

        public string PostalCode { get; set; }

        public string DaytimePhone { get; set; }

        public string EveningPhone { get; set; }

        public string Email { get; set; }

        public string Notes { get; set; }

        public string WhenAndWhereTrained { get; set; }

        public string ProblemConflictOfInterest1 { get; set; }

        public string ProblemConflictOfInterest2 { get; set; }

        public string ProblemConflictOfInterest3 { get; set; }

        public string ProblemChoice1 { get; set; }

        public string ProblemChoice2 { get; set; }

        public string ProblemChoice3 { get; set; }

        public string TshirtSize { get; set; }

        public string ContinuingEducationUnits { get; set; }

        public string YearsOfLongTermJudgingExperience { get; set; }

        public string YearsOfSpontaneousJudgingExperience { get; set; }

        public DateTime? TimeRegistered { get; set; }

        public DateTime? TimeAssignedToTeam { get; set; }

        public DateTime? TimeRegistrationStarted { get; set; }

        public string UserAgent { get; set; }
    }
}
