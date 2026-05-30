// --------------------------------------------------------------------------------------------------------------------
// <copyright file="JudgesExport.cs" company="Tardis Technologies">
//   Copyright 2013 Tardis Technologies. All rights reserved.
// </copyright>
// <summary>
//   The judges export.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

// Decompiled with JetBrains decompiler
// Type: OdysseyMvc4.Models.JudgesExport
// Assembly: OdysseyMvc4, Version=1.0.5460.36587, Culture=neutral, PublicKeyToken=null
// MVID: 7B658547-521F-44CB-80FA-52857CB94B72
// Assembly location: C:\Users\rob\OneDrive\Odyssey\OdysseyProd\registration\bin\OdysseyMvc4.dll

using System;

namespace OdysseyMvc2023.Models
{
    /// <summary>
    /// The judges export.
    /// </summary>
    public class JudgesExport
    {
        /// <summary>
        /// Gets or sets the judge id.
        /// </summary>
        public int JudgeId { get; set; }

        /// <summary>
        /// Gets or sets the team id.
        /// </summary>
        public string TeamId { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the address 2.
        /// </summary>
        public string Address2 { get; set; }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the state or province.
        /// </summary>
        public string StateOrProvince { get; set; }

        /// <summary>
        /// Gets or sets the postal code.
        /// </summary>
        public string PostalCode { get; set; }

        /// <summary>
        /// Gets or sets the daytime phone.
        /// </summary>
        public string DaytimePhone { get; set; }

        /// <summary>
        /// Gets or sets the evening phone.
        /// </summary>
        public string EveningPhone { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the notes.
        /// </summary>
        public string Notes { get; set; }

        /// <summary>
        /// Gets or sets the when and where trained.
        /// </summary>
        public string WhenAndWhereTrained { get; set; }

        /// <summary>
        /// Gets or sets the problem conflict of interest 1.
        /// </summary>
        public string ProblemConflictOfInterest1 { get; set; }

        /// <summary>
        /// Gets or sets the problem conflict of interest 2.
        /// </summary>
        public string ProblemConflictOfInterest2 { get; set; }

        /// <summary>
        /// Gets or sets the problem conflict of interest 3.
        /// </summary>
        public string ProblemConflictOfInterest3 { get; set; }

        /// <summary>
        /// Gets or sets the problem choice 1.
        /// </summary>
        public string ProblemChoice1 { get; set; }

        /// <summary>
        /// Gets or sets the problem choice 2.
        /// </summary>
        public string ProblemChoice2 { get; set; }

        /// <summary>
        /// Gets or sets the problem choice 3.
        /// </summary>
        public string ProblemChoice3 { get; set; }

        /// <summary>
        /// Gets or sets the t-shirt size.
        /// </summary>
        public string TshirtSize { get; set; }

        /// <summary>
        /// Gets or sets the continuing education units.
        /// </summary>
        public string ContinuingEducationUnits { get; set; }

        /// <summary>
        /// Gets or sets the years of long-term judging experience.
        /// </summary>
        public string YearsOfLongTermJudgingExperience { get; set; }

        /// <summary>
        /// Gets or sets the years of spontaneous judging experience.
        /// </summary>
        public string YearsOfSpontaneousJudgingExperience { get; set; }

        /// <summary>
        /// Gets or sets the time registered.
        /// </summary>
        public DateTime? TimeRegistered { get; set; }

        /// <summary>
        /// Gets or sets the time assigned to team.
        /// </summary>
        public DateTime? TimeAssignedToTeam { get; set; }

        /// <summary>
        /// Gets or sets the time registration started.
        /// </summary>
        public DateTime? TimeRegistrationStarted { get; set; }

        /// <summary>
        /// Gets or sets the user agent.
        /// </summary>
        public string UserAgent { get; set; }
    }
}
