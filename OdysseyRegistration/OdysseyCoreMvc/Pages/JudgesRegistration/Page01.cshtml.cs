// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Page01.cshtml.cs" company="Tardis Technologies">
//   Copyright 2022 Tardis Technologies. All rights reserved.
// </copyright>
// <summary>
//   Defines the Page01Model type.
// </summary>
// <created>
//   Sunday, October 27th, 2013
// </created>
// <updated>
//   Tuesday, September 30th, 2014
// </updated>
// --------------------------------------------------------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using OdysseyCoreMvc.Data;
using OdysseyCoreMvc.Models;
using System.Text;

namespace OdysseyCoreMvc.Pages.JudgesRegistration
{
    /// <summary>
    /// The Judges Registration Page01 page model.
    /// 
    /// Razor Pages are derived from PageModel. By convention, the PageModel derived class is named PageNameModel. For
    /// example, the Index page is named IndexModel.
    /// https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.razorpages.pagemodel
    /// </summary>
    public class Page01Model : BasePageModel
    {
        // The constructor uses dependency injection to add the OdysseyContext and logging to the page.
        // https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-6.0
        public Page01Model(OdysseyContext context, ILogger<BasePageModel> logger) : base(context, logger)
        {
            this.CurrentRegistrationType = RegistrationType.Judges;
            this.FriendlyRegistrationName = this.GetDisplayableRegistrationName();
            Context = context;
        }

        /// <summary>
        /// Gets or sets the judges info.
        /// </summary>
        public Events? JudgesInfo { get; set; }

        /// <summary>
        /// Gets the judges training date or "TBA" if it is not defined.
        /// </summary>
        public string JudgesTrainingDate
        {
            get
            {
                return (JudgesInfo != null && JudgesInfo.StartDate.HasValue)
                    ? JudgesInfo.StartDate.Value.ToLongDateString()
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
                return (JudgesInfo != null && !string.IsNullOrWhiteSpace(JudgesInfo.Location))
                    ? JudgesInfo.Location
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
                return (JudgesInfo != null && !string.IsNullOrWhiteSpace(JudgesInfo.Time))
                    ? JudgesInfo.Time
                    : "TBA";
            }
        }

        public string? MailRegionalDirectorHyperLink { get; set; }

        public string? MailRegionalDirectorHyperLinkText
        {
            get
            {
                StringBuilder builder = new StringBuilder();
                builder.Append("mailto:");
                builder.Append(Config["RegionalDirectorEmail"]);
                string mailString =
                    ("?subject=I would like to help at the Region " +
                    RegionNumber +
                    " Tournament&body=I cannot be a judge this year, but would like to help in some other way.%0A%0AMy name is ______________________.%0A%0AMy phone number is ______________________.%0A%0A").Replace(" ", "%20");
                builder.Append(mailString);
                return builder.ToString();
            }
        }

        public OdysseyContext Context { get; }

        public async Task OnGetAsync()
        {
            // Create a LINQ query to select the events.
            // Note: The query is only defined at this point, it has not been run against the database.
            var events = from e in _context.Events
                         select e;

            JudgesInfo = await (from o in _context.Events
                          where o.EventName.Contains("Judges") && o.EventName.Contains("Training")
                          select o).FirstAsync();
        }

        public string BuildMailRegionalDirectorHyperLink()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("mailto:");
            builder.Append(Config["RegionalDirectorEmail"]);
            string mailString =
                ("?subject=I would like to help at the Region " +
                RegionNumber +
                " Tournament&body=I cannot be a judge this year, but would like to help in some other way.%0A%0AMy name is ______________________.%0A%0AMy phone number is ______________________.%0A%0A").Replace(" ", "%20");
            builder.Append(mailString);
            return builder.ToString();
        }
    }
}
