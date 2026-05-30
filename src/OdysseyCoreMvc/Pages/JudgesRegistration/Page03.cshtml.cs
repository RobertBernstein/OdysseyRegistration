// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Page03.cshtml.cs" company="Tardis Technologies">
//   Copyright 2022 Tardis Technologies. All rights reserved.
// </copyright>
// <summary>
//   Defines the Page03Model type.
// </summary>
// <created>
//   October 27th, 2013
// </created>
// --------------------------------------------------------------------------------------------------------------------

using OdysseyCoreMvc.Data;
using OdysseyCoreMvc.Models;

namespace OdysseyCoreMvc.Pages.JudgesRegistration
{
    /// <summary>
    /// The Judges Registration Page02 page model.
    /// 
    /// Razor Pages are derived from PageModel. By convention, the PageModel derived class is named PageNameModel. For
    /// example, the Index page is named IndexModel.
    /// https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.razorpages.pagemodel
    /// </summary>
    public class Page03Model : BasePageModel
    {
        // The constructor uses dependency injection to add the OdysseyContext and logging to the page.
        // https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-6.0
        public Page03Model(OdysseyContext context, ILogger<BasePageModel> logger) : base(context, logger)
        {
            this.CurrentRegistrationType = RegistrationType.Judges;
        }

        /// <summary>
        /// Gets or sets the judges info.
        /// </summary>
        public Events? JudgesInfo { get; set; }

        /// <summary>
        /// Gets or sets the judge.
        /// </summary>
        public Judges? Judge { get; set; }

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
