// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Page01ViewData.cs" company="Tardis Technologies">
//   Copyright 2021 Tardis Technologies. All rights reserved.
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

namespace OdysseyMvc4.ViewData.JudgesRegistration
{
    using OdysseyMvc4.Models;
    using OdysseyMvc4.ViewData;

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
        public string JudgesTrainingDate
        {
            get
            {
                return this.JudgesInfo.StartDate.HasValue
                    ? this.JudgesInfo.StartDate.Value.ToLongDateString()
                    : "TBA";
            }
        }

        /// <summary>
        /// Gets the judges training location or "TBA" if it is not defined.
        /// </summary>
        public string JudgesTrainingLocation
        {
            get
            {
                return !string.IsNullOrWhiteSpace(this.JudgesInfo.Location)
                    ? this.JudgesInfo.Location
                    : "TBA";
            }
        }

        /// <summary>
        /// Gets the judges training time or "TBA" if it is not defined.
        /// </summary>
        public string JudgesTrainingTime
        {
            get
            {
                return !string.IsNullOrWhiteSpace(this.JudgesInfo.Time)
                    ? this.JudgesInfo.Time
                    : "TBA";
            }
        }

        public string MailRegionalDirectorHyperLink { get; set; }

        public string MailRegionalDirectorHyperLinkText { get; set; }
    }
}
