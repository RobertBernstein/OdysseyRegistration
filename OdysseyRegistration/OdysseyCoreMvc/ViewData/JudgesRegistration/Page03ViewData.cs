// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Page03ViewData.cs" company="Tardis Technologies">
//   Copyright 2022 Tardis Technologies. All rights reserved.
// </copyright>
// <summary>
//   Defines the Page03ViewData type.
// </summary>
// <created>
//   October 27th, 2013
// </created>
// --------------------------------------------------------------------------------------------------------------------

using OdysseyCoreMvc.Models;

namespace OdysseyCoreMvc.ViewData.JudgesRegistration
{
    /// <summary>
    /// The page 03 view data.
    /// </summary>
    public class Page03ViewData : BaseViewData
    {
        /// <summary>
        /// Gets or sets the judges info.
        /// </summary>
        public Event? JudgesInfo { get; set; }

        /// <summary>
        /// Gets or sets the judge.
        /// </summary>
        public Judge? Judge { get; set; }

        /// <summary>
        /// Gets or sets the mail body.
        /// </summary>
        public string? MailBody { get; set; }

        /// <summary>
        /// Gets or sets the mail error message.
        /// </summary>
        public string? MailErrorMessage { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether email address was specified.
        /// </summary>
        public bool EmailAddressWasSpecified { get; set; }

        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        public string? ErrorMessage { get; set; }
    }
}
