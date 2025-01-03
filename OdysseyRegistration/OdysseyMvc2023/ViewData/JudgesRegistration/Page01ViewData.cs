﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Page01ViewData.cs" company="Tardis Technologies">
//   Copyright 2014 Tardis Technologies. All rights reserved.
// </copyright>
// <summary>
//   Defines the Page01ViewData type.
// </summary>
// <created>
//   Sunday, October 27th, 2013
// </created>
// <updated>
//   Tuesday, September 30th, 2014
// </updated>
// --------------------------------------------------------------------------------------------------------------------

// Decompiled with JetBrains decompiler
// Type: OdysseyMvc4.ViewData.JudgesRegistration.Page01ViewData
// Assembly: OdysseyMvc4, Version=1.0.5460.36587, Culture=neutral, PublicKeyToken=null
// MVID: 7B658547-521F-44CB-80FA-52857CB94B72
// Assembly location: C:\Users\rob\OneDrive\Odyssey\OdysseyProd\registration\bin\OdysseyMvc4.dll

using OdysseyMvc2023.Models;

namespace OdysseyMvc2023.ViewData.JudgesRegistration
{
    /// <summary>
    /// The page 01 view data.
    /// </summary>
    public class Page01ViewData : BaseViewData
    {
        /// <summary>
        /// Gets or sets the judges info.
        /// </summary>
        public Event JudgesInfo { get; set; }

        /// <summary>
        /// Gets the judges training date or "TBA" if it is not defined.
        /// </summary>
        public string JudgesTrainingDate => this.JudgesInfo.StartDate.HasValue
            ? this.JudgesInfo.StartDate.Value.ToLongDateString()
            : "TBA";

        /// <summary>
        /// Gets the judges training location or "TBA" if it is not defined.
        /// </summary>
        public string JudgesTrainingLocation => !string.IsNullOrWhiteSpace(this.JudgesInfo.Location)
            ? this.JudgesInfo.Location
            : "TBA";

        /// <summary>
        /// Gets the judges training time or "TBA" if it is not defined.
        /// </summary>
        public string JudgesTrainingTime => !string.IsNullOrWhiteSpace(this.JudgesInfo.Time)
            ? this.JudgesInfo.Time
            : "TBA";

        public string MailRegionalDirectorHyperLink { get; set; }

        public string MailRegionalDirectorHyperLinkText { get; set; }
    }
}
