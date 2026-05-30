// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Page08ViewData.cs" company="Tardis Technologies">
//   Copyright 2014 Tardis Technologies. All rights reserved.
// </copyright>
// <summary>
//   Defines the Page08ViewData type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

// Decompiled with JetBrains decompiler
// Type: OdysseyMvc4.ViewData.TournamentRegistration.Page08ViewData
// Assembly: OdysseyMvc4, Version=1.0.5460.36587, Culture=neutral, PublicKeyToken=null
// MVID: 7B658547-521F-44CB-80FA-52857CB94B72
// Assembly location: C:\Users\rob\OneDrive\Odyssey\OdysseyProd\registration\bin\OdysseyMvc4.dll

namespace OdysseyMvc2023.ViewData.TournamentRegistration
{
    /// <summary>
    /// Backing data for Page 08 of the Tournament Registration wizard.
    /// </summary>
    public class Page08ViewData : BaseViewData
    {
        public string SchedulingIssues { get; set; }

        public string SpecialConsiderations { get; set; }

        public Models.TournamentRegistration TournamentRegistration { get; set; }
    }
}
