// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Page02.cshtml.cs" company="Tardis Technologies">
//   Copyright 2022 Tardis Technologies. All rights reserved.
// </copyright>
// <summary>
//   Defines the Page02Model type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using OdysseyCoreMvc.Data;
using OdysseyCoreMvc.Models;

namespace OdysseyCoreMvc.Pages.CoachesTrainingRegistration
{
    /// <summary>
    /// The Coaches Training Registration Page01 page model.
    /// 
    /// Razor Pages are derived from PageModel. By convention, the PageModel derived class is named PageNameModel. For
    /// example, the Index page is named IndexModel.
    /// https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.razorpages.pagemodel
    /// </summary>
    public class Page02Model : BasePageModel
    {
        // The constructor uses dependency injection to add the OdysseyContext and logging to the page.
        // https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-6.0
        public Page02Model(OdysseyContext context, ILogger<BasePageModel> logger) : base(context, logger)
        {
        }

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