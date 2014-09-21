// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Page01ViewData.cs" company="Tardis Technologies">
//   Copyright 2013 Tardis Technologies. All rights reserved.
// </copyright>
// <summary>
//   Defines the Page01ViewData type.
// </summary>
// <created>
//   October 27th, 2013
// </created>
// --------------------------------------------------------------------------------------------------------------------

namespace OdysseyMvc4.ViewData.JudgesRegistration
{
    using OdysseyMvc4.Models;

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
        /// Gets or sets the tournament info.
        /// </summary>
        public Event TournamentInfo { get; set; }

        /// <summary>
        /// Gets or sets the mail regional director hyper link.
        /// </summary>
        public string MailRegionalDirectorHyperLink { get; set; }

        /// <summary>
        /// Gets or sets the mail regional director hyper link text.
        /// </summary>
        public string MailRegionalDirectorHyperLinkText { get; set; }

        /// <summary>
        /// Gets the judges training location or "TBA" if it's not defined.
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
        /// Gets the judges training date.
        /// </summary>
        public string JudgesTrainingDate
        {
            get
            {
                return this.JudgesInfo.StartDate != null
                    ? this.JudgesInfo.StartDate.Value.ToLongDateString()
                    : "TBA";
            }
        }

        /// <summary>
        /// Gets the judges training time.
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

        /// <summary>
        /// Gets the tournament location or "TBA" if it's not defined.
        /// </summary>
        public string TournamentLocation
        {
            get
            {
                return !string.IsNullOrWhiteSpace(this.TournamentInfo.Location)
                    ? this.TournamentInfo.Location
                    : "TBA";
            }
        }

        /// <summary>
        /// Gets the tournament date.
        /// </summary>
        public string TournamentDate
        {
            get
            {
                return this.TournamentInfo.StartDate != null
                    ? this.TournamentInfo.StartDate.Value.ToLongDateString()
                    : "TBA";
            }
        }

        /// <summary>
        /// Gets the tournament training time.
        /// </summary>
        public string TournamentTime
        {
            get
            {
                return !string.IsNullOrWhiteSpace(this.TournamentInfo.Time)
                    ? this.TournamentInfo.Time
                    : "TBA";
            }
        }
    }
}