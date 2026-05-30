// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Page09ViewData.cs" company="Tardis Technologies">
//   Copyright 2014 Tardis Technologies. All rights reserved.
// </copyright>
// <summary>
//   Defines the Page09ViewData type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

// Decompiled with JetBrains decompiler
// Type: OdysseyMvc4.ViewData.TournamentRegistration.Page09ViewData
// Assembly: OdysseyMvc4, Version=1.0.5460.36587, Culture=neutral, PublicKeyToken=null
// MVID: 7B658547-521F-44CB-80FA-52857CB94B72
// Assembly location: C:\Users\rob\OneDrive\Odyssey\OdysseyProd\registration\bin\OdysseyMvc4.dll

using OdysseyMvc2024.Models;

namespace OdysseyMvc2024.ViewData.TournamentRegistration
{
    /// <summary>
    /// Backing data for Page 09 of the Tournament Registration wizard.
    /// </summary>
    public class Page09ViewData : BaseViewData
    {
        public string? Division { get; set; }

        public string? IsDoingSpontaneous { get; set; }

        public string? JudgeFirstName { get; set; }

        public string? JudgeLastName { get; set; }

        public string? ProblemName { get; set; }

        public string? SchoolName { get; set; }

        public Models.TournamentRegistration? TournamentRegistration { get; set; }

        public string? VolunteerFirstName { get; set; }

        public string? VolunteerLastName { get; set; }
    }
}
