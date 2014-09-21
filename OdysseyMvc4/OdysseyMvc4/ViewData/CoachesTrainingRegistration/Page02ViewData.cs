// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Page02ViewData.cs" company="Tardis Technologies">
//   Copyright 2013 Tardis Technologies. All rights reserved.
// </copyright>
// <summary>
//   Defines the Page02ViewData type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OdysseyMvc4.ViewData.CoachesTrainingRegistration
{
    using OdysseyMvc4.Models;
    using OdysseyMvc4.ViewData;

    /// <summary>
    /// The page 02 view data.
    /// </summary>
    public class Page02ViewData : BaseViewData
    {
        /// <summary>
        /// Gets or sets the coaches training registration.
        /// </summary>
        public CoachesTrainingRegistration CoachesTraining { get; set; }

        /// <summary>
        /// Gets or sets the coaches training info.
        /// </summary>
        public @Event CoachesTrainingInfo { get; set; }

        /// <summary>
        /// Gets or sets the mail body.
        /// </summary>
        public string MailBody { get; set; }

        /// <summary>
        /// Gets or sets the mail error message.
        /// </summary>
        public string MailErrorMessage { get; set; }

        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        public string ErrorMessage { get; set; }
    }
}