// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Page02ViewData.cs" company="Tardis Technologies">
//   Copyright 2022 Tardis Technologies. All rights reserved.
// </copyright>
// <summary>
//   Defines the Page02ViewData type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using OdysseyCoreMvc.Models;

namespace OdysseyCoreMvc.ViewData.CoachesTrainingRegistration
{
    /// <summary>
    /// The page 02 view data.
    /// </summary>
    public class Page02ViewData : BaseViewData
    {
        /// <summary>
        /// Gets or sets the coaches training registration.
        /// </summary>
        public Models.CoachesTrainingRegistrations? CoachesTraining { get; set; }

        /// <summary>
        /// Gets or sets the coaches training info.
        /// </summary>
        public @Events? CoachesTrainingInfo { get; set; }

        /// <summary>
        /// Gets or sets the mail body.
        /// </summary>
        public string? MailBody { get; set; }

        /// <summary>
        /// Gets or sets the mail error message.
        /// </summary>
        public string? MailErrorMessage { get; set; }

        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        public string? ErrorMessage { get; set; }
    }
}